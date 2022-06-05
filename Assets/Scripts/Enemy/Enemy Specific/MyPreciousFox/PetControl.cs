using System;
using Player;
using UnityEngine;
using CharacterController = Player.CharacterController;

namespace Enemy.Enemy_Specific.MyPreciousFox
{
    public class PetControl : MonoBehaviour
    {
        public bool petted;
        [SerializeField] private float 
        
        speed = 5f;
        private Transform followBird, followBee;
        private CharacterController characterController;
        private Animator anim;
        private Stats stat;
        private static readonly int Move = Animator.StringToHash("move");
        private static readonly int Idle = Animator.StringToHash("idle");

        [SerializeField] private bool
            isBird,
            isBee;
        
        private void Start()
        {
            anim = this.GetComponentInChildren<Animator>();
            stat = GameObject.FindWithTag("Player").GetComponent<Stats>();
            characterController = GameObject.FindWithTag("Player").GetComponent<CharacterController>();
            anim.SetBool(Move,false);
            anim.SetBool(Idle, true);
        }
        private void Update()
        {
            if (GameObject.FindWithTag("Player") != null)
            {
                followBird = GameObject.Find("BirdFollowPosition").transform;
                followBee = GameObject.Find("BeeFollowPosition").transform;
                transform.rotation = GameObject.FindWithTag("Player").transform.rotation;
                if (petted && isBird)
                {
                    CheckAndApplyTransformPosition();
                    CheckAnims();

                    if (stat.GetDeadStatus())
                    {
                        transform.position = followBird.position;
                    }
                }
                else if (petted && isBee)
                {
                    anim.SetBool(Move, true);
                    if(followBee != null)
                        transform.position = Vector2.MoveTowards(transform.position, followBee.position, speed * Time.deltaTime);
                }
            }
        }
        

        private void CheckAnims()
        {
            if (GameObject.FindWithTag("Player") != null)
            {
                if (isBird)
                {
                    if (Math.Abs(characterController.horizontalMove) > 0f)
                    {
                        anim.SetBool(Move, true);
                        anim.SetBool(Idle, false);
                    }
                    else
                    {
                        anim.SetBool(Move, true);
                    }
                }
            }
        }
        

        private void CheckAndApplyTransformPosition() {
            if (GameObject.FindWithTag("Player") != null)
            {
                if (isBird && followBird != null)
                    transform.position = Vector2.MoveTowards(transform.position, followBird.position, speed * Time.deltaTime);
                if (isBee && followBee != null)
                {
                    transform.position = Vector2.MoveTowards(transform.position, followBird.position, speed * Time.deltaTime);
                }
            }

        }


        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
                petted = true;
        }

    }
}
