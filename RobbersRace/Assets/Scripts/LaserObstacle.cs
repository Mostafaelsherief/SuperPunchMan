using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserObstacle : TriggerableObject
{

    public GameObject lasers;
    bool stopLaser = false;
    public float time=2f;
    public float startDelay;
    Action laserStart;
    private void Start()
    {        
        laserStart += LaserStart;
        LeanTween.delayedCall(startDelay, laserStart);
    }
    void LaserStart()
    {
        StartCoroutine(LaserCountDown(time));
    }
    IEnumerator LaserCountDown(float time)
    {
        float timeNow = Time.fixedTime;
        while (Time.fixedTime - timeNow < time)
        {
            yield return null;
        }
            ChangeCurrentActivation(lasers);
            StartCoroutine(LaserCountDown(time));
      
    }
    void ChangeCurrentActivation(GameObject obj)
    {
        if (obj.activeSelf)
            obj.SetActive(false);
        else obj.SetActive(true);
    }
    

}
