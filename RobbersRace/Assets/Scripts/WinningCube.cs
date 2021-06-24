using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningCube : MonoBehaviour
{

    public Transform lastNode;
    public GameObject roadAfterFinishline;
    public void ChangeColor(Color color)
    {
        roadAfterFinishline.GetComponent<Renderer>().material.color = color;
    }
   public Vector3 ReturnLastNode()
    {
        Transform parentObj=transform.parent;
        
        Vector3 localPointInNestedChild=lastNode.localPosition;

        Vector3 worldPoint = this.transform.TransformPoint(localPointInNestedChild);
        Vector3 localPointInParent = parentObj.InverseTransformPoint(worldPoint);

        return localPointInParent;

    }

}
