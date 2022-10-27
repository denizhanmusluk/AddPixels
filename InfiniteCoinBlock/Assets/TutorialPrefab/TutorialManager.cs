using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private static TutorialManager _instance = null;
    public static TutorialManager Instance => _instance;

    [SerializeField] GameObject tutorial_TapPlay, tutorial_Upgrade_1, tutorial_Upgrade_2, tutorial_Upgrade_3;
    bool tapTutActive = false;
    void Awake()
    {
        _instance = this;
        tutorial_TapPlay.SetActive(false);
        tutorial_Upgrade_1.SetActive(false);
        tutorial_Upgrade_2.SetActive(false);
        tutorial_Upgrade_3.SetActive(false);

    }
    private void Start()
    {
        //WeaponTutorial();
        //CarTaskTutorial();
    }
    public void TapPlayOpen()
    {
        if (PlayerPrefs.GetInt("levelIndex") == 1)
        {
            tutorial_TapPlay.SetActive(true);
        }
    }
    public void TapPlayClose()
    {
        if (PlayerPrefs.GetInt("levelIndex") == 1)
        {
            tutorial_TapPlay.SetActive(false);
            StartCoroutine(TapTutorialCounter());
        }
    }


    public void Tutorial_1_Open()
    {
        if (PlayerPrefs.GetInt("levelIndex") == 1)
        {
            if (PlayerPrefs.GetInt("StaminaUpgradeLevel") == 0 && Globals.StartActive)
            {
                tutorial_Upgrade_1.SetActive(true);
                TapPlayClose();

            }
        }
    }
    public void Tutorial_1_Close()
    {
        tutorial_Upgrade_1.SetActive(false);
    }

    public void Tutorial_2_Open()
    {
        if (PlayerPrefs.GetInt("levelIndex") == 1)
        {
            if (PlayerPrefs.GetInt("BrickUpgradeLevel") == 0 && Globals.StartActive)
            {
                if (PlayerPrefs.GetInt("StaminaUpgradeLevel") != 0 && PlayerPrefs.GetInt("ClickAnimLevel") != 0)
                {
                    tutorial_Upgrade_2.SetActive(true);
                    TapPlayClose();
                }
            }
        }
    }
    public void Tutorial_2_Close()
    {
        tutorial_Upgrade_2.SetActive(false);
    }
    public void Tutorial_3_Open()
    {
        if (PlayerPrefs.GetInt("levelIndex") == 1)
        {
            if (PlayerPrefs.GetInt("ClickAnimLevel") == 0 && Globals.StartActive)
            {
                if (PlayerPrefs.GetInt("StaminaUpgradeLevel") != 0)
                {
                    tutorial_Upgrade_3.SetActive(true);
                    TapPlayClose();
                }
            }
        }
    }
    public void Tutorial_3_Close()
    {
        tutorial_Upgrade_3.SetActive(false);
    }
    IEnumerator TapTutorialCounter()
    {
        tapTutActive = false;
        yield return null;
        tapTutActive = true;
        float counter = 0f;

        while (counter < 4f && tapTutActive)
        {
            counter += Time.deltaTime;

            yield return null;
        }
        if (tapTutActive)
        {
            TapPlayOpen();
        }
    }
}
