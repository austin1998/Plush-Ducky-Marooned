using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunHitscanDamage : HitscanDamage
{

    [SerializeField] float bulletRadius = .1f;

    protected override void ProcessShotWithDeviation(float shotDeviationFactor)
    {
        // What we hit with the cast
        RaycastHit hit;

        // Gets a random shot deviation to apply to the center of the screen to make shots not 100% acurrate
        Vector3 shotDeviation = new Vector3(UnityEngine.Random.Range(-shotDeviationFactor, shotDeviationFactor),
            UnityEngine.Random.Range(-shotDeviationFactor, shotDeviationFactor), UnityEngine.Random.Range(-shotDeviationFactor, shotDeviationFactor));

        if(drawRays)
        {
            Debug.DrawRay(firstPersonCamera.transform.position, (firstPersonCamera.transform.forward + shotDeviation) * weaponRange, Color.red, 5.0f);
        }

        // First is where to shoot the ray from, next is what direction, then what we hit, and finally the range
        if(Physics.SphereCast(firstPersonCamera.transform.position, bulletRadius, firstPersonCamera.transform.forward + shotDeviation, out hit, weaponRange, layersToHit))
        { // If we hit something, and assigns this to "hit"

            Debug.Log("Hit " + hit.transform.name + " with raycast");

            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if(target == null) { return; } // protects against null reference
            updateWeaponDamage();
            float damageToDeal = UnityEngine.Random.Range(minWeaponDamage, maxWeaponDamage); // Simple damage spread
            Debug.Log("Deal " + damageToDeal + " damage to " + target.transform.name);

            target.TakeDamage(damageToDeal);

            CallOnHitRegistered();
        }
        else { return; } // protects against null reference


        // Piercing implementation, not using this anymore
        //    // What we hit with the cast
        //    RaycastHit[] hits;

        //    // Gets a random shot deviation to apply to the center of the screen to make shots not 100% acurrate
        //    Vector3 shotDeviation = new Vector3(UnityEngine.Random.Range(-shotDeviationFactor, shotDeviationFactor),
        //        UnityEngine.Random.Range(-shotDeviationFactor, shotDeviationFactor), UnityEngine.Random.Range(-shotDeviationFactor, shotDeviationFactor));

        //    if(drawRays)
        //    {
        //        Debug.DrawRay(firstPersonCamera.transform.position, (firstPersonCamera.transform.forward + shotDeviation) * weaponRange, Color.red, 5.0f);
        //    }

        //    hits = Physics.SphereCastAll(firstPersonCamera.transform.position, bulletRadius, firstPersonCamera.transform.forward + shotDeviation, weaponRange, layersToHit);

        //    // First is where to shoot the ray from, next is what direction, then what we hit, and finally the range
        //    foreach(RaycastHit hit in hits)
        //    {
        //        Debug.Log("Hit " + hit.transform.name + " with raycast");

        //        EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
        //        if(target == null) { return; } // protects against null reference

        //        float damageToDeal = UnityEngine.Random.Range(minWeaponDamage, maxWeaponDamage); // Simple damage spread
        //        Debug.Log("Deal " + damageToDeal + " damage to " + target.transform.name);

        //        target.TakeDamage(damageToDeal);
        //    }
    }
}
