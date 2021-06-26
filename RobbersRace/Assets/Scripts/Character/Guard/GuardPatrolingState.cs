using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardPatrolingState : State
{
    public GuardPatrolingState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    { }
    public override void Enter()
    {
        base.Enter();

        if (character.currentTarget != null)
            character.Move();
        character.SetAgentSpeed(character.topSpeed * Random.Range(0.4f, 0.8f));
        character = character.GetComponent<Guard>();
        GameEvents.currentEvent.killPlayer += ChangeState;
        GameEvents.currentEvent.killEnemy += KillEnemy;
        GameEvents.currentEvent.playerDetectPosition +=AlertGuard ;
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
  

    }
    public void AlertGuard(Vector3 LaserPosition)
    {
        character.SetAgentSpeed(1.6f * character.topSpeed);
        character.GetComponent<Guard>().GoToPlayerDetectPosition(LaserPosition);
    }

    public void KillEnemy(int Id)
    {
        Debug.Log("Checking ID");
        
    if(Id==character.gameObject.GetInstanceID())
        {
            Debug.Log("ItsTheSameID");
            
            stateMachine.ChangeState(character.GetComponent<Guard>().guardKilledState);
        }    
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (character.ReachedTarget())
        {
            character.SetAgentSpeed(character.topSpeed*Random.Range(0.4f,1.2f));
            character.GetComponent<Guard>().DetermineNextPosition();
        }
        if (character.currentTarget != null)
            AdjustSpeed();
    }
    public void AdjustSpeed()
    {
        Vector3 node2 = character.currentTarget.transform.position;
        Vector3 node1 = character.currentTarget.previousNode.transform.position;
        Vector3 midPoint = (node2 + node1) / 2;
        if (Vector3.Magnitude(character.transform.position - node2) > Vector3.Magnitude(midPoint - node2))
            return;
        else 
        {
            float currentSpeedCoefficient = Vector3.Magnitude(node2 - character.transform.position) / Vector3.Magnitude(node2 - midPoint);
            float currentSpeed = currentSpeedCoefficient * character.topSpeed;
            float minimumSpeedThreshold = 2.4f;
            if(currentSpeed>minimumSpeedThreshold)
                character.SetAgentSpeed(currentSpeedCoefficient * character.topSpeed);
            else
                character.SetAgentSpeed(minimumSpeedThreshold);
        }


    }
    void ChangeState(Guard guard)
    {
        if (guard == character.GetComponent<Guard>())
            stateMachine.ChangeState(character.GetComponent<Guard>().playerDetectState);
        else if(character.gameObject.activeSelf) character.Stop();
    }
    public override void Exit()
    {
        base.Exit();
        GameEvents.currentEvent.killEnemy -= KillEnemy;

    }
}
