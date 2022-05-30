using System.Collections;
using System.Collections.Generic;
using Enemy.StateMach;
using UnityEngine;

public class MoveState : State
{
 protected MoveStateData stateData;
 protected bool isDetectingLedge;

 protected bool isPlayerInMinAgroRange;

 public MoveState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, MoveStateData stateData) : base(etity, stateMachine, animBoolName)
 {
  this.stateData = stateData;
 }

 public override void Enter()
 {
  base.Enter();
  entity.SetVelocity(stateData.movementSpeed);

  isDetectingLedge = entity.CheckLedge();

  isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
 }
 public override void Exit()
 {
  base.Exit();
 }
 public override void LogicUpdate()
 {
  base.LogicUpdate();
 }
 public override void PhysicsUpdate()
 {
  base.PhysicsUpdate();
  isDetectingLedge = entity.CheckLedge();
  isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
 }


}
