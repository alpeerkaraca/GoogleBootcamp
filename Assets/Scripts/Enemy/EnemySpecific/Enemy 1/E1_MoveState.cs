using Enemy.StateMach;

namespace Enemy.EnemySpecific.Enemy_1
{
 public class E1_MoveState : MoveState
 {
  private Enemy1 enemy; 
  public E1_MoveState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, MoveStateData stateData, Enemy1 enemy) : base(etity, stateMachine, animBoolName, stateData)
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

   if (isPlayerInMinAgroRange)
   {
    stateMachine.ChangeState(enemy.playerDetectedState);
   }
   else if (!isDetectingLedge)
   {
    enemy.IdleState.SetFlipAfterIdle(true);
    stateMachine.ChangeState(enemy.IdleState);
   }
  }
  public override void PhysicsUpdate()
  {
   base.PhysicsUpdate();
  }
 }
}
