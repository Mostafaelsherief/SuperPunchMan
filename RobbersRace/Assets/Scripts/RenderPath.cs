using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RenderPath : MonoBehaviour
{
    LineRenderer line;
    Transform target;
    NavMeshAgent agent;
    // Start is called before the first frame update
    private void Start()
    {
        line= GetComponent<LineRenderer>();
        agent = GetComponent<NavMeshAgent>();
        
    }
    void DrawPath(NavMeshAgent agent)
    {
        NavMeshPath path;
        path = agent.path;
        if (path.corners.Length < 2)
            return;
        line.SetVertexCount(path.corners.Length);
        line.SetPositions(path.corners);
      

    }
    float t = 0;
    // Update is called once per frame
    void Update()
    {
   
            DrawPath(agent);
        
    }
}
