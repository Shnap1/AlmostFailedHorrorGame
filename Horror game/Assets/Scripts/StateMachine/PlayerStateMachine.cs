using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    //declare reference variables
    CharacterController _characterController;
    Animator _animator;
    PlayerInput _playerInput;

    //variables to store optimized setter/getter parameter IDs
    int _isWalkingHash;
    int _isRunningHash;
    int _isFallingHash;


    // variables to store player input values
    Vector2 _currentMovementInput;
    Vector3 _currentMovement;
    Vector3 _appliedMovement; //_currentRunMovement
    Vector3 _cameraRelativeMovement;
    [SerializeField] bool _isMovementPressed;
    bool _isRunPressed;
    [SerializeField] bool isMainPlayer = false;

    //constants
    float _rotationFactorPerFrame = 15.0f;

    [Header("Movenment Multipliers")]
    public float runMultiplier;
    public float _currentRunMultiplier = 5.0f;

    public float walkMultiplier;
    public float _currentWalkMultiplier = 2.0f;
    int _zero = 0;

    //gravity variables
    float initialGravity = -9.8f;
    //float _groundedGravity = -.05f;

    //jumping variables
    [SerializeField] bool _isJumpPressed = false;
    float _initialJumpVelocity;

    public float _currentJumpHeight; // 4.0
    public float _currentJumpTime; //.75

    public float _maxJumpHight;
    public float _maxJumpTime; //.75

    public float _maxJumpPadHeight;

    public float _maxJumpPadTime; //.75




    [SerializeField] bool _isJumping = false;
    int _isJumpingHash;
    int _jumpCountHash;
    bool _requireNewJumpPress = false;
    [SerializeField] int _jumpCount = 1; //  0
    Dictionary<int, float> _initialJumpVelocities = new Dictionary<int, float>();
    Dictionary<int, float> _jumpGravities = new Dictionary<int, float>();
    Coroutine currentJumpResetRoutine = null;

    //state variables
    PlayerBaseState _currentState;
    PlayerStateFactory _states;

    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public Animator Animator { get { return _animator; } set { _animator = value; } }
    public CharacterController CharacterController { get { return _characterController; } set { _characterController = value; } }
    public Coroutine CurrentJumpResetRoutine { get { return currentJumpResetRoutine; } set { currentJumpResetRoutine = value; } }
    public Dictionary<int, float> InitialJumpVelocities { get { return _initialJumpVelocities; } }
    public Dictionary<int, float> JumpGravities { get { return _jumpGravities; } }
    public int JumpCount { get { return _jumpCount; } set { _jumpCount = value; } }
    public int IsWalkingHash { get { return _isWalkingHash; } }
    public int IsRunningHash { get { return _isRunningHash; } }
    public int IsFallingHash { get { return _isFallingHash; } }
    public int IsJumpingHash { get { return _isJumpingHash; } }
    public int JumpCountHash { get { return _jumpCountHash; } }
    public bool IsMovementPressed { get { return _isMovementPressed; } }
    public bool IsRunPressed { get { return _isRunPressed; } }
    public bool RequireNewJumpPress { get { return _requireNewJumpPress; } set { _requireNewJumpPress = value; } }
    public bool IsJumping { set { _isJumping = value; } }
    public bool IsJumpPressed { get { return _isJumpPressed; } }
    //public float GroundedGravity { get { return _groundedGravity; } }
    public float Gravity { get { return initialGravity; } }
    public float CurrentMovementY { get { return _currentMovement.y; } set { _currentMovement.y = value; } }
    public float AppliedMovementY { get { return _appliedMovement.y; } set { _appliedMovement.y = value; } }
    public float AppliedMovementX { get { return _appliedMovement.x; } set { _appliedMovement.x = value; } }
    public float AppliedMovementZ { get { return _appliedMovement.z; } set { _appliedMovement.z = value; } }
    public float RunMultiplier { get { return _currentRunMultiplier; } }
    public Vector2 CurrentMovementInput { get { return _currentMovementInput; } }
    //

    public float WalkMultiplier { get { return _currentWalkMultiplier; } }

    public float launchGravity = -100f;
    private bool isLaunching = false;
    public float jumpForce = 10f;
    public bool isJumpPadCollided = false;
    public bool wasGroundedBeforePause;


    [Header("Stats")]

    [SerializeField] int playerHealth;
    [SerializeField] int playerHealthMax;



    public LootCounter lootCounter;
    public static event Action<Transform> onPlayerCreated;

    public bool isPaused = false;

    public bool FPS = true;

    //launch
    Rigidbody boxRigidbody;

    private Vector3 velocity;


    void Awake()
    {
        if (isMainPlayer)
        {
            onPlayerCreated?.Invoke(this.transform);
            // Debug.Log("onPlayerCreated?.Invoke in " + this.gameObject.name + "with isMainPlayer set to" + isMainPlayer);
        }
        // onPlayerCreated?.Invoke(this.transform);
        // initialy set reference variables
        lootCounter = GetComponent<LootCounter>();

        _playerInput = new PlayerInput();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        //setup state
        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();

        //set the parameter hash reference
        _isWalkingHash = Animator.StringToHash("isWalking");
        _isRunningHash = Animator.StringToHash("isRunning");
        _isFallingHash = Animator.StringToHash("isFalling");
        _isJumpingHash = Animator.StringToHash("isJumping");
        _jumpCountHash = Animator.StringToHash("jumpCount");

        //
        _playerInput.CharacterControlls.Move.started += OnMovementInput;
        _playerInput.CharacterControlls.Move.canceled += OnMovementInput;
        _playerInput.CharacterControlls.Move.performed += OnMovementInput;
        _playerInput.CharacterControlls.Run.started += onRun;
        _playerInput.CharacterControlls.Run.canceled += onRun;
        _playerInput.CharacterControlls.Jump.started += onJump;
        _playerInput.CharacterControlls.Jump.canceled += onJump;

        SetupJumpVariables();

        boxRigidbody = GetComponent<Rigidbody>();

        _currentWalkMultiplier = walkMultiplier;
        _currentRunMultiplier = runMultiplier;

    }

    //
    public void SetupJumpVariables()
    {
        if (_initialJumpVelocities.Count > 0) _initialJumpVelocities.Clear();//new shit
        if (_jumpGravities.Count > 0) _jumpGravities.Clear(); //new shit

        float timeToApex = _currentJumpTime / 2;
        float initialGravity = (-2 * _currentJumpHeight) / Mathf.Pow(timeToApex, 2);
        _initialJumpVelocity = (2 * _currentJumpHeight) / timeToApex;
        float secondJumpGravity = (-2 * (_currentJumpHeight + 2)) / Mathf.Pow((timeToApex * 1.25f), 2);
        float secondJumpInitialVelocity = (2 * (_currentJumpHeight + 2)) / (timeToApex * 1.25f);
        float thirdJumpGravity = (-2 * (_currentJumpHeight + 4)) / Mathf.Pow((timeToApex * 1.5f), 2);
        float thirdJumpInitialVelocity = (2 * (_currentJumpHeight + 4)) / (timeToApex * 1.5f);

        _initialJumpVelocities.Add(1, _initialJumpVelocity);
        _initialJumpVelocities.Add(2, secondJumpInitialVelocity);
        _initialJumpVelocities.Add(3, thirdJumpInitialVelocity);

        _jumpGravities.Add(0, initialGravity);
        _jumpGravities.Add(1, initialGravity);
        _jumpGravities.Add(2, secondJumpGravity);
        _jumpGravities.Add(3, thirdJumpGravity);
    }


    public void Launch()
    {
        isJumpPadCollided = true;
        // velocity = force;
        // isLaunching = true;

        // boxRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);


    }

    public void LaunchLogic()
    {
        if (!CharacterController.enabled) return;

        if (isLaunching)
        {
            CharacterController.Move(velocity * Time.deltaTime);
            velocity += Vector3.up * launchGravity * Time.deltaTime; // simulate gravity

            if (CharacterController.isGrounded)
            {
                isLaunching = false;
                velocity = Vector3.zero;
            }
        }
        else
        {
            // Your regular movement logic here
        }
    }


    void Start()
    {
        _characterController.Move(_appliedMovement * Time.deltaTime);
        //onPlayerHealthChanged?.Invoke(playerHealth, playerHealthMax);
    }

    public void TurnOnCController(bool value)
    {
        _characterController.enabled = value;
        if (value) _characterController.Move(_appliedMovement * Time.deltaTime);
    }
    // Update is called once per frame
    void Update()
    {
        //NEW
        LaunchLogic();

        FPSModeSwitch();
        if (FPS) HandleCharacterRotationTwo();
        else HandleCharacterRotation();

        // HandleCharacterRotation();
        _currentState.UpdateStates();



        //TODO comment out to be replaced
        _cameraRelativeMovement = ConvertToCameraSpace(_appliedMovement);

        //TODO comment out to be replaced
        _characterController.Move(_cameraRelativeMovement * Time.deltaTime);


    }

    void FPSModeSwitch()
    {
        if (Input.GetKeyDown(KeyCode.F)) FPS = !FPS;

    }

    Vector3 ConvertToCameraSpace(Vector3 vectorToRotate)
    {
        //
        float currentYValue = vectorToRotate.y;

        // get the forward and right directional vectors of the camera
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        // remove the Y values to ignore upward/downward camera angles
        cameraForward.y = 0;
        cameraRight.y = 0;

        //re-normalize both vectots so they can each have a magnitude of 1
        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;

        //rotate the X and Z VectorToRotate values to cameraspace
        Vector3 cameraForwardZProduct = vectorToRotate.z * cameraForward;
        Vector3 cameraRightXProduct = vectorToRotate.x * cameraRight;

        //the sum of both products is the Vector3 in camera
        Vector3 vectorRotatedToCameraSpace = cameraForwardZProduct + cameraRightXProduct;
        vectorRotatedToCameraSpace.y = currentYValue;
        return vectorRotatedToCameraSpace;
    }

    void HandleTPCharRotation()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector3 worldAimTarget = mouseWorldPosition;
        worldAimTarget.y = transform.position.y;
        Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
        // transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        Vector3 _TPCharRotation = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        // _characterController.Move(_TPCharRotation * Time.deltaTime);
    }
    void HandleCharacterRotation()
    {
        Vector3 positionToLookAt;
        //
        positionToLookAt.x = _cameraRelativeMovement.x;
        positionToLookAt.y = _zero;
        positionToLookAt.z = _cameraRelativeMovement.z;
        //
        Quaternion currentRotation = transform.rotation;

        if (_isMovementPressed)
        {
            //
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            //
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, _rotationFactorPerFrame * Time.deltaTime);
        }
    }

    void HandleCharacterRotationTwo()
    {
        // Get the camera's forward direction
        Vector3 cameraForward = Camera.main.transform.forward;

        // Normalize the vector to ensure it has length 1
        cameraForward.y = 0; // Ignore vertical component
        cameraForward = cameraForward.normalized;

        // Calculate the target rotation
        Quaternion targetRotation = Quaternion.LookRotation(cameraForward);

        // Smoothly rotate the character towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationFactorPerFrame * Time.deltaTime);
    }
    //
    void OnMovementInput(InputAction.CallbackContext context) //TODO comment out to be replaced
    {
        _currentMovementInput = context.ReadValue<Vector2>();
        _isMovementPressed = _currentMovementInput.x != 0 || _currentMovementInput.y != _zero;
    }

    //
    void onJump(InputAction.CallbackContext context)
    {
        _isJumpPressed = context.ReadValueAsButton();
        _requireNewJumpPress = false;
    }

    //callback handler function for run buttons
    void onRun(InputAction.CallbackContext context)
    {
        _isRunPressed = context.ReadValueAsButton();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "TargetCollectable")
        {
            lootCounter.AddLoot();
        }
    }
    //public static Action<int, int> onPlayerHealthChanged;

    void RecordIfGoundedBeforePause(bool ifPaused)
    {
        wasGroundedBeforePause = CharacterController.isGrounded;
    }

    private void OnEnable()
    {
        //eneble the charachter controls action map
        _playerInput.CharacterControlls.Enable();
        SettingsMenu.OnGamePaused += RecordIfGoundedBeforePause;
    }

    private void OnDisable()
    {
        //disable the charachter controls action map
        _playerInput.CharacterControlls.Disable();
        SettingsMenu.OnGamePaused -= RecordIfGoundedBeforePause;

    }
}
