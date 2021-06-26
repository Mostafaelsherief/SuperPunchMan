using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using LionStudios;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject VmCam;
    public Vector3 characterScale;
    public GameObject winMenu;

    public Image soundIcon;
    public Sprite[] soundSprites;

    public GameObject Tutorial;
    public GameObject loseMenu;


    public Material[] groundMaterials;
    public Color[] BackgroundColors;
    public GameObject background;
    public GameObject[] floors;
    public TextMeshProUGUI keyUIText;
    public TextMeshProUGUI encouragingText;
    public TextMeshProUGUI levelNumberText;
    
    public  CinemachineVirtualCamera VirtualCamera;

    public int noOfKeys;
    //temp should be in a CameraControllerClass
    //for Camera Pos Lerp
    float initCameraPosY;
    float finalCameraPosY;

    float initCameraPosX;
    float finalCameraPosX;

    float initScreenXYBiasValue = 0.3f;
    float finalScreenXYBiasValue = 0.7f;
    
    float initScreenXBiasValue = 0.4f;
    float finalScreenXBiasValue = 0.8f;
    

    string[] encouragingWords=new string[11] {"GOOD JOB","AWESOME","AMAZING","SUPERB","MAGNIFICIENT","EXCELLENT","BRILLIANT","FABULOUS","IMPRESSIVE","MARVELOUS","MIRACULOUS"};

    //Temp Bad Coded
    int currentNoOfkeys;
   public int currentLevelno;

  public  bool adWillbeActivated;

    // Start is called before the first frame update
    void Awake()
    {
        
        encouragingText.text = encouragingWords[Random.Range(0, encouragingWords.Length - 1)]+" !!";
        Time.timeScale = 0.1f;
        Application.targetFrameRate = 60;
        instance = this;
        if (PlayerPrefs.GetInt("LevelNumber") <= 1)
        {
            LeanTween.delayedCall(0.8f, ActivateTutorial);        
        }
        StartCoroutine(StartupDelay());
   //     PlayerPrefs.SetInt("LevelNumber", currentLevelno);
        VmCam = FindObjectOfType<CinemachineVirtualCamera>().gameObject;
    }
    void ActivateTutorial()
    { Time.timeScale = 0;
        Tutorial.SetActive(true);
    } 
    void DeactivateTutorial()
    {
        Tutorial.SetActive(false);
    }

    IEnumerator StartupDelay()
    {
        yield return new WaitForSecondsRealtime(1);
        if (!adWillbeActivated)

            Time.timeScale = 1;
    }
    IEnumerator DisableCamera(float time)
    {
        yield return new WaitForSeconds(time);
        VmCam.gameObject.SetActive(false);

    }
    private void Start()
    {
        initCameraPosY = -3;
        finalCameraPosY = LevelManager.instance.currentLevel.rows*5-3;
        initCameraPosX = 0;
        finalCameraPosX = LevelManager.instance.currentLevel.columns * 5;


        levelNumberText.text ="LEVEL "+ LevelManager.instance.Levelno.ToString();
        GameEvents.currentEvent.gameWon += CameraDisable;
        GameEvents.currentEvent.restartLevel += RestartLevel;
        GameEvents.currentEvent.hideTutorial += DeactivateTutorial;

        keyUIText.text = currentNoOfkeys.ToString() + "/" + noOfKeys.ToString();

        GameEvents.currentEvent.loadNextLevel += LoadNextLevel;
        GameEvents.currentEvent.gameWon += StartWinMenu;
        GameEvents.currentEvent.playerCaught += StartLoseMenu;
       
        VirtualCamera = VmCam.GetComponent<CinemachineVirtualCamera>();
        
    }
    bool once = false;
    void CheckIfGuardsAlive()
    {
        Guard[] guards = FindObjectsOfType<Guard>();
        foreach (Guard guard in guards)
            if (guard.gameObject.activeSelf)
                return;
        if(!once)
        {
            once = true;
            GameEvents.currentEvent.GameWon();
        }


    }
    void OnApplicationFocus(bool pauseStatus)
    {
        if (!pauseStatus)
        { 
        }
    }
  
    IEnumerator waitTime(float time)
    {
        yield return new WaitForSeconds(time);
        AudioManager.Instance.Play("Win");

        winMenu.SetActive(true);
    
    }
   
    void StartWinMenu()
    {
        StartCoroutine(waitTime(2));
    }
    IEnumerator WaitforLoseMenu()
    {

        yield return new WaitForSeconds(0.5f);
        ActivateLoseMenu();
    }
    void ActivateLoseMenu() 
    {
        loseMenu.SetActive(true);

    }
    void StartLoseMenu()
    {
        Analytics.Events.LevelFailed(PlayerPrefs.GetInt("LevelNumber"), 0);
        StartCoroutine(WaitforLoseMenu());
    }
    
    
    void CameraDisable() { VmCam.SetActive(false); Analytics.Events.LevelComplete(PlayerPrefs.GetInt("LevelNumber"), 0); }
    public void ChangeCameraOrientation(Vector3 cameraOrientation)
    { 
        VmCam.SetActive(true);
        LeanTween.rotateLocal(VmCam, cameraOrientation, 0.5f).setEaseInOutSine();
     //   StartCoroutine(DisableCamera(0.6f));
    }
    void LoadNextLevel()
    {
        LevelManager.instance.UpgradeLevelNumber();
        SceneManager.LoadScene("demo");
    }
    void RestartLevel()
    {
        SceneManager.LoadScene("demo");

    }
    public void ColorBackgroundAndChangeGroundTexture ()
    {
        if (PlayerPrefs.GetInt("LevelNumber") < 10)
        {

            background.GetComponent<Renderer>().material.SetColor("_BaseColor",BackgroundColors[0]);
            foreach(GameObject floor in floors)
            floor.GetComponent<Renderer>().sharedMaterial=groundMaterials[0];

        } 
        if (PlayerPrefs.GetInt("LevelNumber") > 10&&PlayerPrefs.GetInt("LevelNumber")<=20)
        {

            background.GetComponent<Renderer>().material.SetColor("_BaseColor", BackgroundColors[1]);
            foreach (GameObject floor in floors)
                floor.GetComponent<Renderer>().sharedMaterial=groundMaterials[1];

        }
        if (PlayerPrefs.GetInt("LevelNumber") > 20&&PlayerPrefs.GetInt("LevelNumber")<=30)
        {

            background.GetComponent<Renderer>().material.SetColor("_BaseColor", BackgroundColors[2]);
            foreach (GameObject floor in floors)

                floor.GetComponent<Renderer>().sharedMaterial=groundMaterials[2];

        } 
        if (PlayerPrefs.GetInt("LevelNumber") > 30&&PlayerPrefs.GetInt("LevelNumber")<=40)
        {

            background.GetComponent<Renderer>().material.SetColor("_BaseColor", BackgroundColors[3]);
            foreach (GameObject floor in floors)

                floor.GetComponent<Renderer>().sharedMaterial=groundMaterials[3];

        }
              if (PlayerPrefs.GetInt("LevelNumber") > 30&&PlayerPrefs.GetInt("LevelNumber")<=40)
        {

            background.GetComponent<Renderer>().material.SetColor("_BaseColor", BackgroundColors[4]);
            foreach (GameObject floor in floors)

                floor.GetComponent<Renderer>().sharedMaterial=groundMaterials[4];

        }
            
    
    
    
    
    }

  
    void Update()
    {
        UpdateCameraCenter();
        CheckIfGuardsAlive();
        CheckSound();
    }
    void CheckSound()
    {
        if (AudioListener.volume == 0)
            soundIcon.sprite = soundSprites[0];
        else soundIcon.sprite = soundSprites[1];
    }
    void UpdateCameraCenter()
    {
        //        float posLerp = Mathf.Lerp(initCameraPos, finalCameraPos, VirtualCamera.Follow.transform.position.z);
        float posYLerp = Mathf.Abs(VirtualCamera.Follow.transform.position.z - finalCameraPosY) / Mathf.Abs(finalCameraPosY - initCameraPosY);
        float screenYLerp = Mathf.Lerp(initScreenXYBiasValue, finalScreenXYBiasValue, posYLerp);
        
        float posXLerp = Mathf.Abs(VirtualCamera.Follow.transform.position.x - finalCameraPosX) / Mathf.Abs(finalCameraPosX - initCameraPosX);
        float screenXLerp = Mathf.Lerp(finalScreenXBiasValue, initScreenXBiasValue, posXLerp);
     
        VirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY = screenYLerp;
        VirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = screenXLerp;
    }
}
