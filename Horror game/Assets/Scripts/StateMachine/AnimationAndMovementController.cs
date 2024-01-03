using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationAndMovementController : MonoBehaviour
{
    //declare reference variables
    CharacterController _characterController;
    Animator _animator;
    PlayerInput _playerInput;

    //variables to store optimized setter/getter parameter IDs
    int _isWalkingHash;
    int _isRunningHash;

    // variables to store player input values
    Vector2 _currentMovementInput;
    Vector3 _currentMovement;
    Vector3 _appliedMovement; //_currentRunMovement
    bool _isMovementPressed;
    bool _isRunPressed;

    //constants
    float _rotationFactorPerFrame = 15.0f;
    float _runMultiplier = 3.0f;
    int _zero = 0;

    //gravity variables
    float _gravity = -9.8f;
    //float _groundedGravity = -.05f;

    //jumping variables
    bool _isJumpPressed = false;
    float _initialJumpVelocity;
    float _maxJumpHeight = 2.0f; // 4.0
    float _maxJumpTime = 0.3f; //.75
    bool _isJumping = false;
    int _isJumpingHash;
    int _jumpCountHash;
    bool _isJumpAnimating = false;
    public int jumpCount = 1; //  0
    Dictionary<int, float> _initialJumpVelocities = new Dictionary<int, float>();
    Dictionary<int, float> _jumpGravities = new Dictionary<int, float>();
    Coroutine currentJumpResetRoutine = null;

    //
    void Awake()
    {
        //
        _playerInput = new PlayerInput();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        //
        _isWalkingHash = Animator.StringToHash("isWalking");
        _isRunningHash = Animator.StringToHash("isRunning");
        _isJumpingHash = Animator.StringToHash("isJumping");
        _jumpCountHash = Animator.StringToHash("jumpCount");
        /*/
        //
        _playerInput.CharacterControlls.Move.started += OnMovementInput;
        _playerInput.CharacterControlls.Move.canceled += OnMovementInput;
        _playerInput.CharacterControlls.Move.performed += OnMovementInput;
        _playerInput.CharacterControlls.Run.started += onRun;
        _playerInput.CharacterControlls.Run.canceled += onRun;
        _playerInput.CharacterControlls.Jump.started += onJump;
        _playerInput.CharacterControlls.Jump.canceled += onJump;

        SetupJumpVariables();
        //*/
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

 
























































    // Update is called once per frame
    void Update()
    {
        //HandleRotation();
        //HandleAnimation();

        _appliedMovement.x = _isRunPressed ? _currentMovementInput.x * _runMultiplier : _currentMovement.x;
        _appliedMovement.z = _isRunPressed ? _currentMovementInput.y * _runMultiplier : _currentMovement.y;
        _characterController.Move(_appliedMovement * Time.deltaTime);
        //HandleGravity();
        //HandleJump();
    }


    //
    void SetupJumpVariables()
    {
        float timeToApex = _maxJumpTime / 2;
        _gravity = (-2 * _maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        _initialJumpVelocity = (2 * _maxJumpHeight) / timeToApex;

        float secondJumpGravity = (-2 * (_maxJumpHeight + 2)) / Mathf.Pow((timeToApex * 1.25f), 2);
        float secondJumpInitialVelocity = (2 * (_maxJumpHeight + 2)) / (timeToApex * 1.25f);

        float thirdJumpGravity = (-2 * (_maxJumpHeight + 4)) / Mathf.Pow((timeToApex * 1.5f), 2);
        float thirdJumpInitialVelocity = (2 * (_maxJumpHeight + 4)) / (timeToApex * 1.5f);

        _initialJumpVelocities.Add(1, _initialJumpVelocity);
        _initialJumpVelocities.Add(2, secondJumpInitialVelocity);
        _initialJumpVelocities.Add(3, thirdJumpInitialVelocity);


        _jumpGravities.Add(0, _gravity);
        _jumpGravities.Add(1, _gravity);
        _jumpGravities.Add(2, secondJumpGravity);
        _jumpGravities.Add(3, thirdJumpGravity);


    }





















































    IEnumerator jumpResetRoutine()
    {
        yield return new WaitForSeconds(.5f);
        jumpCount = 0; //
    }


    private void OnEnable()
    {
        //eneble the charachter controls action map
        _playerInput.CharacterControlls.Enable();
    }

    private void OnDisable()
    {
        //disable the charachter controls action map
        _playerInput.CharacterControlls.Disable();
    }
}
