using System;
using UnityEngine;

namespace Enemy.EnemySpecific.MyPreciousFox
{
    public class FollowPlayer : MonoBehaviour
    {
        public bool petted;
        [SerializeField] private float 
        speed = 5f;
        private Transform playerPos;
        private CharacterController characterController;
        private Animator anim;
        private static readonly int Move = Animator.StringToHash("move");
        private static readonly int Idle = Animator.StringToHash("idle");
        private void Start()
        {
            anim = this.GetComponentInChildren<Animator>();
            playerPos = GameObject.Find("follow").GetComponent<Transform>();
            characterController = GameObject.Find("Player").GetComponent<CharacterController>();
            anim.SetBool(Move,false);
            anim.SetBool(Idle, true);
        }
        private void Update()
        {
            if (petted)
            {
                CheckAnims();
                FollowUp();
                transform.rotation = playerPos.rotation;
            }
        }

        private void FollowUp()
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
        }

        private void CheckAnims()
        {
            if (Math.Abs(characterController._horizontalMove)> 0f)
            {
                anim.SetBool(Move, true);
                anim.SetBool(Idle, false);
            }
            else
            {
                anim.SetBool(Move, false);
                anim.SetBool(Idle, true);
            }
        }
    


    }
}
