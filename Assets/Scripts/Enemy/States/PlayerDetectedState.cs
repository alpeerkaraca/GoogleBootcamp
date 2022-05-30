using System.Collections;
using System.Collections.Generic;
using Enemy.StateMach;
using Enemy.States.Data;
using UnityEngine;

public class PlayerDetectedState : State
{
    // Start is called before the first frame update
    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;
    
    protected PlayerDetectedData stateData;
    
    public PlayerDetectedState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, PlayerDetectedData stateData) : base(etity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }
    public override void Enter()
    {
        base.Enter();
        entity.SetVelocity(0f);
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
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
        
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange(); 
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();

    }
}
