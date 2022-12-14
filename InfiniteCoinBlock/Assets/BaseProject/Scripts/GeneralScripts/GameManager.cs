using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using ObserverSystem;

public class GameManager : Observer
{
    private static GameManager _instance = null;
    public static GameManager Instance => _instance;

    public LevelManager lvlManager;
    public UIManager ui;
    float firstTime;
    void Awake()
    {
        firstTime = Time.timeScale;
        InitConnections();

        _instance = this;
    }
    void InitConnections()
    {
        ui.OnLevelStart += OnLevelStart;
        ui.OnNextLevel += OnNextLevel;
        ui.OnLevelRestart += OnLevelRestart;
        ui.OnGamePaused += onLevelPause;
        ui.OnGameResume += onLevelResume;
    }

    void Start()
    {

        lvlManager.levelInit();

        //Application.targetFrameRate = 60;
        Globals.moneyAmount = PlayerPrefs.GetInt("money");
        if (Globals.moneyAmount < 1000)
        {
            ui.inGameScoreText.text = Globals.moneyAmount.ToString();
        }
        else if(Globals.moneyAmount < 1000000)
        {
            ui.inGameScoreText.text = ((int)Globals.moneyAmount / 1000).ToString() + "." + (((int)Globals.moneyAmount / 100) % 10).ToString() + "k";
        }
        else
        {
            ui.inGameScoreText.text = ((int)Globals.moneyAmount / 1000000).ToString() + "." + (((int)Globals.moneyAmount / 100000) % 10).ToString() + "m";
        }
        ui.startCanvas.SetActive(true);
        ui.finishCanvas.SetActive(false);
        ui.failCanvas.SetActive(false);
        //// Observer Register
        ObserverManager.Instance.RegisterObserver(this, SubjectType.GameState);//observer register
    }
    void OnLevelStart()
    {
        ui.startCanvas.SetActive(false);
        ui.ingameCanvas.SetActive(true);
    }
    void OnNextLevel()
    {
        Globals.currentLevel++;
        PlayerPrefs.SetInt("levelIndex", Globals.currentLevel);

        Globals.currentLevelIndex++;
        if (Globals.LevelCount - 1 < Globals.currentLevelIndex)
        {
            Globals.currentLevelIndex = Random.Range(0, Globals.LevelCount - 1);
            PlayerPrefs.SetInt("level", Globals.currentLevelIndex);
        }
        else
        {
            PlayerPrefs.SetInt("level", Globals.currentLevelIndex);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OnLevelRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }  
    public void onLevelPause()
    {
        Time.timeScale = 0;
    }
    public void onLevelResume()
    {
        Time.timeScale = firstTime;
    }
    ///////////////////////////////////////////////////

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            ObserverManager.Instance.RemoveObserver(this);//observer register
        }

        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    Notify_WinObservers();
        //}

        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    Notify_LoseObservers();
        //}

        if (Input.GetKeyDown(KeyCode.K))
        {
            MoneyUpdate(1000);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            MoneyUpdate(1000000);
        }
    }
    public void StartState()
    {
        Globals.StartActive = true;
    }
    public void FinalState()
    {
    }
    public void failLevelbutton()
    {
        PlayerPrefs.SetInt("levelIndex", Globals.currentLevel);    
        PlayerPrefs.SetInt("level", Globals.currentLevelIndex);
    }
  
    public void FailState()
    {
        ui.failCanvas.SetActive(true);
    }
 
    public void WinState()
    {
        ui.finishCanvas.SetActive(true);
    }
    public void MoneyUpdate(int miktar)
    {
        ui.MoneyUpdate(miktar);
    }

    //// Observer Notify
    public override void OnNotify(NotificationType notificationType) //observer notify
    {
        switch (notificationType)
        {
            case NotificationType.Start:
                {
                    StartState();
                }
                break;
            case NotificationType.End:
                {

                }
                break;
            case NotificationType.Win:
                {
                    Invoke("WinState", 2);
                    Debug.Log("win");
                }
                break;
            case NotificationType.Fail:
                {
                    Invoke("FailState", 1);
                    Debug.Log("fail");
                }
                break;
       
        }
    }
}
