using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class Player : Character
{
    public MovingState movingState;
    public HitLaserState hitLaserState;
    public DanceState winState;
    public GameObject TR;
    public GameObject playerDeathEffect;
    public Vector3 winNode;
    public Material playerMaterial;
    public LayerMask ground;
   // public SkinnedMeshRenderer playerRenderer;
    public DeadState deadState;
    public GameObject currentHitTarget;

    public int deadParam = Animator.StringToHash("PlayerCaught");
    public int danceParam = Animator.StringToHash("Dance");
    public int punchParam = Animator.StringToHash("Punch");
    public override void Start()
    {
        base.Start();
        TR = GameObject.FindGameObjectWithTag("XMark");
        CharacterSM = new StateMachine();
        hitLaserState = new HitLaserState(this, CharacterSM);
        winState = new DanceState(this, CharacterSM);
        movingState = new MovingState(this, CharacterSM);
        deadState = new DeadState(this, CharacterSM);
        CharacterSM.Intialize(movingState);
        //winNode= LevelManager.instance.levelUtility.lastNode.localPosition;
    }
    private void OnTriggerEnter(Collider other)
    {
        currentHitTarget = other.gameObject;
    }
    public GameObject flyDisk;
    public void ActivateFlyDisk()
    {

        Instantiate(flyDisk, transform.position, Quaternion.identity);
        }
   public void DelayActivateFlyDiskCall()
    {

        LeanTween.delayedCall(0.8f, ActivateFlyDisk);
    
    }
    public void ActivateDeathEffect()
    {
        AudioManager.Instance.Play("Disappear");
    //    Instantiate(playerDeathEffect, transform.position+Vector3.up*2f, Quaternion.identity);
        transform.gameObject.SetActive(false);
    }


}
