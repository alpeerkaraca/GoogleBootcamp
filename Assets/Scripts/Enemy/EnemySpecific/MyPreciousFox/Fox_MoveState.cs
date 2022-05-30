using Enemy.StateMach;

public class Fox_MoveState : MoveState
{
    private Fox fox;
    

    public Fox_MoveState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, MoveStateData stateData, Fox fox) : base(etity, stateMachine, animBoolName, stateData)
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

        if (!isDetectingLedge)
        {
            fox.IdleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(fox.IdleState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
