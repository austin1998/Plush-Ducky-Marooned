using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaRecoveryUpgradeVisuals : MonoBehaviour
{
    PlayerHealth healthGO;
    CustomFirstPersonController FPController;
    [SerializeField] List<GameObject> levelsVisualList = new List<GameObject>();
    [SerializeField] GameObject UPGRADE_COST_Text;
    [SerializeField] GameObject StamRecoveryText;
    // Start is called before the first frame update
    void Start() {
        healthGO = FindObjectOfType<PlayerHealth>();
        FPController = FindObjectOfType<CustomFirstPersonController>();
        Debug.Log("CurrentUpgradeVisual instantiated");
        updateStaminaRecoveryVisuals(0, FPController.GetStaminaRecoveryPerSecond(), GameObject.Find("BuyMenu").GetComponent<Shop>().get_UPGRADE_COST());
    }

    // Update is called once per frame
    public void updateLevelsVisualList(int level) {
        //int level = 1; //change this
        Debug.Log("updating color level: " + level);
        for (int i = 0; i < level; i++) {
            levelsVisualList[i].GetComponent<UnityEngine.UI.Image>().color = Color.white;
        }
    }

    public void updateStaminaRecoveryVisuals(int level, float staminaRecovery, int UPGRADE_COST) {
        updateHPDelayValueText(level, staminaRecovery);
        updateTextToUpgrade(level, UPGRADE_COST);
        updateLevelsVisualList(level);
    }

    public void updateHPDelayValueText(int level, float staminaRecovery) {
        float hpDelayRounded = (int)Math.Round(staminaRecovery * 100);
        float staminaRecoveryInt = (float)hpDelayRounded / 100;
        StamRecoveryText.GetComponent<Text>().text = staminaRecoveryInt + " Stamina/second";
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
