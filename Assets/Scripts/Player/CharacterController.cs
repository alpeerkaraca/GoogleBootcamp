using System;
using Enemy;
using Unity.Mathematics;
using UnityEngine;


public class CharacterController : MonoBehaviour
{
    //My Player Rigidbody Settings: Mass = 10, Gravity Scale = 5
    //Also don't forget to add "Sprint" option to LShift, "Dash" to LCtrl on Edit => ProjectSettings => InputManager
    
    [Header("Move")] 
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float runSpeed = 9f;
    [SerializeField] private float jumpVelocity = 50f;
    public float _horizontalMove;
    private Vector3 _moveDirection;
    private Vector2 _velocity = Vector2.zero;
    private bool _isGround;
    private bool _isSprinting;
    private bool _isWalking;
    private bool _jump;
    private bool _sprint;
    public bool _facingRight = true;
    private bool _canFlip;
    private bool canMove;
    
    [Header("Dash")]
    [SerializeField] private float dashForce = 250f;
    [SerializeField] private float dashCooldownTime = .5f;
    [SerializeField] private float distanceBetweenImages;
    [SerializeField] private float dashTime;
    private float lastImageXpos;
    private float dashStarted = Mathf.NegativeInfinity;
    private float dashTimeLeft;
    private bool _isDashing;


    [Header("Taking Damage Knockback", order = 3)]
    private bool _knockback;
    [SerializeField]
    private float knockbackDuration;
    [SerializeField]
    private Vector2 knockbackSpeed;
    private float _knockbackStartTime;
    
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private Enemy1AI eAI;
    
    
    //Turn Strings to ID for better optimization.
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Sprint = Animator.StringToHash("Sprint");
    private static readonly int tookDamage = Animator.StringToHash("Took Damage");

    private void Start()
    {  
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        eAI = GameObject.FindWithTag("Enemy").GetComponent<Enemy1AI>();
    }

    private void Update()
    {
        //Basics for Movement
        CheckMove();
        CheckKnockBack();
        CheckDash();
        CheckJump();
        CheckSprint();
        CheckTookDamage();
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
        _animator.SetBool(Jump, _jump);
    }
    
    private void CheckDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if(Time.time >= (dashStarted + dashCooldownTime))
                AttempToDash();
        }
    }

    private void AttempToDash()
    {
        _isDashing = true;
        dashTimeLeft = dashTime;
        dashStarted = Time.time;
        PlayerAfterImagePool.Instance.GetFromPool();
        lastImageXpos = transform.position.x;
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

    private void CheckTookDamage()
    {
        _animator.SetBool(tookDamage, _knockback);
    }

    private void TryDash()
    {
        _isDashing = true;
        dashStarted = Time.time;
        PlayerAfterImagePool.Instance.GetFromPool();
        lastImageXpos = transform.position.x;
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
            if (_horizontalMove == 0)
            {
                _rigidbody2D.velocity = Vector2.up * jumpVelocity;
                _isGround = false;
                _jump = false;

            }

            if (Math.Abs(_horizontalMove) > 0 && _isSprinting == false)
            {
                _rigidbody2D.velocity = Vector2.up * (jumpVelocity + 40);
                _isGround = false;
                _jump = false;

            }

            if (Math.Abs(_horizontalMove) > 0 && _isSprinting)
            {
                _rigidbody2D.velocity = Vector2.up * (jumpVelocity + 40);
                _isGround = false;
                _jump = false;

            }
        }
    }

    private void LetsDash()
        {
            if (_isDashing && !_knockback)
            {
                if (dashTimeLeft > 0)
                {
                    _canFlip = false;
                    _rigidbody2D.velocity = new Vector2(dashForce * _horizontalMove, _rigidbody2D.velocity.y);
                    dashTimeLeft -= Time.deltaTime;

                    if (Math.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages)
                    {
                        PlayerAfterImagePool.Instance.GetFromPool();
                        lastImageXpos = transform.position.x;
                    }
                }

                if (dashTimeLeft <= 0)
                {
                    _isDashing = false;
                    _canFlip = true;
                }
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