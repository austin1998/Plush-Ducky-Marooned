using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaberDamageUpgradeVisuals : MonoBehaviour
{
    Saber saberGO;
    [SerializeField] GameObject SaberObject;
    [SerializeField] List<GameObject> levelsVisualList = new List<GameObject>();
    [SerializeField] GameObject UPGRADE_COST_Text;
    [SerializeField] GameObject damageText;
    // Start is called before the first frame update
    void Start() {
        saberGO = SaberObject.GetComponent<Saber>();
        Debug.Log("CurrentUpgradeVisual instantiated");
        updateSaberDamageVisuals(0, saberGO.GetMaxWeaponDamage(), FindObjectOfType<Shop>().get_UPGRADE_COST());
    }

    // Update is called once per frame
    public void updateLevelsVisualList(int level) {
        //int level = 1; //change this
        Debug.Log("updating color level: " + level);
        for (int i = 0; i < level; i++) {
            levelsVisualList[i].GetComponent<UnityEngine.UI.Image>().color = Color.white;
        }
    }

    public void updateSaberDamageVisuals(int level, float damage, int UPGRADE_COST) {
        updateHPDelayValueText(level, damage);
        updateTextToUpgrade(level, UPGRADE_COST);
        updateLevelsVisualList(level);
    }

    public void updateHPDelayValueText(int level, float damage) {
        float damageRounded = (int)Math.Round(damage * 100);
        float staminaRecoveryInt = (float)damageRounded / 100;
        damageText.GetComponent<Text>().text = staminaRecoveryInt + " Damage";
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
