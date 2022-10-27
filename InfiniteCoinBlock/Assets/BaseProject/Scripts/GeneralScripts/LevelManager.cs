using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[System.Serializable]
public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance = null;
    public static LevelManager Instance => _instance;

    public GameObject loadedLevel;

    [SerializeField] private List<LevelScriptable> level_set;
    [SerializeField] public List<BuildSettings> levelBuildSettings;
    [SerializeField] public List<UpgradeSettings> upgradeSettings;

    //[SerializeField] List<GameObject> levels;
    //[SerializeField] public int LevelCount;
    public TextMeshProUGUI levelText;
    private void Awake()
    {
        _instance = this;
    }
    public void levelInit()
    {
        Globals.LevelCount = levelBuildSettings.Count;
        if (levelBuildSettings.Count > 0)
        {
            levelLoad();
        }
    }
    void levelLoad()
    {
        if (PlayerPrefs.GetInt("levelIndex") != 0)
        {
            Globals.currentLevel = PlayerPrefs.GetInt("levelIndex");
            levelText.text = Globals.currentLevel.ToString();
        }
        else
        {
            PlayerPrefs.SetInt("levelIndex", Globals.currentLevel);

        }
        //PlayerPrefs.SetInt("level", 0);
        if (PlayerPrefs.GetInt("level") != 0)
        {
            Debug.Log(PlayerPrefs.GetInt("level"));
            Globals.currentLevelIndex = PlayerPrefs.GetInt("level");

        }

        ////////LevelsPrefab = (GameObject)Instantiate(Resources.Load("Level" + Globals.currentLevelIndex.ToString()));
        loadedLevel = Instantiate(level_set[Globals.currentLevelIndex]._levelPrefab, transform.position, Quaternion.identity);

        Debug.Log("level " + Globals.currentLevel);
        Debug.Log("LEVEL " + PlayerPrefs.GetInt("levelIndex"));
        Debug.Log(PlayerPrefs.GetInt("level"));
    }
}
