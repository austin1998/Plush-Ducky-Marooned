using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class PowerWeapon : Weapon {

    public enum PowerWeaponTypes { Blunderbuss, HarpoonGun, HandCannon }

    public PowerWeaponTypes powerWeaponType;

    [SerializeField] string ATTACK_TRIGGER_NAME;

    private Animator animator;

    private bool attackFinished = true;

    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
        animator = GetComponent<Animator>();
    }

    public override void Attack() {

        base.Attack();

        Debug.Log("Shoot power weapon!");

        attackFinished = false;

        // Overriding behavior for power weapons
        if(weaponAttackSound != null)
        {
            weaponAttackSound.Stop();
        }

        // Start animation
        if(animator != null)
        {
            animator.SetTrigger(ATTACK_TRIGGER_NAME);
        }
    }

    // Is called by an animation event to actually do the damage and stuff
    protected virtual void LaunchAttack()
    {
        // Play the sound for attacking here
        if(weaponAttackSound != null)
        {
            weaponAttackSound.PlayOneShot(weaponAttackClip, 0.3f);
        }

        StartCoroutine(EnableWeaponsAfterTime());
    }

    // Is called by animation event to finish the attack up
    protected virtual void FinishPowerWeaponAttack() {
        attackFinished = true;
        PlayerWeaponManager.Instance.EnableOtherWeapons();
        Debug.Log("Finished power weapon attack");
    }

    IEnumerator EnableWeaponsAfterTime()
    {
        yield return new WaitForSeconds(1.0f);
        FinishPowerWeaponAttack();
    }

    protected override void Update() {
        base.Update();

        // Makes the cooldown only actually count once the attack is over
        if(!attackFinished) {
            timeSinceAttacking = 0;
        }

        switch(powerWeaponType)
        {
            case PowerWeaponTypes.Blunderbuss:
                CooldownIcon.Blunderbuss.UpdateSliderValue(Mathf.Clamp(1 - (timeSinceAttacking / timeBetweenAttacks), 0f, 1f));
                break;
            case PowerWeaponTypes.HarpoonGun:
                CooldownIcon.HarpoonGun.UpdateSliderValue(Mathf.Clamp(1 - (timeSinceAttacking / timeBetweenAttacks), 0f, 1f));
                break;
            case PowerWeaponTypes.HandCannon:
                CooldownIcon.HandCannon.UpdateSliderValue(Mathf.Clamp(1 - (timeSinceAttacking / timeBetweenAttacks), 0f, 1f));
                break;
        }
        
    }

    public void AddPowerWeapon() {
        PlayerWeaponManager.Instance.AddPowerWeapon(this);
    }
}
