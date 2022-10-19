using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UpgradeButton : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI levelText;
    [SerializeField] public TextMeshProUGUI costText;
    public void TextInit(int level, int cost)
    {
        levelText.text = "Lv. " + (1 + level).ToString();
        //costText.text = "$" + cost.ToString();


        if (cost < 1000)
        {
            costText.text = "$" + cost.ToString();
        }
        else
        {
            costText.text = "$" + (cost / 1000).ToString() + "." + ((cost / 100) % 10).ToString() + "k";
        }

    }
    public void TextInitFull()
    {
        levelText.text = "Full";
        costText.text = "";
    }
}
