using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;


[CustomEditor (typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView fow = (FieldOfView)target;
        Handles.color = Color.black;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.radius);
        Vector3 boundAngleA = fow.DirFromAngle(fow.angle / 2, false);
        Vector3 boundAngleB = fow.DirFromAngle(-fow.angle / 2, false);
        Handles.DrawLine(fow.transform.position, fow.transform.position+ boundAngleA * fow.radius);
        Handles.DrawLine(fow.transform.position, fow.transform.position+ boundAngleB * fow.radius);
    }
}
