using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Blunderbuss : PowerWeapon {

    [SerializeField] int numberOfPelletsToFire = 20;
    [SerializeField] float shotDeviation = 2.0f;

    private Vector3 initialParticlePosition;
    private Quaternion initialParticleRotation;

    private HitscanDamage hitscanDamage;

    protected override void Start()
    {
        base.Start();
        hitscanDamage = GetComponent<HitscanDamage>();
        initialParticlePosition = weaponAttackParticles.transform.localPosition;
        initialParticleRotation = weaponAttackParticles.transform.localRotation;
    }

    protected override void Update() {
        base.Update();
        //if(CDText != null) {
        //    CDText.text = "Blunderbuss CD: " + Mathf.Clamp((timeBetweenAttacks - timeSinceAttacking), 0f, timeBetweenAttacks).ToString("0.00");
        //}
    }

    protected override void LaunchAttack() {

        base.LaunchAttack();

        weaponAttackParticles.Play();
        weaponAttackParticles.transform.parent = null;

        for(int i = 0; i < numberOfPelletsToFire; i++)
        {
            hitscanDamage.ProcessShot(shotDeviation);
        }
    }

    protected override void FinishPowerWeaponAttack()
    {
        base.FinishPowerWeaponAttack();
        weaponAttackParticles.transform.parent = gameObject.transform.GetChild(0).transform;
        weaponAttackParticles.transform.localPosition = initialParticlePosition;
        weaponAttackParticles.transform.localRotation = initialParticleRotation;
    }

}
