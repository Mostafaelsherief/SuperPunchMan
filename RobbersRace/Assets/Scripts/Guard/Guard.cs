using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : Character
{
    #region Variables
    public GuardPatrolingState guardPatrollingState;
    public GuardKilledState guardKilledState;
    public PlayerDetectState playerDetectState;
    public GameObject deathEffect;
    public GameObject spine;
    //Temporary
    public List<Vector2> targetPositions = new List<Vector2>();

    public GameObject respawnDisk;
    public float guardWaitTime;
    public bool isAlive;
    public Player player;
    public int caughtParam = Animator.StringToHash("PlayerCaught");
    

    public float guardScaleFactor;
    Vector3 guardScale;
    #endregion
    public override void Start()
    {
        base.Start();
        transform.localScale = transform.localScale * guardScaleFactor;
    
        CharacterSM = new StateMachine();
        player = FindObjectOfType<Player>();
        //SetCharacterDestination(currentTarget.transform.position);
     
        DetermineNextPosition();
        guardPatrollingState = new GuardPatrolingState(this, CharacterSM);
        guardKilledState = new GuardKilledState(this, CharacterSM);
        playerDetectState = new PlayerDetectState(this, CharacterSM);
        CharacterSM.Intialize(guardPatrollingState);
    }
    public void DetermineNextPosition()
    {
        int randomPos = 0;
        int preventRuntimeErrorIterator = 0;
        Vector3 nextTarget = transform.position;
        while (Vector3.Magnitude(transform.position - nextTarget) < 6 || !(preventRuntimeErrorIterator > 10))
        {
            preventRuntimeErrorIterator++;
            randomPos = Random.Range(1, targetPositions.Count - 1);
            nextTarget = new Vector3(targetPositions[randomPos].x, transform.position.y, targetPositions[randomPos].y);
        }
        SetCharacterDestination(nextTarget);
    }
    public void GoToPlayerDetectPosition(Vector3 position)
    {
        int preventRuntimeErrorIterator = 0;
        int randomPos = 0;
        Vector3 nextTarget = transform.position;
        while (Vector3.Magnitude(position - nextTarget) > 5 || !(preventRuntimeErrorIterator > 10))
        {
            preventRuntimeErrorIterator++;
            randomPos = Random.Range(0, targetPositions.Count - 1);
            nextTarget = new Vector3(targetPositions[randomPos].x, transform.position.y, targetPositions[randomPos].y);
        }
        SetCharacterDestination(nextTarget);
    }
    public void DeathEffect()
    {
        guardScale = transform.localScale;
        transform.GetComponent<FieldOfView>().enabled = false;
        isAlive = false;
        LeanTween.delayedCall(0.2f, DeactivateGameobject);
        LeanTween.delayedCall(0.1f, PlayHitSound);
        Instantiate(deathEffect, transform.position, deathEffect.transform.rotation);
    }
    void PlayHitSound()
    {
        AudioManager.Instance.Play("EnemyHit");
    }
    void DeactivateGameobject()
    {
        AudioManager.Instance.Play("Disappear");
        gameObject.SetActive(false);
    }

    
  
    
   
}
