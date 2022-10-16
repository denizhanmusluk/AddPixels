using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    private static UpgradeManager _instance = null;
    public static UpgradeManager Instance => _instance;
    [SerializeField] Transform brickParent;
    [SerializeField] public GameObject upgradePanel;
    [SerializeField] UpgradeSettings upgradeSettings;
    [SerializeField] UpgradeButton brickUpgradeButton;
    [SerializeField] UpgradeButton staminaUpgradeButton;
    [SerializeField] UpgradeButton clickAnimUpgradeButton;
    //int brickUpgrade_Level;
    //int staminaUpgrade_Level;
    private void Awake()
    {
        _instance = this;

        upgradePanel.SetActive(false);
        Globals.brickLevel = PlayerPrefs.GetInt("BrickUpgradeLevel");
        Globals.staminaLevel = PlayerPrefs.GetInt("StaminaUpgradeLevel");
        Globals.clickAnimLevel = PlayerPrefs.GetInt("ClickAnimLevel");
        Init();
    }
    private void Start()
    {
        isEnoughMoney();
    }
    void Init()
    {
        Globals.brickPerHit = upgradeSettings._brickCount[Globals.brickLevel];
        Globals.healthDownSpeed = upgradeSettings._healthDownPerSeconds[Globals.staminaLevel];
        Globals.clickAnimSpeed = upgradeSettings._clickAnimSpeed[Globals.clickAnimLevel];
        ModelCreat();
        if (Globals.brickLevel < upgradeSettings._brickUpgradeCost.Length - 1)
        {
            brickUpgradeButton.TextInit(Globals.brickLevel, upgradeSettings._brickUpgradeCost[Globals.brickLevel + 1]);
        }
        else
        {
            brickUpgradeButton.TextInitFull();
        }
        if (Globals.staminaLevel < upgradeSettings._staminaUpgradeCost.Length - 1)
        {
            staminaUpgradeButton.TextInit(Globals.staminaLevel, upgradeSettings._staminaUpgradeCost[Globals.staminaLevel + 1]);
        }
        else
        {
            staminaUpgradeButton.TextInitFull();
        }
        if (Globals.clickAnimLevel < upgradeSettings._clickAnimUpgradeCost.Length - 1)
        {
            clickAnimUpgradeButton.TextInit(Globals.clickAnimLevel, upgradeSettings._clickAnimUpgradeCost[Globals.clickAnimLevel + 1]);
        }
        else
        {
            clickAnimUpgradeButton.TextInitFull();
        }
    }
    public void BrickUpgradeButton()
    {
        if (Globals.brickLevel < upgradeSettings._brickUpgradeCost.Length - 1)
        {
            if (Globals.moneyAmount >= upgradeSettings._brickUpgradeCost[Globals.brickLevel + 1])
            {
                GameManager.Instance.MoneyUpdate(-upgradeSettings._brickUpgradeCost[Globals.brickLevel + 1]);
                Globals.brickLevel++;
                PlayerPrefs.SetInt("BrickUpgradeLevel", Globals.brickLevel);
                Init();
            }
        }
    }
    public void StaminaUpgradeButton()
    {
        if (Globals.staminaLevel < upgradeSettings._staminaUpgradeCost.Length - 1)
        {
            if (Globals.moneyAmount >= upgradeSettings._staminaUpgradeCost[Globals.staminaLevel + 1])
            {
                GameManager.Instance.MoneyUpdate(-upgradeSettings._staminaUpgradeCost[Globals.staminaLevel + 1]);
                Globals.staminaLevel++;
                PlayerPrefs.SetInt("StaminaUpgradeLevel", Globals.staminaLevel);
                Init();
            }
        }
    }
    public void ClickAnimUpgradeButton()
    {
        if (Globals.clickAnimLevel < upgradeSettings._clickAnimUpgradeCost.Length - 1)
        {
            if (Globals.moneyAmount >= upgradeSettings._clickAnimUpgradeCost[Globals.clickAnimLevel + 1])
            {
                GameManager.Instance.MoneyUpdate(-upgradeSettings._clickAnimUpgradeCost[Globals.clickAnimLevel + 1]);
                Globals.clickAnimLevel++;
                PlayerPrefs.SetInt("ClickAnimLevel", Globals.clickAnimLevel);
                Init();
            }
        }
    }
    public void isEnoughMoney()
    {
        if (Globals.brickLevel < upgradeSettings._brickUpgradeCost.Length - 1 && Globals.moneyAmount >= upgradeSettings._brickUpgradeCost[Globals.brickLevel + 1])
        {
            brickUpgradeButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            brickUpgradeButton.GetComponent<Button>().interactable = false;
        }


        if (Globals.staminaLevel < upgradeSettings._staminaUpgradeCost.Length - 1 && Globals.moneyAmount >= upgradeSettings._staminaUpgradeCost[Globals.staminaLevel + 1])
        {
            staminaUpgradeButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            staminaUpgradeButton.GetComponent<Button>().interactable = false;
        }


        if (Globals.clickAnimLevel < upgradeSettings._clickAnimUpgradeCost.Length - 1 && Globals.moneyAmount >= upgradeSettings._clickAnimUpgradeCost[Globals.clickAnimLevel + 1])
        {
            clickAnimUpgradeButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            clickAnimUpgradeButton.GetComponent<Button>().interactable = false;
        }
    }
    void ModelCreat()
    {
        if(brickParent.childCount > 0)
        {
            Destroy(brickParent.GetChild(0).gameObject);
        }
        GameObject _brick = Instantiate(upgradeSettings._brickBoxPrefab[Globals.brickLevel], transform.position, Quaternion.identity, brickParent);
        _brick.transform.localPosition = new Vector3(0, 10, 0);
        _brick.transform.localScale = new Vector3(21, 21, 21);
    }
}
