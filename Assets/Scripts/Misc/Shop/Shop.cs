using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {
    static int UPGRADE_COST = 1000;
    //objects
    CustomFirstPersonController FPController;
    PlayerHealth healthGO;
    Pistol pistolGO;
    Saber saberGO;
    Blunderbuss blunderbussGO;
    HarpoonGun harpoonGO;
    HandCannon handCannonGO;

    //levels
    //player
    int HPLevel = 0;
    int HPDelayLevel = 0;
    int HPRegenLevel = 0;
    int MaxStaminaLevel = 0;
    int staminaRecLevel = 0;
    //pistol
    int pistolDamageLevel = 0;
    int pistolFRLevel = 0;
    int pistolAmmoLevel = 0;
    //saber
    int saberDamageLevel = 0;
    //blunderbuss
    int blunderDamageLevel = 0;
    int blunderFRLevel = 0;
    //harpoon
    int harpoonDamageLevel = 0;
    int harpoonFRLevel = 0;
    //cannon
    int cannonDamageLevel = 0;
    int cannonFRLevel = 0;

    //ints
    int money = 0;//CHANGE THIS TO 0
    [SerializeField] GameObject moneyText;
    [SerializeField] GameObject moneyTextInBuy;

    [SerializeField] GameObject SaberObject;

    //serialized fields
    //player
    [SerializeField] float maxHealthAdd = 10;
    [SerializeField] float regenTimerReduc = 0.15f;
    [SerializeField] float healthRegenPerSec = 0.4f;
    [SerializeField] int staminaAdd = 8;
    [SerializeField] float staminaRecoveryAdd = 2.0f;
    //pistol
    [SerializeField] int pistolUpDamage = 8;
    [SerializeField] float pistolReduceFireRate = 0.05f;
    [SerializeField] int pistolAddAmmo = 10;
    //saber
    [SerializeField] int saberUpDamage = 10;
    //blunderbuss
    [SerializeField] int blunderUpDamage = 8;
    [SerializeField] float blunderReduceFireRate = 0.15f;
    //harpoon
    [SerializeField] int harpoonUpDamage = 8;
    [SerializeField] float harpoonReduceFireRate = 0.15f;
    //cannon
    [SerializeField] int cannonUpDamage = 8;
    [SerializeField] float cannonReduceFireRate = 0.15f;


    // Start is called before the first frame update
    void Start() {
        FPController = FindObjectOfType<CustomFirstPersonController>();
        healthGO = FindObjectOfType<PlayerHealth>();
        pistolGO = FindObjectOfType<Pistol>();
        saberGO = SaberObject.GetComponent<Saber>();
        blunderbussGO = FindObjectOfType<Blunderbuss>();
        harpoonGO = FindObjectOfType<HarpoonGun>();
        handCannonGO = FindObjectOfType<HandCannon>();

    }

    public int get_UPGRADE_COST() {
        return UPGRADE_COST;
    }

    private void OnEnable() {//setActive(true) https://www.youtube.com/watch?v=OD-p1eMsyrU&ab_channel=Unity
        Debug.Log("Enabled");
    }
    private void OnDisable() {//setActive(false)
        Debug.Log("Disabled");
    }

    public void AddMoney(int amount) {
        //austin
        money += amount;
        moneyText.GetComponent<Text>().text = "$" + money;
        moneyTextInBuy.GetComponent<Text>().text = "$ " + money;
        /*try { moneyText.GetComponent<Text>().text = "$ " + money; }
        catch(Exception exception) {
            Debug.Log(exception);
        }*/
    }

    private void RemoveMoney() {
        //austin
        //can you throw exception "Not Enough Money" if not enough skill points
        if(money >= UPGRADE_COST)
        {
            money -= UPGRADE_COST;
            moneyText.GetComponent<Text>().text = "$ " + money;
            moneyTextInBuy.GetComponent<Text>().text = "$ " + money;
            return;
        }
        else
        {
            throw new Exception("RemoveMoney() broke: Your broke! get some more money you piece of poop");
        }
    }


    public void HPIncrease()
    {
        Debug.Log("HPIncrease() called");
        try
        {
            if (HPLevel < 20)
            {
                try
                {
                    RemoveMoney();
                }
                catch (Exception exception)
                {
                    Debug.Log("RemoveMoney() returned exception: " + exception);
                    return;
                }
                HPLevel++;
                //Debug.Log(player.transform.GetComponent<Health>().);
                healthGO.SetMaxHealth( (float)Math.Round((healthGO.GetMaxHealth() + maxHealthAdd + float.Epsilon) *100)/100);
                //player.transform.GetComponent<PlayerHealth>().SetMaxHealth(healthGO.GetMaxHealth() + maxHealthAdd);
                //GameObject.Find("Player").GetComponent<PlayerHealth>().SetMaxHealth(healthGO.GetMaxHealth() + maxHealthAdd);
                //Debug.Log("max hp:" + GameObject.Find("Player").GetComponent<PlayerHealth>().GetMaxHealth());
                Debug.Log("max hp:" + healthGO.GetMaxHealth());
                GameObject.Find("HealthContainer/UpgradeInfo")
                    .GetComponentInChildren<CurrentUpgradeVisual>()
                    .updateMaxHPVisuals(HPLevel, healthGO.GetMaxHealth(), UPGRADE_COST);//.updateLevelsVisualList(HPLevel);
            }
            else {
                Debug.Log("You are max level");
            }
        }
        catch (Exception exception)
        {
            Debug.Log("RemoveMoney() from HPIncrease() returned exception: " + exception);
            return;
        }
    }

    public void HPDelayDecrease()
    {

        try
        {
            if (HPDelayLevel < 20)
            {
                try
                {
                    RemoveMoney();
                }
                catch (Exception exception)
                {
                    Debug.Log("RemoveMoney() returned exception: " + exception);
                    return;
                }
                HPDelayLevel++;
                healthGO.SetHealthRegenTimer(healthGO.GetHealthRegenTimer() - regenTimerReduc);
                Debug.Log("hp delay lowered by:" + regenTimerReduc);
                Debug.Log("hp delay:" + healthGO.GetHealthRegenTimer());
                GameObject.Find("HPDelayLevelContainer/UpgradeInfo")
                    .GetComponentInChildren<HPDelayUpgradeVisuals>()
                    .updateHPDelayVisuals(HPDelayLevel, healthGO.GetHealthRegenTimer(), UPGRADE_COST);
            }
        }
        catch (Exception exception)
        {
            Debug.Log("RemoveMoney() from HPDelayDecrease() returned exception: " + exception);
            return;
        }
    }

    public void HPRegenIncrease()
    {
        Debug.Log("HPRegenIncrease() called");
        try
        {
            if (HPRegenLevel < 20)
            {
                try
                {
                    RemoveMoney();
                }
                catch (Exception exception)
                {
                    Debug.Log("RemoveMoney() returned exception: " + exception);
                    return;
                }

                HPRegenLevel++;
                healthGO.SetHealthRegenPerSecond(healthGO.GetHealthRegenPerSecond() + healthRegenPerSec);
                GameObject.Find("HPRegenContainer/UpgradeInfo")
                        .GetComponentInChildren<HPRegenUpgradeVisuals>()
                        .updateHPDelayVisuals(HPRegenLevel, healthGO.GetHealthRegenPerSecond(), UPGRADE_COST);
            }
        }

        catch (Exception exception)
        {
            Debug.Log("RemoveMoney() from HPRegenIncrease() returned exception: " + exception);
            return;
        }

    }

    public void MaxStaminaIncrease()
    {
        try
        {
            if (MaxStaminaLevel < 20)
            {
                try
                {
                    RemoveMoney();
                }
                catch (Exception exception)
                {
                    Debug.Log("RemoveMoney() returned exception: " + exception);
                    return;
                }

                MaxStaminaLevel++;
                FPController.SetMaxStamina(FPController.GetMaxStamina() + staminaAdd);
                GameObject.Find("MaxStaminaContainer/UpgradeInfo")
                    .GetComponentInChildren<MaxStaminaUpgradeVisuals>()
                    .updateStaminaVisuals(MaxStaminaLevel, FPController.GetMaxStamina(), UPGRADE_COST);
            }
        }
        catch (Exception exception)
        {
            Debug.Log("RemoveMoney() from MaxStaminaIncrease() from returned exception: " + exception);
            return;
        }
    }

    public void StaminaRecoveryIncrease()
    {
        if(staminaRecLevel < 20) { 
            try
            {
                RemoveMoney();
            }
            catch (Exception exception)
            {
                Debug.Log("RemoveMoney() from MoveSpeedIncrease() returned exception: " + exception);
                return;
            }
            staminaRecLevel++;
            FPController.SetStaminaRecoveryPerSecond(FPController.GetStaminaRecoveryPerSecond() + staminaRecoveryAdd);
            GameObject.Find("StaminaRecoveryContainer/UpgradeInfo")
                .GetComponentInChildren<StaminaRecoveryUpgradeVisuals>()
                .updateStaminaRecoveryVisuals(staminaRecLevel, FPController.GetStaminaRecoveryPerSecond(), UPGRADE_COST);
        }
        
    }

    //weapon upgrades
    public void pistolDamageUp()
    {
        if (pistolDamageLevel < 20)
        {
            try
            {
                RemoveMoney();
            }
            catch (Exception exception)
            {
                Debug.Log("RemoveMoney() from pistolDamageUp() returned exception: " + exception);
                return;
            }
            pistolGO.SetMinWeaponDamage(pistolGO.GetMinWeaponDamage() + pistolUpDamage);
            pistolGO.SetMaxWeaponDamage(pistolGO.GetMaxWeaponDamage() + pistolUpDamage);
            pistolDamageLevel++;
            GameObject.Find("DamageContainer/UpgradeInfo")
                .GetComponentInChildren<PistolDamageUpgradeVisuals>()
                .updatePistolDamageVisuals(pistolDamageLevel, pistolGO.GetMaxWeaponDamage(), UPGRADE_COST);
            return;
        }
        else
        {
            throw new Exception("Your at max upgrades, are you kidding you want more after all ive done for you!");
        }

    }

    public void pistolFireRate()
    {
        if (pistolFRLevel < 20)
        {
            try
            {
                RemoveMoney();
            }
            catch (Exception exception)
            {
                Debug.Log("RemoveMoney() from pistolFireRate() returned exception: " + exception);
                return;
            }

            pistolGO.SetTimeBetweenAttacks(pistolGO.GetTimeBetweenAttacks() - pistolReduceFireRate);
            pistolFRLevel++;
            GameObject.Find("ReloadTimeContainer/UpgradeInfo")
                .GetComponentInChildren<PistolReloadTimeUpgradeVisuals>()
                .updatePistolReloadTimeVisuals(pistolFRLevel, pistolGO.GetTimeBetweenAttacks(), UPGRADE_COST);
            return;
        }
        else
        {
            throw new Exception("Your at max upgrades, are you kidding you want more after all ive done for you!");
        }

    }
    public void pistolMaxAmmo()
    {
        if (pistolAmmoLevel < 20)
        {
            try
            {
                RemoveMoney();
            }
            catch (Exception exception)
            {
                Debug.Log("RemoveMoney() from pistolMaxAmmo() returned exception: " + exception);
                return;
            }

            //change to ammo
            //pistolGO.SetMaxAmmo(pistolGO.GetMaxAmmo() + pistolAddAmmo);
            Pistol.AddMaxAmmo(pistolAddAmmo);
            pistolAmmoLevel++;
            GameObject.Find("MaxAmmoContainer/UpgradeInfo")
                .GetComponentInChildren<MaxAmmoUpgradeVisuals>()
                .updatePistolMaxAmmoVisuals(pistolAmmoLevel, pistolGO.GetMaxAmmo(), UPGRADE_COST);
            return;
        }
        else
        {
            throw new Exception("Your at max upgrades, are you kidding you want more after all ive done for you!");
        }

    }

    public void saberDamageUp()
    {
        if (saberDamageLevel < 20)
        {
            try
            {
                RemoveMoney();
            }
            catch (Exception exception)
            {
                Debug.Log("RemoveMoney() returned exception: " + exception);
                return;
            }
            saberGO.SetMinWeaponDamage(saberGO.GetMinWeaponDamage() + saberUpDamage);
            saberGO.SetMaxWeaponDamage(saberGO.GetMaxWeaponDamage() + saberUpDamage);
            saberDamageLevel++;
            GameObject.Find("DamageContainer/UpgradeInfo")
                .GetComponentInChildren<SaberDamageUpgradeVisuals>()
                .updateSaberDamageVisuals(saberDamageLevel, saberGO.GetMaxWeaponDamage(), UPGRADE_COST);
            return;
        }
        else
        {
            throw new Exception("Your at max upgrades, are you kidding you want more after all ive done for you!");
        }

    }

    public void blunderDamageUp()
    {
        if (blunderDamageLevel < 20)
        {
            try
            {
                RemoveMoney();
            }
            catch (Exception exception)
            {
                Debug.Log("RemoveMoney() returned exception: " + exception);
                return;
            }

            blunderbussGO.SetMaxWeaponDamage(blunderbussGO.GetMaxWeaponDamage() + blunderUpDamage);
            blunderbussGO.SetMinWeaponDamage(blunderbussGO.GetMinWeaponDamage() + blunderUpDamage);
            blunderDamageLevel++;
            GameObject.Find("DamageContainer/UpgradeInfo")
                .GetComponentInChildren<BlunderbussDamageUpgradeVisuals>()
                .updateBlunderDamageVisuals(blunderDamageLevel, blunderbussGO.GetMaxWeaponDamage(), UPGRADE_COST);
            return;
        }
        else
        {
            throw new Exception("Your at max upgrades, are you kidding you want more after all ive done for you!");
        }

    }

    public void blunderFireRate()
    {
        if (blunderFRLevel < 20)
        {
            try
            {
                RemoveMoney();
            }
            catch (Exception exception)
            {
                Debug.Log("RemoveMoney() returned exception: " + exception);
                return;
            }

            blunderbussGO.SetTimeBetweenAttacks(blunderbussGO.GetTimeBetweenAttacks() - blunderReduceFireRate);
            blunderFRLevel++;
            GameObject.Find("ReloadTimeContainer/UpgradeInfo")
                .GetComponentInChildren<BlunderbussReloadUpgradeVisuals>()
                .updateBlunderReloadTimeVisuals(blunderFRLevel, blunderbussGO.GetTimeBetweenAttacks(), UPGRADE_COST);
            return;
        }
        else
        {
            throw new Exception("Your at max upgrades, are you kidding you want more after all ive done for you!");
        }

    }

    public void harpoonDamageUp() {
        if (harpoonDamageLevel < 20) {
            try {
                RemoveMoney();
            }
            catch (Exception exception) {
                Debug.Log("RemoveMoney() returned exception: " + exception);
                return;
            }
            harpoonGO.SetMaxWeaponDamage(harpoonGO.GetMaxWeaponDamage() + harpoonUpDamage);
            harpoonGO.SetMinWeaponDamage(harpoonGO.GetMinWeaponDamage() + harpoonUpDamage);
            harpoonDamageLevel++;
            GameObject.Find("DamageContainer/UpgradeInfo")
                .GetComponentInChildren<HarpoonDamageUpgradeVisuals>()
                .updateHarpoonDamageVisuals(harpoonDamageLevel, harpoonGO.GetMaxWeaponDamage(), UPGRADE_COST);
            return;
        }
        else {
            throw new Exception("Your at max upgrades, are you kidding you want more after all ive done for you!");
        }
    }

    public void harpoonFireRate() {
        try {
            if (harpoonFRLevel < 20) {
                try {
                    RemoveMoney();
                }
                catch (Exception exception) {
                    Debug.Log("RemoveMoney() returned exception: " + exception);
                    return;
                }
                harpoonGO.SetTimeBetweenAttacks(harpoonGO.GetTimeBetweenAttacks() - harpoonReduceFireRate);
                harpoonFRLevel++;
                GameObject.Find("ReloadTimeContainer/UpgradeInfo")
                    .GetComponentInChildren<HarpoonReloadUpgradeVisuals>()
                    .updateHarpoonReloadVisuals(harpoonFRLevel, harpoonGO.GetTimeBetweenAttacks(), UPGRADE_COST);
                return;
            }
            else {
                throw new Exception("Your at max upgrades, are you kidding you want more after all ive done for you!");
            }
        }
        catch (Exception exception) {
            Debug.Log("harpoonFireRate() returned exception: " + exception);
            return;
        }
    }

    public void cannonDamageUp() {
        if (cannonDamageLevel < 20) {
            try {
                RemoveMoney();
            }
            catch (Exception exception) {
                Debug.Log("RemoveMoney() returned exception: " + exception);
                return;
            }
            handCannonGO.SetMaxWeaponDamage(handCannonGO.GetMaxWeaponDamage() + cannonUpDamage);
            handCannonGO.SetMinWeaponDamage(handCannonGO.GetMinWeaponDamage() + cannonUpDamage);
            cannonDamageLevel++;
            GameObject.Find("DamageContainer/UpgradeInfo")
                .GetComponentInChildren<CannonDamageUpgradeVisuals>()
                .updateCannonDamageVisuals(cannonDamageLevel, handCannonGO.GetMaxWeaponDamage(), UPGRADE_COST);
            return;
        }
        else {
            throw new Exception("Your at max upgrades, are you kidding you want more after all ive done for you!");
        }
    }

    public void cannonFireRate() {
        try {
            if (cannonFRLevel < 20) {
                try {
                    RemoveMoney();
                }
                catch (Exception exception) {
                    Debug.Log("RemoveMoney() returned exception: " + exception);
                    return;
                }
                handCannonGO.SetTimeBetweenAttacks(handCannonGO.GetTimeBetweenAttacks() - cannonReduceFireRate);
                cannonFRLevel++;
                CannonReloadUpgradeVisuals.Instance.updateCannonReloadTimeVisuals(cannonFRLevel, handCannonGO.GetTimeBetweenAttacks(), UPGRADE_COST);
                /*GameObject.Find("ReloadTimeContainer/UpgradeInfo")
                    .GetComponentInChildren<CannonReloadUpgradeVisuals>()
                    .updateCannonReloadTimeVisuals(cannonFRLevel, handCannonGO.GetTimeBetweenAttacks(), UPGRADE_COST);*/
                return;
            }
            else {
                throw new Exception("Your at max upgrades, are you kidding you want more after all ive done for you!");
            }
        }
        catch (Exception exception) {
            Debug.Log("cannonFireRate() returned exception: " + exception);
            return;
        }
    }


    public int MyProperty { get; set; }
    public float MyProperty2 { get; set; }


}