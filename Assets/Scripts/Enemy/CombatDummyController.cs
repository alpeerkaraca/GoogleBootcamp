using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class CombatDummyController : MonoBehaviour
    {
        [SerializeField] private float 
            maxHp,
            knockbackSpeedX,
            knockbackSpeedY,
            knockbackDuration,
            knockbackDeathSpeedX,
            knockbackDeathSpeedY,
            deathTorque;
        [SerializeField] private bool applyKnockback;

        [SerializeField] private GameObject hitParticle;

        private bool
            _playerOnLeft,
            _knockback;

        private float 
            _currentHp,
            _knockbackStart;

        private int _playerFacingDir;

        private CharacterController _pc;

        private GameObject
            _aliveGo,
            _brokenTopGo,
            _brokenBotGo;

        private Rigidbody2D
            _rbAlive,
            _rbBrokenTop,
            _rbBrokenBot;

        private Animator _aliveAnim;
        private static readonly int PlayerOnLeft = Animator.StringToHash("PlayerOnLeft");
        private static readonly int  DamageOnAnimator = Animator.StringToHash("Damage");

        private void Start()
        {
            _currentHp = maxHp; 

            _pc = GameObject.Find("Player").GetComponent<CharacterController>();

            _aliveGo = transform.Find("Alive").gameObject;
            _brokenTopGo = transform.Find("BrokenTop").gameObject;
            _brokenBotGo = transform.Find("BrokenBottom").gameObject;

            _aliveAnim = _aliveGo.GetComponent<Animator>();

            _rbAlive = _aliveGo.GetComponent<Rigidbody2D>();
            _rbBrokenBot = _brokenBotGo.GetComponent<Rigidbody2D>();
            _rbBrokenTop = _brokenTopGo.GetComponent<Rigidbody2D>();

            _aliveGo.SetActive(true);
            _brokenTopGo.SetActive(false);
            _brokenBotGo.SetActive(false);
        }

        private void Update()
        {
            CheckKnockback();
        }

        private void Damage(float[] details)
        {
            _currentHp -= details[0];
            if (details[1] < _aliveGo.transform.position.x)
            {
                _playerFacingDir = 1;
            }
            else
            {
                _playerFacingDir = -1;
            }

            Instantiate(hitParticle, _aliveGo.transform.position, quaternion.Euler(0f,0f,Random.Range(0f,360f)));

            if (_playerFacingDir == 1)
                _playerOnLeft = true;
            else
            {
                _playerOnLeft = false;
            }

            _aliveAnim.SetBool(PlayerOnLeft, _playerOnLeft);
            _aliveAnim.SetTrigger(DamageOnAnimator);

            if (applyKnockback && _currentHp > 0f)
            {
                Knockback();
            }
            
            if(_currentHp <=0f){
                Die();
            }

        }

        private void Knockback()
        {
            _knockback = true;
            _knockbackStart = Time.time;
            _rbAlive.velocity = new Vector2(knockbackSpeedX * _playerFacingDir, knockbackSpeedY);
        }

        private void CheckKnockback()
        {
            if (Time.time >= (_knockbackStart + knockbackDuration) && _knockback)
            {
                _knockback = false;
                _rbAlive.velocity = new Vector2(0f, _rbAlive.velocity.y);
            }
        }

        private void Die()
        {
            _aliveGo.SetActive(false);
            _brokenTopGo.SetActive(true);
            _brokenBotGo.SetActive(true);

            _brokenTopGo.transform.position = _aliveAnim.transform.position;
            _rbBrokenBot.transform.position = _aliveAnim.transform.position;

            _rbBrokenBot.velocity = new Vector2(knockbackSpeedX * _playerFacingDir, knockbackSpeedY);
            _rbBrokenTop.velocity = new Vector2(knockbackDeathSpeedX * _playerFacingDir, knockbackDeathSpeedY);
            _rbBrokenTop.AddTorque(deathTorque * -_playerFacingDir, ForceMode2D.Impulse);
        }
    }
}
