using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectState : State
{

   public  PlayerDetectState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
        
    }

    public override void Enter()
    {
        // GameEvents.currentEvent.PlayerCaught();
        character.SetAnimationTrigger(character.GetComponent<Guard>().caughtParam);
        AudioManager.Instance.Play("Gunshot");
        character.LookAt(character.GetComponent<Guard>().player.transform.position);
        base.Enter();
        character.Stop();
    }

}
