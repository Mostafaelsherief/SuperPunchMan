using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
public class MovingState : State 
{
    bool gameWonState;
    float lastClickTime;
    float radius = 3;
    Material playerMaterial;
    
    // Start is called before the first frame update
    public MovingState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {

    }


    public override void Enter()
    {
        base.Enter();
        playerMaterial = character.GetComponent<Player>().playerMaterial;
        GameEvents.currentEvent.killPlayer += DeadState;
        GameEvents.currentEvent.gameWon += WinState;
        GameEvents.currentEvent.playerCaught += DisableRenderedPath;
        GameEvents.currentEvent.killEnemy += HitEnemy;

    }
    void Sneak()
    {
        character.SetAgentSpeed(5);
    }

    void Run()
    {
        character.SetAgentSpeed(character.topSpeed);
    }

    bool IsWallNearby()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(character.transform.position, radius);

        foreach (Collider col in targetsInViewRadius)
            if (col.tag == "Wall")
                return true;
        
        return false;
    }

    float t = 0;
    public override void HandleInput()
    {
        base.HandleInput();
        /*
        if (IsWallNearby())
            Sneak();
        else Run();
        */
        t+=Time.fixedDeltaTime;

        if (Input.GetMouseButton(0)&&!gameWonState&&t>0.1f)
        {
            t = 0;
            
            Debug.Log("Time Difference = "+ (Time.time - lastClickTime));
            
            lastClickTime = Time.time;
            
            character.Move();
            character.GetComponent<Player>().TR.SetActive(true);
            Ray ray = MazeSpawner.instance.vmCam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (Vector3.Magnitude(hit.point-character.transform.position) >4f)
                    Run();
                else Sneak();
                character.SetCharacterDestination(hit.point);
                TargetMarkEffect(character.GetComponent<Player>().TR);
            }
        }
    }
    void HitEnemy(int id)
    {
      
        //  character.LookAt(character.GetComponent<Player>().currentHitTarget.transform.position);
        AudioManager.Instance.Play("PlayerPunch");

        character.SetAnimationTrigger(character.GetComponent<Player>().punchParam);
    }
    void TargetMarkEffect(GameObject obj)
    {
        character.GetComponent<Player>().TR.transform.position =character.AgentTargetDestination();
        obj.transform.localScale = Vector3.zero;
        LeanTween.scale(obj, Vector3.one * 0.6f, Random.Range(0.1f,0.4f));
    }
    void WinState()
    {

        gameWonState = true ;
        
        Debug.Log(character.GetComponent<Player>().winNode);
        character.SetCharacterDestination(character.GetComponent<Player>().winNode);
        Debug.Log(character.AgentTargetDestination());
        DisableRenderedPath();
        character.GetComponent<NavMeshAgent>().enabled = false;
        character.GetComponent<Player>().TR.SetActive(false);
        character.GetComponent<Player>().DelayActivateFlyDiskCall();


        LeanTween.delayedCall(1f, FlyHigh);
    }
    void FlyLow()
    {
        LeanTween.moveY(character.gameObject, character.transform.position.y + 5, 1f).setEaseInOutQuart();
    }
    void FlyHigh()
    {
        AudioManager.Instance.Play("PlayerFly");

        LeanTween.moveY(character.gameObject, character.transform.position.y + 50, 2f).setEaseInOutQuart();
    }
    
    void DisableRenderedPath()
    {
        character.GetComponent<LineRenderer>().enabled = false;
        character.GetComponent<Player>().TR.SetActive(false);
    }
    void DeadState(Guard guard)
    {
       
        stateMachine.ChangeState(character.GetComponent<Player>().deadState);
    }
    void HitLaserState() 
    {        
        stateMachine.ChangeState(character.GetComponent<Player>().hitLaserState);
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (character.ReachedTarget())
        {
           
            character.Stop();
            character.GetComponent<Player>().TR.SetActive(false);
        }
    }
    public override void Exit()
    {
        base.Exit();
        GameEvents.currentEvent.killPlayer -= DeadState;
        GameEvents.currentEvent.gameWon -= WinState;
        GameEvents.currentEvent.playerCaught -= DisableRenderedPath;

    }
}
