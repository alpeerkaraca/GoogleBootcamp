using System;
using Enemy;
using UnityEngine;

namespace Player
{
    public class CharacterController : MonoBehaviour
    {
        //My Player Rigidbody Settings: Mass = 10, Gravity Scale = 5
        //Also don't forget to add "Sprint" option to LShift, "Dash" to LCtrl on Edit => ProjectSettings => InputManager



        [Header("Move")]
        public float moveSpeed = 5f;
        public float runSpeed = 9f;
        [SerializeField] private float jumpVelocity = 50f;
        public float horizontalMove;
        private Vector3 _moveDirection;
        private Vector2 _velocity = Vector2.zero;
        private bool _isGround;
        private bool _isSprinting;
        private bool _isWalking;
        private bool _jump;
        private bool _sprint;
        public bool facingRight = true;
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


        [Header("Taking Damage Knockback")]
        private bool _knockback;
        private bool attack;
        [SerializeField]
        private float knockbackDuration;
        [SerializeField]
        private Vector2 knockbackSpeed;
        private float _knockbackStartTime;

        [Header("ParticleSystem")]
        public ParticleSystem dust;

        private Rigidbody2D _rigidbody2D;
        public Animator _animator;
        public bool isDialogBoxOpen;


        //Turn Strings to ID for better optimization.
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int Sprint = Animator.StringToHash("Sprint");
        private static readonly int TookDamage = Animator.StringToHash("Took Damage");

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            //Basics for Movement
            if (!isDialogBoxOpen)
            {
                CheckMove();
                CheckKnockBack();
                CheckDash();
                CheckJump();
                CheckSprint();
                CheckTookDamage();
            }
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
            {
                _isGround = true;
                _canFlip = true;
            }
        }
        //******* DEFINING FUNCTIONS DOWN BELOW*******

        //--Check Functions--

        private void CheckMove()
        {
            horizontalMove = Input.GetAxisRaw("Horizontal");
            _moveDirection = new Vector3(horizontalMove, 0f);
            CheckFlipDir();
            _animator.SetFloat(Speed, Math.Abs(horizontalMove));
            _isWalking = Input.GetButton("Horizontal");
        }

        private void CheckSprint()
        {
            _isSprinting = Input.GetButton("Sprint"); //Get Sprint Value
            _animator.SetBool(Sprint, _isSprinting);
        }

        private void CheckJump()
        {
            if (Input.GetButtonDown("Jump")) //Check is pressed Space
                _jump = true; //Set jump to ground so don't add jump task to queue.
            _animator.SetBool(Jump, !_isGround);
        }

        private void CheckDash()
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                if (Time.time >= (dashStarted + dashCooldownTime))
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
            if (facingRight && horizontalMove < 0)
                Flip();
            else if (!facingRight && horizontalMove > 0)
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
            _animator.SetBool(TookDamage, _knockback);
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
            if (_isWalking && !_knockback)
                _rigidbody2D.velocity = Vector2.SmoothDamp(_rigidbody2D.velocity, _moveDirection * moveSpeed, ref _velocity, 0.05f); //Add smoothing time CR: YouTube:Brackeys

        }
        private void LetsSprint()
        {
            if (_isSprinting && !_knockback)
            {
                _rigidbody2D.velocity = new Vector2(horizontalMove * runSpeed, _rigidbody2D.velocity.y);
            }
        }

        private void LetsJump()
        {
            if (_isGround && _jump && !_knockback) //Make char jump
            {
                CreateDust();
                if (horizontalMove == 0)
                {
                    _rigidbody2D.velocity = Vector2.up * jumpVelocity;
                    _isGround = false;
                    _jump = false;

                }

                if (Math.Abs(horizontalMove) > 0 && _isSprinting == false)
                {
                    _rigidbody2D.velocity = Vector2.up * (jumpVelocity + 30);
                    _isGround = false;
                    _jump = false;

                }

                if (Math.Abs(horizontalMove) > 0 && _isSprinting)
                {
                    _rigidbody2D.velocity = Vector2.up * (jumpVelocity + 30);
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
                    _rigidbody2D.velocity = new Vector2(dashForce * horizontalMove, _rigidbody2D.velocity.y);
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
                facingRight = !facingRight;
                horizontalMove *= -1;
                transform.Rotate(0f, 180f, 0f);
                CreateDust();
            }
        }

        public float FacingDir()
        {
            return horizontalMove;
        }

        public bool GetDashStatus()
        {
            return _isDashing;
        }

        void CreateDust()
        {
            dust.Play();
        }


    }
}