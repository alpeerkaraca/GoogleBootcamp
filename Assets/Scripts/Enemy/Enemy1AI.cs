using UnityEngine;

namespace Enemy
{
    public class Enemy1AI : MonoBehaviour 
    { 
        [SerializeField] private Transform 
            groundCheck,
            touchDamageCheck;
        [SerializeField] private LayerMask 
            whatIsGround,
            whatIsPlayer;

        [SerializeField] private float
            groundCheckDistance,
            movementSpeed,
            maxHp,
            knockbackDuration,
            lastTouchDamageTime,
            touchDamageCooldown,
            touchDamage,
            touchDamageWidth,
            touchDamageHeight;

        [SerializeField] private GameObject
            hitParticles,
            deathChunkParticle,
            deathBloodParticle;
        [SerializeField] private Vector2 
            knockbackSpeed,
            touchDamageBotLeft,
            touchDamageTopRight;
    
        private enum State
        { 
            Moving, 
            Knockback,
            Dead
        }

        private State _currentState;
        private Rigidbody2D _aliveRb;
        private GameObject _alive;
        private Vector2 _movement;
        private Animator _aliveAnim;
    
        private float 
            _currentHp,
            _knockbackStartTime;

        private float[] attackDetails = new float[2];
        private int 
            _facingDir,
            _damageDir;

        private bool _groundDetected;


        private static readonly int Knockback = Animator.StringToHash("Knockback");

        private void Start()
        {
            _alive = transform.Find("Alive").gameObject;
            _aliveRb = _alive.GetComponent<Rigidbody2D>();
            _aliveAnim = _alive.GetComponent<Animator>();
            _facingDir = 1;
            _currentHp = maxHp;
        }


        private void Update()
        {
            switch (_currentState)
            {
                case State.Moving:
                    UpdateMovingState();
                    break;
            
                case State.Knockback:
                    UpdateKnockbackState();
                    break;
            
                case State.Dead:
                    UpdateDeadState();
                    break;
            }
        }

        //--WALKING STATE--
        private void EnterMovingState()
        {
            
        }

        private void UpdateMovingState()
        {
            _groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    
            CheckTouchDamage();
            if (!_groundDetected)
            {
                Flip();
            }
            else
            {
                _movement.Set(movementSpeed * _facingDir, _aliveRb.velocity.y);
                _aliveRb.velocity = _movement;
            }
        }

        private void ExitMovingState() 
        {
            
        }
    
        // --KNOCKBACK STATE---
        private void EnterKnockbackState()
        {
            _knockbackStartTime = Time.time;
            _movement.Set(knockbackSpeed.x * _damageDir, knockbackSpeed.y);
            _aliveRb.velocity = _movement;
            _aliveAnim.SetBool(Knockback, true);
        }

        private void UpdateKnockbackState()
        {
            if(Time.time >= _knockbackStartTime + knockbackDuration)
                SwitchState(State.Moving);
        }

        private void ExitKnockbackState()
        {
            _aliveAnim.SetBool(Knockback, false);
        } 
    
        //--DEAD STATE--
        private void EnterDeadState()
        {
            var position = _alive.transform.position;
            Instantiate(deathChunkParticle, position, deathChunkParticle.transform.rotation);
            Instantiate(deathBloodParticle, position, deathBloodParticle.transform.rotation);
            Destroy(gameObject);
        }

        private void UpdateDeadState()
        {
        
        }

        private void ExitDeadState()
        {
        
        }
    
        //--OTHER FUNCTIONS--


        // ReSharper disable Unity.PerformanceAnalysis
        private void CheckTouchDamage()
        {
            if (Time.time >= lastTouchDamageTime + touchDamageCooldown)
            {
                touchDamageBotLeft.Set(touchDamageCheck.position.x - (touchDamageWidth / 2),touchDamageCheck.position.y - (touchDamageHeight / 2));
                touchDamageTopRight.Set(touchDamageCheck.position.x + (touchDamageWidth / 2),touchDamageCheck.position.y + (touchDamageHeight / 2));

                Collider2D hit = Physics2D.OverlapArea(touchDamageBotLeft, touchDamageTopRight,whatIsPlayer);

                if (hit != null)
                {
                    lastTouchDamageTime = Time.time;
                    attackDetails[0] = touchDamage;
                    attackDetails[1] = _alive.transform.position.x;
                    hit.SendMessage("Damage", attackDetails);
                }
            }
        }
        private void Damage()
        {
            _currentHp -= attackDetails[0];

            Instantiate(hitParticles, _alive.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f,360f)));

            if (attackDetails[1] > _alive.transform.position.x)
                _damageDir = -1;
            else
                _damageDir = 1;
            
            if (_currentHp > 0.0f)
                SwitchState(State.Knockback);
            else if(_currentHp <= 0.0f)
                SwitchState(State.Dead);
        
        }
        private void Flip()
        {
            _facingDir *= -1;
            _alive.transform.Rotate(0.0f, 180.0f, 0.0f);

        }

        private void SwitchState(State state)
        {
            switch (_currentState)
            {
                case State.Moving:
                    ExitMovingState();
                    break;
                case State.Knockback:
                    ExitKnockbackState();
                    break;
                case State.Dead:
                    ExitDeadState();
                    break;
            }

            switch (state)
            {
                case State.Moving:
                    EnterMovingState();
                    break;
                case State.Knockback:
                    EnterKnockbackState();
                    break;
                case State.Dead:
                    EnterDeadState();
                    break;
            }

            _currentState = state;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));

            Vector2 
                botLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2),touchDamageCheck.position.y - (touchDamageHeight / 2)), 
                botRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2),touchDamageCheck.position.y - (touchDamageHeight / 2)),
                topRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2),touchDamageCheck.position.y + (touchDamageHeight / 2)),
                topLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2),touchDamageCheck.position.y + (touchDamageHeight / 2));
            
            Gizmos.DrawLine(botLeft,botRight);
            Gizmos.DrawLine(botRight,topRight);
            Gizmos.DrawLine(topRight, topLeft);
            Gizmos.DrawLine(topLeft, botLeft);

        }
    }
}

