using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceState : State
{
    
    // Start is called before the first frame update
    public DanceState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
    
    }
    public override void Enter()
    {
        base.Enter();
     character.GetComponent<Player>().SetAnimationTrigger(character.GetComponent<Player>().danceParam);
    
    }
    public override void HandleInput()
    {
        base.HandleInput();
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
       
    }
    public override void Exit()
    {
        base.Exit();

    }
}
