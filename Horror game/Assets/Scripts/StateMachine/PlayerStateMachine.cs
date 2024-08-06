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

    //constants
    float _rotationFactorPerFrame = 15.0f;
    [SerializeField] float _runMultiplier = 5.0f; [SerializeField] float _walkMultiplier = 2.0f;
    int _zero = 0;

    //gravity variables
    float initialGravity = -9.8f;
    //float _groundedGravity = -.05f;

    //jumping variables
    [SerializeField] bool _isJumpPressed = false;
    float _initialJumpVelocity;
    float _maxJumpHeight = 2.0f; // 4.0
    float _maxJumpTime = 0.3f; //.75
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


    //getters  and setters
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
    public float RunMultiplier { get { return _runMultiplier; } }
    public Vector2 CurrentMovementInput { get { return _currentMovementInput; } }
    //

    public float WalkMultiplier { get { return _walkMultiplier; } }
    [SerializeField] int playerHealth;
    [SerializeField] int playerHealthMax;


    public bool wasGroundedBeforePause;

    public LootCounter lootCounter;
    public static event Action<Transform> onPlayerCreated;

    public bool isPaused = false;
    void Awake()
    {
        onPlayerCreated?.Invoke(this.transform);
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
    }

    //
    void SetupJumpVariables()
    {
        float timeToApex = _maxJumpTime / 2;
        float initialGravity = (-2 * _maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        _initialJumpVelocity = (2 * _maxJumpHeight) / timeToApex;
        float secondJumpGravity = (-2 * (_maxJumpHeight + 2)) / Mathf.Pow((timeToApex * 1.25f), 2);
        float secondJumpInitialVelocity = (2 * (_maxJumpHeight + 2)) / (timeToApex * 1.25f);
        float thirdJumpGravity = (-2 * (_maxJumpHeight + 4)) / Mathf.Pow((timeToApex * 1.5f), 2);
        float thirdJumpInitialVelocity = (2 * (_maxJumpHeight + 4)) / (timeToApex * 1.5f);

        _initialJumpVelocities.Add(1, _initialJumpVelocity);
        _initialJumpVelocities.Add(2, secondJumpInitialVelocity);
        _initialJumpVelocities.Add(3, thirdJumpInitialVelocity);

        _jumpGravities.Add(0, initialGravity);
        _jumpGravities.Add(1, initialGravity);
        _jumpGravities.Add(2, secondJumpGravity);
        _jumpGravities.Add(3, thirdJumpGravity);
    }

    // Start is called before the first frame update
    void Start()
    {
        _characterController.Move(_appliedMovement * Time.deltaTime);
        //onPlayerHealthChanged?.Invoke(playerHealth, playerHealthMax);

    }

    // Update is called once per frame
    void Update()
    {

        HandleRotation();

        _currentState.UpdateStates();

        //TODO comment out to be replaced
        _cameraRelativeMovement = ConvertToCameraSpace(_appliedMovement);

        //TODO comment out to be replaced
        _characterController.Move(_cameraRelativeMovement * Time.deltaTime);



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

    void HandleRotation()
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
