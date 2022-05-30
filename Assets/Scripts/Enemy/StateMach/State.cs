using System.Collections;
using System.Collections.Generic;
using Enemy.StateMach;
using UnityEngine;

public abstract class State
{
    protected string animBoolName;
    
    protected FiniteStateMachine stateMachine;
    protected Entity entity;

    protected float startTime;


    public State(Entity etity, FiniteStateMachine stateMachine,string animBoolName)
    {
        this.entity = etity;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        entity.anim.SetBool(animBoolName, true);
    }

    public virtual void Exit()
    {
        entity.anim.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate()
    {
        
    }

    public virtual void PhysicsUpdate()
    {
        
    }


}
