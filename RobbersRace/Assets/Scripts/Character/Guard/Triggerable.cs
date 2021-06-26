using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggerable : MonoBehaviour
{
    public bool triggered;
    public TriggerableObject triggerable;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {


            triggered = true;
            triggerable.TriggerEnter();
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            triggerable.TriggerExit();
        }
    }
}

