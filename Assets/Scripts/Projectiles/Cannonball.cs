using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : Projectile
{
    [SerializeField] GameObject explosionVFX;
    [SerializeField] float explosionTime = .5f;
    [SerializeField] float gravityMultiplier = 1f;
    [SerializeField] bool explodeOnContactWithEnemy = true;
    
    float fuse;
    float radius;
    Rigidbody myRigidbody;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        myRigidbody.AddForce(Physics.gravity * (myRigidbody.mass * myRigidbody.mass) * gravityMultiplier);
    }

    public override void Init()
    {
        HandCannon handCannon = (HandCannon)owner.projectileWeapon;
        fuse = handCannon.bombFuse;
        radius = handCannon.bombRadius;
        StartCoroutine(WaitAndExplode());
    }

    IEnumerator WaitAndExplode()
    {
        yield return new WaitForSeconds(fuse);
        Explode();
    }

    private void Explode()
    {
        // VFX, unparent because we're about to destroy the bomb
        explosionVFX.SetActive(true);
        explosionVFX.transform.parent = null;
        Destroy(explosionVFX, explosionTime);

        Collider[] collisions = Physics.OverlapSphere(transform.position, radius);

        foreach(Collider collider in collisions)
        {
            EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth>();
            if(enemyHealth != null)
            {
                float damageToDeal = Random.Range(owner.projectileWeapon.GetMinWeaponDamage(), owner.projectileWeapon.GetMaxWeaponDamage());
                Debug.Log("Do " + damageToDeal + " to " + collider.name);
                enemyHealth.TakeDamage(damageToDeal);
                owner.CallOnHitRegistered();
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 3f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!explodeOnContactWithEnemy) { return; }

        EnemyHealth enemyHealth = collision.collider.GetComponent<EnemyHealth>();
        if(enemyHealth != null)
        {
            StopAllCoroutines();
            Explode();
        }
    }
}
