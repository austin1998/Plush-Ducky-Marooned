using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pistol : Weapon {

    private static TextMeshProUGUI currentAmmoText;
    private static TextMeshProUGUI totalAmmoText;

    [Header("Ammo System")]
    [SerializeField] int maxAmmoStart = 40;

    private static int maxAmmo;
    private static int currentAmmo;

    protected override void Start() {
        base.Start();

        currentAmmoText = GameObject.FindGameObjectWithTag("CurrentAmmo").GetComponent<TextMeshProUGUI>();
        totalAmmoText = GameObject.FindGameObjectWithTag("TotalAmmo").GetComponent<TextMeshProUGUI>();
        //reloadingText = GameObject.FindGameObjectWithTag("ReloadingUI").GetComponent<TextMeshProUGUI>();

        maxAmmo = maxAmmoStart;
        currentAmmo = maxAmmo;
        SetAmmoText();
    }

    protected override void Update() {
        base.Update();
        //reloadingText.text = "Reloading " + Mathf.Clamp((timeBetweenAttacks - timeSinceAttacking), 0f, timeBetweenAttacks).ToString("0.00");
    }

    public override void Attack() {

        if(currentAmmo <= 0) { return; }
        
        base.Attack();
        Debug.Log("Fired pistol");
        ((HitscanDamage)damageType).ProcessShot();

        currentAmmo--;
        SetAmmoText();

        weaponAttackParticles.Play();
    }

    public static void AddAmmo(int ammountToAdd) {
        currentAmmo = Mathf.Clamp(currentAmmo + ammountToAdd, 0, maxAmmo);
        SetAmmoText();
    }

    public static void AddMaxAmmo(int ammountToAdd)
    {
        maxAmmo += ammountToAdd;
        currentAmmo = maxAmmo;
        SetAmmoText();
    }

    private static void SetAmmoText() {
        currentAmmoText.text = currentAmmo.ToString();
        totalAmmoText.text = maxAmmo.ToString();
    }

    public int GetMaxAmmo() {
        return maxAmmo;
    }

    public int GetMaxAmmoStart() {
        return maxAmmoStart;
    }

    // No start or update, because they don't need to be overridden
}
