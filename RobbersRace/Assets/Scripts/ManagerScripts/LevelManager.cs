using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public int Levelno;
    public List<LevelData> levelData;
    public LevelData currentLevel;
    private void Awake()
    {
        instance = this;
        currentLevel= levelData[PlayerPrefs.GetInt("LevelNumber")];
        Levelno = currentLevel.Levelnumber;
        
    }

    public void IntializePlayerPrefs()
    {
        if (PlayerPrefs.GetInt("LevelNumber") <= 0)
            PlayerPrefs.SetInt("LevelNumber", 0);
        
    }
    public void UpgradeLevelNumber()
    {
        PlayerPrefs.SetInt("LevelNumber", PlayerPrefs.GetInt("LevelNumber") + 1);
    }
    
}

[Serializable]
public class LevelData 
{
    public int Levelnumber;
    public int randomSeed;
    public bool laser;
    public int rows;
    public int columns;
    public int numberOfGuards;
    public bool keyTrap;
}
