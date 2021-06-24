using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected Character character;
    protected StateMachine stateMachine;
    public State(Character character, StateMachine stateMachine)
    {
        this.character = character;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() 
    { }
    public virtual void UpdateLogic() 
    { }
    public virtual void HandleInput()
    { }
    public virtual void PhysicsUpdate()
    { }
    public virtual void Exit()
    { }
}
