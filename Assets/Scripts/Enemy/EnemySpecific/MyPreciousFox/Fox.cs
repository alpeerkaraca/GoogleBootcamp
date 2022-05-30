using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : Entity
{
 public Fox_IdleState IdleState { get; private set; }

 public Fox_MoveState moveState { get; private set; }

 [SerializeField] private IdleStateData IdleStateData;
 [SerializeField] private MoveStateData MoveStateData;

 public override void Start()
 {
  base.Start();
  IdleState = new Fox_IdleState(this, stateMachine, "idle", IdleStateData, this);
  moveState = new Fox_MoveState(this, stateMachine, "move", MoveStateData, this);
  stateMachine.Initialize(moveState);
 }

}
 
