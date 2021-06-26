using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)

        {
            Debug.Log("LaserISTriggered");
            GameEvents.currentEvent.PlayerDetected(transform.position);
            AudioManager.Instance.Play("Siren");
        }
    }

    // Update is called once per frame
}
