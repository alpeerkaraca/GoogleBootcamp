using System.Collections;
using System.Collections.Generic;
using Enemy.EnemySpecific.Enemy_1;
using Enemy.States.Data;
using UnityEngine;

public class Enemy1 : Entity
{
 public E1_IdleState IdleState { get; private set; }
 public E1_MoveState moveState { get; private set; }

 public E1_PlayerDetectedState playerDetectedState { get;  private set; }
 [SerializeField] private IdleStateData IdleStateData;
 [SerializeField] private MoveStateData MoveStateData;
 [SerializeField] private PlayerDetectedData playerDetectedData;
 public override void Start()
 {
  base.Start();
  IdleState = new E1_IdleState(this, stateMachine, "idle", IdleStateData,this);
  moveState = new E1_MoveState(this, stateMachine, "move", MoveStateData, this);
  playerDetectedState = new E1_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
  
  stateMachine.Initialize(moveState);
 }
}
