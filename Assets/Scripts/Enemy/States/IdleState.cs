using Enemy.StateMach;
using UnityEngine;

public class IdleState : State
{
 protected IdleStateData stateData;

 protected float idleTime;

 protected bool flipAfterIdle;
 protected bool isIdleTimeOver;
 protected bool isPlayerInMinAgroRange;

 public IdleState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, IdleStateData stateData) : base(etity, stateMachine, animBoolName)
 {
  this.stateData = stateData;
 }
 public override void Enter()
 {
  base.Enter(); 
  
  entity.SetVelocity(0f);
  isIdleTimeOver = false;
  SetRandomIdleTime();
  isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
 }
 public override void Exit()
 {
  base.Exit();

  if (flipAfterIdle)
  {
   entity.Flip(); 
  }
 }
 public override void LogicUpdate()
 {
  base.LogicUpdate();
  if (Time.time >= startTime + idleTime)
  {
   isIdleTimeOver = true;
  }
 }
 public override void PhysicsUpdate()
 {
  base.PhysicsUpdate();
  isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
 }

 public void SetFlipAfterIdle(bool flip)
 {
  flipAfterIdle = flip;
 }
 private void SetRandomIdleTime()
 {
  idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
 }
}
