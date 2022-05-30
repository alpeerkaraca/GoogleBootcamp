using System.Collections;
using System.Collections.Generic;
using Enemy.StateMach;
using UnityEngine;

public class Fox_IdleState : IdleState
{
 private Fox fox;

 public Fox_IdleState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, IdleStateData stateData, Fox fox) : base(etity, stateMachine, animBoolName, stateData)
 {
  this.fox = fox;
 }
 public override void Enter()
 {
  base.Enter();
 }
 public override void Exit()
 {
  base.Exit();
 }
 public override void LogicUpdate()
 {
  base.LogicUpdate();
  if (isIdleTimeOver)
  {
   stateMachine.ChangeState(fox.moveState);
  }
 }
 public override void PhysicsUpdate()
 {
  base.PhysicsUpdate();
 }
}
