using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamage : DamageType {

    //[SerializeField] float weaponRange = 1f;
    //[SerializeField] float spherecastRadius = 1f;
    [SerializeField] AudioSource weaponAttackSound;
    [SerializeField] AudioClip weaponAttackClip;

    [Tooltip("Whether the hitbox collider is on this object, or its child")]
    [SerializeField] bool HitboxOnParent = true;

    private Collider hitbox;

    // Used to prevent hitting the same enemy twice
    private List<EnemyHealth> enemiesHitThisSwing = new List<EnemyHealth>();

    //private bool hitboxActive = false;

    protected override void Start() {
        base.Start();

        if(HitboxOnParent)
        {
            hitbox = GetComponent<Collider>();
        }
        else
        {
            // TODO: get the collider in the child
            hitbox = GetComponentInChildren<Collider>();
        }
        hitbox.enabled = false;
    }

    private void SetHitboxActive(int value) {
        if(value == 0) {
            //hitboxActive = false;
            hitbox.enabled = false;
            enemiesHitThisSwing.Clear();
        }
        else {
            //hitboxActive = true;
            hitbox.enabled = true;
        }
    }

    //// Update is called once per frame
    //void FixedUpdate() {
    //    if(!hitboxActive) { return; }

    //    HitDetectionThisFrame();
    //}

    //private void HitDetectionThisFrame() {
    //    //// What we hit with the cast
    //    //RaycastHit hit;

    //    ////// FIXME: Need to tweak and also make more understandable
    //    ////// Used to drag the sphere cast back into the player, need to avoid self collision though
    //    ////Vector3 offsetBackwards = new Vector3(.5f, 0f, .5f);

    //    //// TODO: Add way to hit multiple enemies?
    //    //// First is where to shoot the ray from, next is what direction, then what we hit, and finally the range
    //    //if(Physics.SphereCast(firstPersonCamera.transform.position, spherecastRadius, firstPersonCamera.transform.forward, out hit, weaponRange)) { // If we hit something, and assigns this to "hit"

    //    //    hitboxActive = false;

    //    //    Debug.Log("Hit " + hit.transform.name + " with raycast");

    //    //    EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
    //    //    if(target == null) { return; } // protects against null reference

    //    //    float damageToDeal = UnityEngine.Random.Range(minWeaponDamage, maxWeaponDamage); // Simple damage spread
    //    //    Debug.Log("Deal " + damageToDeal + " damage to " + target.transform.name);

    //    //    weaponAttackSound.PlayOneShot(weaponAttackClip, 0.3f);
    //    //    target.TakeDamage(damageToDeal);
    //    //}
    //    //else { return; } // protects against null reference
    //}

    public void TriggerEntered(Collider other) {

        //if(!hitboxActive) { return; }

        Debug.Log("Hit " + other.name + " with sword");

        EnemyHealth target = other.GetComponent<EnemyHealth>();
        if(target == null) { return; } // protects against null reference

        if(!enemiesHitThisSwing.Contains(target)) {

            enemiesHitThisSwing.Add(target);

            //hitboxActive = false;
            updateWeaponDamage();
            float damageToDeal = UnityEngine.Random.Range(minWeaponDamage, maxWeaponDamage); // Simple damage spread
            Debug.Log("Deal " + damageToDeal + " damage to " + target.transform.name);

            if(weaponAttackClip != null)
            {
                weaponAttackSound.PlayOneShot(weaponAttackClip, 0.3f);
            }
            target.TakeDamage(damageToDeal);

            CallOnHitRegistered();
        }
    }
}
