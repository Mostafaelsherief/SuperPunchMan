using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitLaserState : State
{
    // Start is called before the first frame update
    public HitLaserState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
    
    }
    public override void Enter()
    {
        base.Enter();
        character.Stop();
    }
    // Update is called once per frame
}
