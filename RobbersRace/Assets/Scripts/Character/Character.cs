using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal.Internal;
/*
 Character Class:
Gu
 */
public class Character : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;
    public PathNode currentTarget=new PathNode();
    StateMachine characterSM;
    public  float topSpeed;
    public StateMachine CharacterSM;

    #region Methods

    public void Move()
    {
        agent.isStopped = false;
    }
    public void SetAgentSpeed(float speed)
    {
        agent.speed = speed;
    }
    public void LookAt(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = lookRotation;
    }
    float previousRotation;
    float DeltaRotation()
    {
        float deltarotation = transform.localRotation.eulerAngles.y - previousRotation;
        previousRotation = transform.localRotation.eulerAngles.y;
        return deltarotation;
    }

    void AssignVelocitiesToAnimationParameters()
    {
        float currentDeltaRotation = DeltaRotation();
         if (Mathf.Abs(currentDeltaRotation) < 2f)
            currentDeltaRotation = 0;
        if (Mathf.Abs(currentDeltaRotation) > 5)
            currentDeltaRotation = 5;
        
        animator.SetFloat("SpeedX", currentDeltaRotation);
        animator.SetFloat("SpeedZ", agent.velocity.magnitude);
    }

    public void SetAnimationTrigger(int animID)
    {
        animator.SetTrigger(animID);
    }
    public void SetAnimationBool(int animID, bool condition)
    {
        animator.SetBool(animID, condition);
    }
    public bool ReachedTarget()
    {
        Vector2 currentPos = new Vector2(transform.position.x, transform.position.z);
        Vector2 targetPos = new Vector2(agent.destination.x, agent.destination.z);
        if ((targetPos - currentPos).magnitude < 0.02f)
        {
            return true;
        }
        else return false;
    }
    public Vector3 AgentTargetDestination()
    {
        return agent.destination;
    }
    
    public void SetCharacterDestination(Vector3 position)
    {
        if(gameObject.activeSelf)
        agent.SetDestination(position);
    }
    public void Stop()
    {
        if (gameObject.activeSelf)
        {
            agent.velocity = Vector3.zero;
            agent.isStopped = true;
        }
    }
    #endregion

    #region Monobehaviour Callbacks


    float timeNow;
    float delay=0.5f;
    public virtual void Start()
    {
        //temp 
        timeNow = Time.time;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
                topSpeed = agent.speed;

    }
    // Update is called once per frame
    void Update()
    {
        if (CharacterSM != null)
        {
            AssignVelocitiesToAnimationParameters();
            CharacterSM.currentState.HandleInput();
        }
            if (Time.time - timeNow > delay)
        {
            timeNow = Time.time;
          
            if (CharacterSM != null)
            {
                CharacterSM.currentState.UpdateLogic();
            }
            else Debug.LogError("State Machine not assigned");
        }
    }
    private void FixedUpdate()
    {
      
            if (CharacterSM != null)
                CharacterSM.currentState.PhysicsUpdate();
            else Debug.LogError("State Machine not assigned");
        
    }
    #endregion
}
