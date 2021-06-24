using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{

    public static GameEvents currentEvent;


    private void Awake()
    {
        currentEvent = this;
    }
    public event Action playerCaught;
    public event Action gameWon;
    public event Action openMenu;
    public event Action playerHitLaser;
    public event Action<int> killEnemy;
    public event Action openDoor;
    public event Action hideTutorial;
    public event Action <Guard>killPlayer;
    public event Action loadNextLevel;
    public event Action restartLevel;
    public event Action<Vector3> playerDetectPosition;
    public void OpenDoor()
    {
        openDoor.Invoke();
    }  
    public void HideTutorial()
    {

        hideTutorial.Invoke();
    
    }
    public void PlayerDetected(Vector3 pos)
    {
        playerDetectPosition.Invoke(pos);
    }
    public void KillEnemy(int id )
    {

        killEnemy(id);
    
    }
    public void OpenMenu()
    {
        if (gameWon != null)
        {
            gameWon.Invoke();
        }
    }

    public void PlayerHitLaser()
    {
        if (playerHitLaser != null)
        {
            playerHitLaser.Invoke();
            playerCaught.Invoke();
        }
    }
    public void KillPlayer(Guard guard)
    {
        if (killPlayer != null)
        {
            killPlayer.Invoke(guard);
            playerCaught.Invoke();
        }
    }
    public void RestartGame()
    {
        if (restartLevel != null)
        {
            restartLevel.Invoke();
        }
    }
    public void LoadNextLevel()
    {
        if (loadNextLevel != null)
        {
            loadNextLevel.Invoke();
        }
    }
    public void GameWon()
    {
        if(gameWon!=null)
        gameWon.Invoke();
    }

    

}
