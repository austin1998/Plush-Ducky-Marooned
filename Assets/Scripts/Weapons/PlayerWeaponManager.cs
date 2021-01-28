using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoSingleton<PlayerWeaponManager>
{

    [SerializeField] Weapon[] standardWeapons;

    // TODO: Power weapons should always be active, just out of view, maybe turn off their models in an anim
    List<PowerWeapon> powerWeapons = new List<PowerWeapon>();

    private Weapon currentlySelectedWeapon;
    private int currentlySelectedWeaponIndex = 0;
    private bool powerWeaponInUse = false;
    private bool allowWeaponSwitching = true;

    // Start is called before the first frame update
    void Start()
    {

        //// FIXME: Change this to only have blunderbuss at start
        //powerWeapons = new List<PowerWeapon>(GetComponentsInChildren<PowerWeapon>());

        currentlySelectedWeapon = standardWeapons[currentlySelectedWeaponIndex];

        // Disable all weapons but the first one
        foreach(Weapon weap in standardWeapons)
        {
            weap.gameObject.SetActive(false);
        }
        currentlySelectedWeapon.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("powerWeaponInUse: " + powerWeaponInUse);

        // If using a power weapon, then return until it is no longer in use
        if(powerWeaponInUse || PauseMenu.Instance.isPaused) { return; }

        // Switching weapons
        if(Input.GetButtonDown("Switch Weapon") && allowWeaponSwitching)
        {
            HandleWeaponSwitch();
        }

        // Attacking
        if(Input.GetButtonDown(currentlySelectedWeapon.GetAttackButtonName()) && currentlySelectedWeapon.GetCanAttack())
        {
            currentlySelectedWeapon.Attack();
            //currentlySelectedWeapon.SetCanAttack(false);
        }

        foreach(PowerWeapon powerWeapon in powerWeapons)
        {
            if(Input.GetButtonDown(powerWeapon.GetAttackButtonName()) && powerWeapon.GetCanAttack() && allowWeaponSwitching)
            {
                // TODO: Replace this with starting an animation that calls this method in an event
                // TODO: Also needs to lock all other weapons, which will need to happen on switch, as well
                // TODO: Probably a good idea to use each weapon's canAttack member for that
                DisableCurrentWeapon();
                powerWeapon.Attack();
                powerWeaponInUse = true;

                Debug.Log("Called the power weapon stuff");
            }
        }

    }

    private void DisableCurrentWeapon()
    {
        currentlySelectedWeapon.gameObject.SetActive(false);
    }

    private void HandleWeaponSwitch()
    {
        // Disable previous weapon
        DisableCurrentWeapon();
        // Go to next weapon
        currentlySelectedWeaponIndex++;
        // Loop if needed
        if(currentlySelectedWeaponIndex >= standardWeapons.Length)
        {
            currentlySelectedWeaponIndex = 0;
        }
        currentlySelectedWeapon = standardWeapons[currentlySelectedWeaponIndex];
        // Enable next weapon
        currentlySelectedWeapon.gameObject.SetActive(true);
    }

    public void SetAllowWeaponSwitching(bool value)
    {
        allowWeaponSwitching = value;
    }

    public void EnableOtherWeapons()
    {
        currentlySelectedWeapon.gameObject.SetActive(true);
        powerWeaponInUse = false;
        Debug.Log("Called EnableOtherWeapons");
    }

    public void AddPowerWeapon(PowerWeapon powerWeapon)
    {
        //Debug.LogError("Power weapon added");
        powerWeapons.Add(powerWeapon);

        switch(powerWeapon.powerWeaponType)
        {
            case PowerWeapon.PowerWeaponTypes.Blunderbuss:
                CooldownIcon.Blunderbuss.cooldownSlider.transform.parent.gameObject.SetActive(true);
                CooldownIcon.Blunderbuss.UpdateSliderValue(0);
                break;
            case PowerWeapon.PowerWeaponTypes.HarpoonGun:
                CooldownIcon.HarpoonGun.cooldownSlider.transform.parent.gameObject.SetActive(true);
                CooldownIcon.HarpoonGun.UpdateSliderValue(0);
                break;
            case PowerWeapon.PowerWeaponTypes.HandCannon:
                CooldownIcon.HandCannon.cooldownSlider.transform.parent.gameObject.SetActive(true);
                CooldownIcon.HandCannon.UpdateSliderValue(0);
                break;
        }
    }

    public Weapon[] GetStandardWeapons() { return standardWeapons; }
    public List<PowerWeapon> GetPowerWeapons() { return powerWeapons; }
}
