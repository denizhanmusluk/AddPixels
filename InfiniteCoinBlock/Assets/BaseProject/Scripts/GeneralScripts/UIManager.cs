using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using DG.Tweening;
using ObserverSystem;
public class UIManager : Subject
{
    public event Action OnLevelStart, OnNextLevel, OnLevelRestart, OnGamePaused, OnGameResume;
    
    [Header("Screens")]
    public GameObject startCanvas;
    public GameObject ingameCanvas;
    public GameObject finishCanvas;
    public GameObject failCanvas;
    public GameObject pauseMenu;


    //[Header("In Game")]
    //public LevelBarDisplay levelBarDisplay;
    public TextMeshProUGUI inGameScoreText;
    public Image moneyIcon;
    //[Header("Finish Screen")]
    //public ScoreTextManager scoreText;
    
    
    void Awake()
    {
        CheckAndDisplayStartScreen();
    }
    public override void SpesificStart()
    {

    }
    void CheckAndDisplayStartScreen()
    {
        startCanvas.SetActive(true);
        //int displayStart = PlayerPrefs.GetInt("displayStart");
        //if(displayStart > 0)
        //{
        //    startCanvas.SetActive(true);
        //}
        //else
        //{ 
        //    StartLevel();
        //    Invoke(nameof(StartLevelButton), DEFAULT_START_DELAY);
        //    PlayerPrefs.SetInt("displayStart", 1);
        //}
    }

    #region Handler Functions

    public void StartLevelButton()
    {
        OnLevelStart?.Invoke();
        Notify(NotificationType.Start);
        UpgradeManager.Instance.upgradePanel.SetActive(true);
    }

    public void NextLevelButton()
    {
        OnNextLevel?.Invoke();
    }
    public void pauseLevelButton()
    {
        OnGamePaused?.Invoke();
    }
    public void resumeLevelButton()
    {
        OnGameResume?.Invoke();
        pauseMenu.SetActive(false);
        //Globals.isGameActive = true;
    }
    public void RestartLevelButton()
    {
        OnLevelRestart?.Invoke();
    }

    #endregion

    public void StartLevel()
    {
        startCanvas.SetActive(false);
        ingameCanvas.SetActive(true);
    }

    public void FinishLevel()
    {
        ingameCanvas.SetActive(false);
        finishCanvas.SetActive(true);
    }

    public void FailLevel()
    {
        ingameCanvas.SetActive(false);
        failCanvas.SetActive(true);
    }
    // Money Update with LeanTween
    //public void MoneyUpdate(int miktar)
    //{
    //    int moneyOld = Globals.moneyAmount;
    //    Globals.moneyAmount = Globals.moneyAmount + miktar;
    //    LeanTween.value(moneyOld, Globals.moneyAmount, 0.2f).setOnUpdate((float val) =>
    //    {
    //        inGameScoreText.text = ((int)val).ToString();
    //    });
    //    PlayerPrefs.SetInt("money", Globals.moneyAmount);
    //}

    // Money Update with Coroutine
    public void MoneyUpdate(int miktar)
    {
        float moneyOld = (float)Globals.moneyAmount;
        Globals.moneyAmount = Globals.moneyAmount + miktar;
        StartCoroutine(setVal(miktar, moneyOld));

        PlayerPrefs.SetInt("money", Globals.moneyAmount);

        UpgradeManager.Instance.isEnoughMoney();
    }

    IEnumerator setVal(int amount, float oldAmount)
    {
        DoGetValueScale(moneyIcon.transform, true, 0.75f, 1, 0.5f, Ease.OutElastic);
        float counter = 0f;
        while (counter < 1f)
        {
            counter += 8 * Time.deltaTime;
            float money = Mathf.Lerp(oldAmount, (float)Globals.moneyAmount, counter);
            if (Globals.moneyAmount < 1000)
            {
                inGameScoreText.text = ((int)money).ToString();
            }
            else if(Globals.moneyAmount < 1000000)
            {
                inGameScoreText.text = ((int)money / 1000).ToString() + "." + (((int)money / 100) % 10).ToString() + "k";
            }
            else
            {
                inGameScoreText.text = ((int)money / 1000000).ToString() + "." + (((int)money / 100000) % 10).ToString() + "m";
            }
            yield return null;
        }
        if (Globals.moneyAmount < 1000)
        {
            inGameScoreText.text = ((int)Globals.moneyAmount).ToString();
        }
        else if (Globals.moneyAmount < 1000000)

        {
            inGameScoreText.text = ((int)Globals.moneyAmount / 1000).ToString() + "." + (((int)Globals.moneyAmount / 100) % 10).ToString() + "k";
        }
        else
        {
            inGameScoreText.text = ((int)Globals.moneyAmount / 1000000).ToString() + "." + (((int)Globals.moneyAmount / 100000) % 10).ToString() + "m";

        }
    }
    public Tween DoGetValueScale(Transform tr, bool active, float value, float lastValue, float duration, DG.Tweening.Ease type)
    {
        //Vector3 firstScale = tr.localScale;
        Tween tween = DOTween.To
            (() => value, x => value = x, lastValue, duration).SetEase(type).OnUpdate(delegate ()
            {
                tr.localScale = Vector3.one * value;
            });
        return tween;
    }
    public void WinLevel()
    {
        Notify(NotificationType.Win);
    }
}
