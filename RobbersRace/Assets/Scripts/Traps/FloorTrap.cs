using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrap : TriggerableObject
{
    float trapTime = 1f;
    float stopTime=3f;
    public GameObject elec;
    bool stopElec=false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Floortrap(trapTime));
    }
    IEnumerator Floortrap(float trapTime)
    {
        float startTime = Time.time;
        while (Time.time - startTime < trapTime)
        {
            yield return null;

        }
        if (!stopElec)
        {
            AlternateElectricty(elec);
            if(elec.activeSelf)
            StartCoroutine(Floortrap(this.trapTime));
            else StartCoroutine(Floortrap(stopTime));

        }
        else elec.SetActive(false);
    }
    public override void TriggerEnter()
    {
        base.TriggerEnter();
        elec.SetActive(false);
        AudioManager.Instance.Play("Zap");
        stopElec = true;
        GameEvents.currentEvent.KillPlayer(new Guard());

    }
    void AlternateElectricty(GameObject elec)
    {
        if (elec.activeSelf)
            elec.SetActive(false);
        else elec.SetActive(true);
    }

    
}
