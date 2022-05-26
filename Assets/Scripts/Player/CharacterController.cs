using System;
using UnityEngine;


public class CharacterController : MonoBehaviour
{
    //My Player Rigidbody Settings: Mass = 10, Gravity Scale = 5
    //Also don't forget to add "Sprint" option to LShift, "Dash" to LCtrl on Edit => ProjectSettings => InputManager
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    [Header("Move")]
    [SerializeField]private float moveSpeed = 5f;
    [SerializeField]private float runSpeed = 9f;
    [SerializeField]private float jumpForce = 25000f;
    private float _horizontalMove;
    private Vector3 _moveDirection;
    private Vector2 _velocity = Vector2.zero;
    private bool _isGround;
    private bool _isSprinting;
    private bool _jump;
    private bool _sprint;

    [Header("Dash")]
    [SerializeField] private float dashForce = 250f;
    [SerializeField] private float dashCooldownTime = .5f;
    private float _dashCooldownPassed;
    private bool _isDashing;
    private bool _dashReady;
    private bool _canDash;
    
    //Turn Strings to ID for better optimization.
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Jump = Animator.StringToHash("Jump");

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //Basics for Movement
        _horizontalMove = Input.GetAxisRaw("Horizontal");
        _moveDirection = new Vector3(_horizontalMove, 0f);

        Facing();
        _animator.SetFloat(Speed, Math.Abs(_horizontalMove));
        
        if (Input.GetButtonDown("Jump")) //Check is pressed Space
            _jump = _isGround; //Set jump to ground so don't add jump task to queue.
        _animator.SetBool(Jump, _jump && _isGround);

        if (Input.GetKeyDown(KeyCode.LeftControl) && (_dashCooldownPassed >= dashCooldownTime))
            _canDash = true;
        if(!(_dashCooldownPassed >= dashCooldownTime))
        {
            _canDash = false;
            _dashCooldownPassed += Time.deltaTime;
            Mathf.Clamp(_dashCooldownPassed, 0f, dashCooldownTime);
        }

        _isSprinting = Input.GetButton("Sprint"); 
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = Vector2.SmoothDamp(_rigidbody2D.velocity, _moveDirection * moveSpeed, ref _velocity, 0.05f); //Add smoothing time CR: YouTube:Brackeys

        if (_isGround && _jump) //Make char jump
        {
            _rigidbody2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Force);
            _jump = false;
            _isGround = false;
        }

        if (_isSprinting)
        {
            _rigidbody2D.velocity = new Vector2(_horizontalMove * runSpeed, _rigidbody2D.velocity.y);
        }

        if (_canDash)
        {
            _rigidbody2D.AddForce(_moveDirection * dashForce, ForceMode2D.Impulse);
            _canDash = false;
            _dashCooldownPassed = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
            _isGround = true;
    }

    private void Facing()
    {
        _spriteRenderer.flipX = _horizontalMove switch
        {
            < 0f => true,
            > 0f => false,
            _ => _spriteRenderer.flipX
        };
    }
}