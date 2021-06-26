using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GuardKilledState : State
{
    public GuardKilledState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    { }
    public override void UpdateLogic()

    {
        base.UpdateLogic();
        if (character.GetComponent<Guard>().isAlive)
            stateMachine.ChangeState(character.GetComponent<Guard>().guardPatrollingState);
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Sir I AM Dead");
        character.GetComponent<Guard>().DeathEffect();
        
       
    }
}
