using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abstract class that all damage types should inherit from
// Inherit from this class when making a new damage type, like hitscan, projectile, or melee
abstract public class DamageType : MonoBehaviour {

    protected Camera firstPersonCamera;
    protected Weapon weapon;
    protected float minWeaponDamage;
    protected float maxWeaponDamage;

    public delegate void HitRegistered();
    public static event HitRegistered OnHitRegistered;

    // Start is called before the first frame update
    protected virtual void Start() {
        firstPersonCamera = Camera.main;
        weapon = GetComponent<Weapon>();
        minWeaponDamage = weapon.GetMinWeaponDamage();
        maxWeaponDamage = weapon.GetMaxWeaponDamage();
    }

    public void updateWeaponDamage() {
        minWeaponDamage = weapon.GetMinWeaponDamage();
        maxWeaponDamage = weapon.GetMaxWeaponDamage();
    }

    public void CallOnHitRegistered()
    {
        if (OnHitRegistered != null)
        {
            OnHitRegistered();
        }
    }
}
