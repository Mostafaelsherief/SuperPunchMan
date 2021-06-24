using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    public float angle;
    public LayerMask targetLayerMask;
    public float MeshResolution;
    public LayerMask obstacleMask;
    bool isPlayerKilled;
    public MeshFilter filter;
    Mesh mesh;
    bool playerWon;
    private void Start()
    {
        mesh = new Mesh();
        mesh.name = "ViewMesh";
        filter.mesh = mesh;
        GameEvents.currentEvent.gameWon += PlayerWon;
    }
    void PlayerWon()
    {
        playerWon = true;
    
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool isGlobal)
    {
        if (!isGlobal)
        {

            angleInDegrees += transform.rotation.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    float detectionTime = 0;

    void FindVisibleTargets()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, radius, targetLayerMask);
        if (targetsInViewRadius.Length != 0)
        {

            Transform target = targetsInViewRadius[0].transform;
            Vector3 directionToTarget = (target.transform.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distToTarget = Vector3.Distance(target.transform.position, transform.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distToTarget, obstacleMask) && !isPlayerKilled)
                {
                 //   detectionTime += Time.fixedDeltaTime;
                   // if (detectionTime >= 0.3f)
                    //{
                        GameEvents.currentEvent.KillPlayer(GetComponent<Guard>());
                        isPlayerKilled = true;
                    //}

                }
               // else detectionTime = 0;
            }
        }

    }

    void DrawLineRenderer()
    {
        ViewCastInfo viewCastInfo;
        int stepCount = (int)Mathf.Round(MeshResolution * angle);
        float stepAngleSize = angle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        for (int i = 0; i <= stepCount; i++)
        {
            float ang = transform.eulerAngles.y - angle / 2 + i * stepAngleSize;
            viewCastInfo = ReturnRaycastInfo(ang);
            viewPoints.Add(viewCastInfo.point);
        }
        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];
        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount-1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint( viewPoints[i]);
            if(i<vertexCount-2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    ViewCastInfo ReturnRaycastInfo(float globalAngle)
    {
        RaycastHit hit;
        Vector3 direction = DirFromAngle(globalAngle, true);
        if (Physics.Raycast(transform.position, direction, out hit, radius, obstacleMask)) 
        {
            return new ViewCastInfo(true, hit.distance, hit.point, globalAngle);
        }
        else return new ViewCastInfo(false, radius, transform.position+direction*radius, globalAngle);
    }


    private void LateUpdate()
    {

        FindVisibleTargets();
        if (!playerWon&&!isPlayerKilled)
            DrawLineRenderer();
        else mesh.Clear();
    }
    public struct ViewCastInfo
    {
      public  bool hit;
        public float dist;
        public Vector3 point;
        public float angle;
        public ViewCastInfo(bool _hit,float _dist,Vector3 _point, float _angle )
        {
            hit = _hit;
            dist = _dist;
            point = _point;
            angle = _angle;
        }
    }

}




