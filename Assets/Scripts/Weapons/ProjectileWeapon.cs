using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : PowerWeapon
{
    public float projectileVelocity = 5.0f;
    private ProjectileDamage projectileDamage;

    protected override void Start()
    {
        base.Start();
        projectileDamage = GetComponent<ProjectileDamage>();
    }
    protected override void LaunchAttack()
    {
        base.LaunchAttack();

        projectileDamage.LaunchProjectile();
    }
}
