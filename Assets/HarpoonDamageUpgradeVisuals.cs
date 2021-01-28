using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HarpoonDamageUpgradeVisuals : MonoBehaviour
{
    HarpoonGun harpoonGO;
    [SerializeField] List<GameObject> levelsVisualList = new List<GameObject>();
    [SerializeField] GameObject UPGRADE_COST_Text;
    [SerializeField] GameObject damageText;
    // Start is called before the first frame update
    void Start() {
        harpoonGO = FindObjectOfType<HarpoonGun>();
        Debug.Log("CurrentUpgradeVisual instantiated");
        updateHarpoonDamageVisuals(0, harpoonGO.GetMaxWeaponDamage(), GameObject.Find("BuyMenu").GetComponent<Shop>().get_UPGRADE_COST());
    }

    // Update is called once per frame
    public void updateLevelsVisualList(int level) {
        //int level = 1; //change this
        Debug.Log("updating color level: " + level);
        for (int i = 0; i < level; i++) {
            levelsVisualList[i].GetComponent<UnityEngine.UI.Image>().color = Color.white;
        }
    }

    public void updateHarpoonDamageVisuals(int level, float damage, int UPGRADE_COST) {
        updateDamageValueText(level, damage);
        updateTextToUpgrade(level, UPGRADE_COST);
        updateLevelsVisualList(level);
    }

    public void updateDamageValueText(int level, float damage) {
        float damageRounded = (int)Math.Round(damage * 100);
        float damageInt = (float)damageRounded / 100;
        damageText.GetComponent<Text>().text = damageInt + " Damage";
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
