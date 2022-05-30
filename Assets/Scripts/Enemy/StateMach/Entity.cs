using System;
using Enemy.StateMach;
using Enemy.States.Data;
using UnityEngine;

public class Entity : MonoBehaviour

{
 public EntityData entityData;
 public int facingDirection { get; private set; }
 public FiniteStateMachine stateMachine;
 public Rigidbody2D rb { get; private set; }
 public Animator anim { get; private set; }
 public GameObject aliveGo { get; private set; }

 [SerializeField] private Transform ledgeCheck;
 [SerializeField] private Transform playerCheck;

 private Vector2 velocityWorkspace;

 public virtual void Start()
 {
  facingDirection = 1;
  aliveGo = transform.Find("Alive").gameObject;
  rb = aliveGo.GetComponent<Rigidbody2D>();
  anim = aliveGo.GetComponent<Animator>();
  stateMachine = new FiniteStateMachine();
 }
 public virtual void Update()
 {
   stateMachine.currentState.LogicUpdate();
 }

 public virtual void FixedUpdate()
 {
  stateMachine.currentState.PhysicsUpdate();
 }
 public virtual void SetVelocity(float velocity)
 {
  velocityWorkspace.Set(facingDirection * velocity, rb.velocity.y);
  rb.velocity = velocityWorkspace;
 }

 public virtual bool CheckLedge()
 {
  return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
 }

 public virtual void Flip()
 {
  facingDirection *= -1;
  aliveGo.transform.Rotate(0f,180f,0f);
 }

 public virtual void OnDrawGizmos()
 {
  Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));
 }
 public virtual bool CheckPlayerInMinAgroRange()
 {
  return Physics2D.Raycast(playerCheck.position, aliveGo.transform.right, entityData.minAgroDistance, entityData.whatIsPlayer);
 }

 public virtual bool CheckPlayerInMaxAgroRange()
 {
  return Physics2D.Raycast(playerCheck.position, aliveGo.transform.right, entityData.maxAgroDistance, entityData.whatIsPlayer);
 }
}
