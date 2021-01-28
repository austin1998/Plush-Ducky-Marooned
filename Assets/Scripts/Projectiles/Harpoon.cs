using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : Projectile
{
    private int enemiesPierced = 0;

    public override void Init() { }

    private void OnTriggerEnter(Collider other)
    {
        HarpoonGun harpoonGun = (HarpoonGun)owner.projectileWeapon;

        if(enemiesPierced >= harpoonGun.enemiesToPierce) { return; }

        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

        if(enemyHealth != null)
        {
            Debug.Log("Hit enemy!!");
            owner.CallOnHitRegistered();
            enemiesPierced++;
            enemyHealth.TakeDamage(Random.Range(owner.projectileWeapon.GetMinWeaponDamage(), owner.projectileWeapon.GetMaxWeaponDamage()));
        }
    }

}
