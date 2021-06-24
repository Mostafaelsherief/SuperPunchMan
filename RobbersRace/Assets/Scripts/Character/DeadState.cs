using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{
    // Start is called before the first frame update
    public DeadState(Character character,StateMachine stateMachine):base(character,stateMachine)
    {


    }
    public override void Enter()
    {
        base.Enter();
       

        character.SetAnimationTrigger(character.GetComponent<Player>().deadParam);
        character.Stop();

        LeanTween.delayedCall(0.8f, character.GetComponent<Player>().ActivateDeathEffect);
        
    }
}
