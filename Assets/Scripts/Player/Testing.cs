using UnityEngine;

namespace Player
{
    public class Testing : MonoBehaviour
    { 
        [Header("Combat Settings")]
        [SerializeField] private bool combatEnabled;
        [SerializeField] private float inputTimer, 
            attackRadius,
            attack1Damage;
        [SerializeField] private Transform attack1HitboxPos;
        [SerializeField] private LayerMask whatIsDamagable;
        private bool _gotInput, _isAttacking, _isFirstAttack;
    
        private float _lastInputTime = Mathf.NegativeInfinity;

        private CharacterController charController;
        private Animator _anim;
        private static readonly int CanAttack = Animator.StringToHash("canAttack");
        private static readonly int Attack1 = Animator.StringToHash("attack1");
        private static readonly int FirstAttack = Animator.StringToHash("firstAttack");
        private static readonly int İsAttacking = Animator.StringToHash("isAttacking");
        private float[] attackDetails = new float[2];
        private Stats stats;
        private void Start()
        {
            _anim = GetComponent<Animator>();
            //_anim.SetBool(CanAttack, combatEnabled);
            charController = GetComponent<CharacterController>();
            stats = GetComponent<Stats>();
        }

        private void Update()
        {
            CheckCombatInput();
            CheckAttacks();
        }

        private void CheckCombatInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (combatEnabled)
                {
                    //AttempCombat
                    _gotInput = true;
                    _lastInputTime = Time.time;
                }
            }
        }

        private void CheckAttacks()
        {
            if (_gotInput)
            {
                //perform at1

                if (!_isAttacking)
                {
                    _gotInput = false;
                    _isAttacking = true;
                    _isFirstAttack = !_isFirstAttack;
                    _anim.SetBool(Attack1, true);
                    _anim.SetBool(FirstAttack, _isFirstAttack);
                    _anim.SetBool(İsAttacking, _isAttacking);
                }
            }

            if (Time.time >= _lastInputTime + inputTimer)
            {
                //Wait for new input
                _gotInput = false;
            }
        }

        private void CheckAttackHitBox()
        {
            attackDetails[0] = attack1Damage;
            attackDetails[1] = transform.position.x;
            
            Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitboxPos.position, attackRadius, whatIsDamagable);
            
            foreach (Collider2D col in detectedObjects )
            {
                col.transform.parent.SendMessage("Damage", attackDetails);
                //Instantıate hit particles
            }
        
        }

        private void FinishAttack1()
        {
            _isAttacking = false;
            _anim.SetBool(İsAttacking, _isAttacking);
            _anim.SetBool(Attack1, false);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(attack1HitboxPos.position, attackRadius);
        }

        private void Damage(float[] attackDetails)
        {
            if (!charController.GetDashStatus())    //Player won't take damage if dashing.
            {
                int direction;

                stats.DecreaseHP(attackDetails[0]);
            
                if (attackDetails[1] < transform.position.x)
                {
                    direction = 1;
                }
                else
                {
                    direction = -1;
                }
                charController.Knockback(direction);
            }
        }
    }
    
}
