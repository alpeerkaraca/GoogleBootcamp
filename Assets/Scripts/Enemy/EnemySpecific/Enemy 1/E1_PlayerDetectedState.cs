using System.Collections;
using System.Collections.Generic;
using Enemy.StateMach;
using Enemy.States.Data;
using UnityEngine;

public class E1_PlayerDetectedState : PlayerDetectedState
{
    private Enemy1 enemy;
    public E1_PlayerDetectedState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, PlayerDetectedData stateData, Enemy1 enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
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
        if (!isPlayerInMaxAgroRange)
        {
            enemy.IdleState.SetFlipAfterIdle(false);
            stateMachine.ChangeState(enemy.IdleState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
