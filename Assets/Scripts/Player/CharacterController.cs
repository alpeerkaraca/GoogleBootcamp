using System;
using UnityEngine;


public class CharacterController : MonoBehaviour
{
    //My Player Rigidbody Settings: Mass = 10, Gravity Scale = 5
    //Also don't forget to add "Sprint" option to LShift, "Dash" to LCtrl on Edit => ProjectSettings => InputManager
    
    [Header("Move")] 
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float runSpeed = 9f;
    [SerializeField] private float jumpForce = 25000f;
    private float _horizontalMove;
    private Vector3 _moveDirection;
    private Vector2 _velocity = Vector2.zero;
    private bool _isGround;
    private bool _isSprinting;
    private bool _isWalking;
    private bool _jump;
    private bool _sprint;
    private bool _facingRight = true;
    private bool _canFlip;
    
    [Header("Dash")]
    [SerializeField] private float dashForce = 250f;
    [SerializeField] private float dashCooldownTime = .5f;
    private float _dashCooldownPassed;
    private bool _isDashing;
    private bool _dashReady;
    private bool _canDash;
    
    [Header("Taking Damage Knockback", order = 3)]
    private bool _knockback;
    [SerializeField]
    private float knockbackDuration;
    [SerializeField]
    private Vector2 knockbackSpeed;
    private float _knockbackStartTime;
    
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    
    
    //Turn Strings to ID for better optimization.
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Sprint = Animator.StringToHash("Sprint");

    private void Start()
    {  
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //Basics for Movement
        CheckMove();
        CheckKnockBack();
        CheckDash();
        CheckJump();
        CheckSprint();
    }

    private void FixedUpdate()
    {
        LetsWalk();
        LetsJump();
        LetsSprint();
        LetsDash();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {   _isGround = true;
            _canFlip = true;
        }
    }
    //******* DEFINING FUNCTIONS DOWN BELOW*******
    
    //--Check Functions--
    
    private void CheckMove()
    { 
        _horizontalMove =Input.GetAxisRaw("Horizontal");
        _moveDirection = new Vector3(_horizontalMove, 0f);
        CheckFlipDir();
        _animator.SetFloat(Speed, Math.Abs(_horizontalMove));
        _isWalking = Input.GetButton("Horizontal");
    }

    private void CheckSprint()
    {
        _isSprinting = Input.GetButton("Sprint");   //Get Sprint Value
        _animator.SetBool(Sprint, _isSprinting);
    }
    
    private void CheckJump()
    {
        if (Input.GetButtonDown("Jump")) //Check is pressed Space
            _jump = _isGround; //Set jump to ground so don't add jump task to queue.
        _animator.SetBool(Jump, _jump && _isGround);
    }
    
    private void CheckDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && (_dashCooldownPassed >= dashCooldownTime)) //Check reqs. of dash
        {
            _isDashing = true;
            _canDash = true;
            _canFlip = false;
        }
        if (!(_dashCooldownPassed >= dashCooldownTime))          //Check CD
        {
            _isDashing = false;
            _canDash = false;
            _dashCooldownPassed += Time.deltaTime;
            Mathf.Clamp(_dashCooldownPassed, 0f, dashCooldownTime);
        }
    }
    
    private void CheckFlipDir()
    {
        if(_facingRight && _horizontalMove < 0)
            Flip();
        else if(!_facingRight && _horizontalMove > 0)
            Flip();
    }
    
    private void CheckKnockBack()
    {
        if ((Time.time >= _knockbackStartTime + knockbackDuration) && _knockback)
        {
            _knockback = false;
            _rigidbody2D.velocity = new Vector2(0.0f, _rigidbody2D.velocity.y);
        }
    }
    
    //--Physics Functions--
    private void LetsWalk()
    {
        if(_isWalking && !_knockback)
            _rigidbody2D.velocity = Vector2.SmoothDamp(_rigidbody2D.velocity, _moveDirection * moveSpeed, ref _velocity, 0.05f); //Add smoothing time CR: YouTube:Brackeys

    }
    private void LetsSprint()
    {   
        if (_isSprinting && !_knockback)
        { 
            _rigidbody2D.velocity = new Vector2(_horizontalMove * runSpeed, _rigidbody2D.velocity.y);
        }
    }
    private void LetsJump()
    {
        if (_isGround && _jump && !_knockback) //Make char jump
        {
            if (_isWalking && !_isSprinting && _isGround)
            {
                _rigidbody2D.AddForce(new Vector2(0f, jumpForce + 6000f),ForceMode2D.Force);
                _jump = false;
                _isGround = false;
            }

            if (_isWalking && _isSprinting && _isGround)
            {
                _rigidbody2D.AddForce(new Vector2(0f,jumpForce + 6000f));
                _jump = false;
                _isGround = false;
            }

            if (!_isWalking && !_isSprinting && _isGround)
            {
                _rigidbody2D.AddForce(new Vector2(0f, jumpForce - 15000));
                _jump = false;
                _isGround = false;
            }
        }
    }
    
    private void LetsDash()
    {
        if (_canDash && !_knockback)
        {
            _rigidbody2D.AddForce(_moveDirection * dashForce, ForceMode2D.Impulse);
            _canDash = false;
            _canFlip = true;
            _isDashing = true;
            _dashCooldownPassed = 0f;
        }
    }
    
    public void Knockback(int direction)
    {
        _knockback = true;
        _knockbackStartTime = Time.time;
        _rigidbody2D.velocity = new Vector2(knockbackSpeed.x * direction, knockbackSpeed.y);
    }
    
    //--Other Functions--
    public void DisableFlip()
    {
        _canFlip = false;
    }

    public void EnableFlip()
    {
        _canFlip = true;
    }
    
    private void Flip()
    {
        if (_canFlip && !_knockback)
        {
            _facingRight = !_facingRight;
            _horizontalMove *= -1;
            transform.Rotate(0f, 180f, 0f);
        }
    }
    
    public float FacingDir()
    {
        return _horizontalMove;
    }

    public bool GetDashStatus()
    {
        return _isDashing;
    }
    
}