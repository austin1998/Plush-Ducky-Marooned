using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abstract class that all weapons should inherit from
// Inherit from this class when making a new weapon, like a sword or gun
// This component must be placed alongside the damage type of the weapon
abstract public class Weapon : MonoBehaviour {

    [SerializeField] protected float timeBetweenAttacks;
    // The button in the input manager for firing
    [SerializeField] private string attackButtonName = "Fire1";
    [SerializeField] private float minWeaponDamage = 20;
    [SerializeField] private float maxWeaponDamage = 20;
    [SerializeField] private bool canSwitchDuringCooldown = false;

    [SerializeField] protected ParticleSystem weaponAttackParticles;
    [SerializeField] protected AudioSource weaponAttackSound;
    [SerializeField] protected AudioClip weaponAttackClip;

    [SerializeField] AudioSource weaponReloadSound;
    [SerializeField] AudioClip weaponReloadClip;

    protected bool canAttack = true;
    protected DamageType damageType;

    protected float timeSinceAttacking;

    // Overridden by the specific weapon, which defines its actual attacking behavior
    public virtual void Attack() {
        canAttack = false;

        if(!canSwitchDuringCooldown) {
            PlayerWeaponManager.Instance.SetAllowWeaponSwitching(false);
        }

        //// Not all weapons might actually have these (like melee), so best to check
        //if(weaponAttackParticles != null) {
        //    //weaponAttackParticles.Play();
        //}

        // Play the sound for attacking here
        if(weaponAttackSound != null) {
            weaponAttackSound.PlayOneShot(weaponAttackClip, 0.3f);
        }

        //StartCoroutine(DelayAttack());
        timeSinceAttacking = 0f;
    }

    // Start is called before the first frame update
    protected virtual void Start() {
        damageType = GetComponent<DamageType>();
        if(damageType == null) {
            Debug.LogError("You MUST add a damage type to this object!");
        }

        timeSinceAttacking = timeBetweenAttacks;
    }

    protected virtual void Update() {

        if(canAttack) { return; } // Optimization, as none of this is needed if we can attack

        timeSinceAttacking += Time.deltaTime;

        if(timeSinceAttacking >= timeBetweenAttacks) {
            canAttack = true;
            if (weaponReloadSound != null)
            {
                weaponReloadSound.PlayOneShot(weaponReloadClip, 2f);
            }
            PlayerWeaponManager.Instance.SetAllowWeaponSwitching(true);
        }
    }

    //// TODO: Make this like store itself or something so you can't swap spam
    //private void OnEnable() {
    //    //canAttack = true;
    //    if(!canAttack) {
    //        //StartCoroutine(DelayAttack());
    //    }
    //}

    //protected virtual void Update() {
    //    if(canAttack && Input.GetButtonDown(attackButtonName)) {
    //        //Attack();
    //        //canAttack = false;
    //    }
    //}

    //IEnumerator DelayAttack() {
    //    yield return new WaitForSeconds(timeBetweenAttacks);
    //    canAttack = true;
    //    playerWeaponManager.SetAllowWeaponSwitching(true);
    //}

    public float GetMinWeaponDamage() { return minWeaponDamage; }
    public void SetMinWeaponDamage(float value) { minWeaponDamage = value; }
    public float GetMaxWeaponDamage() { return maxWeaponDamage; }
    public void SetMaxWeaponDamage(float value) { maxWeaponDamage = value; }
    public float GetTimeBetweenAttacks() { return timeBetweenAttacks; }
    public void SetTimeBetweenAttacks(float value) { timeBetweenAttacks = value; }
    public string GetAttackButtonName() { return attackButtonName; }
    public bool GetCanAttack() { return canAttack; }

    //public void
    //public void SetCanAttack(bool value) { this.canAttack = value; }

}
