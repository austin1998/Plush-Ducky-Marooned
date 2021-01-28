using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPRegenUpgradeVisuals : MonoBehaviour
{
    PlayerHealth healthGO;
    [SerializeField] List<GameObject> levelsVisualList = new List<GameObject>();
    [SerializeField] GameObject UPGRADE_COST_Text;
    [SerializeField] GameObject HPRegenText;
    // Start is called before the first frame update
    void Start() {
        healthGO = FindObjectOfType<PlayerHealth>();
        Debug.Log("CurrentUpgradeVisual instantiated");
        updateHPDelayVisuals(0, healthGO.GetHealthRegenPerSecond(), GameObject.Find("BuyMenu").GetComponent<Shop>().get_UPGRADE_COST());
    }

    // Update is called once per frame
    public void updateLevelsVisualList(int level) {
        //int level = 1; //change this
        Debug.Log("updating color level: " + level);
        for (int i = 0; i < level; i++) {
            levelsVisualList[i].GetComponent<UnityEngine.UI.Image>().color = Color.white;
        }
    }

    public void updateHPDelayVisuals(int level, float hpRegen, int UPGRADE_COST) {
        updateHPDelayValueText(level, hpRegen);
        updateTextToUpgrade(level, UPGRADE_COST);
        updateLevelsVisualList(level);
    }

    public void updateHPDelayValueText(int level, float hpRegen) {
        //float hpDelayRounded = (int)(Math.Round(hpDelay) * 100);
        float hpDelayRounded = (int)Math.Round(hpRegen * 100);
        hpDelayRounded = (float)hpDelayRounded / 100;
        HPRegenText.GetComponent<Text>().text = hpDelayRounded + " HP/second";
    }

    public void updateTextToUpgrade(int level, int UPGRADE_COST) {
        if (level == 20) {
            UPGRADE_COST_Text.GetComponent<Text>().color = Color.green;
            UPGRADE_COST_Text.GetComponent<Text>().text = "MAX LEVEL!";
        }
        else {
            UPGRADE_COST_Text.GetComponent<Text>().text = "-$" +
                UPGRADE_COST +
                " to upgrade to level " +
                (level + 1);
        }

    }
}
