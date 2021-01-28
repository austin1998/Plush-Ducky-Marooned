using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This currently only supports ammo for the pistols, which is the only weapon we plan on having limited ammo
public class AmmoPickup : MonoBehaviour {

    [SerializeField] int ammoValue = 5;

    private void OnTriggerEnter(Collider other) {

        // If not player, return
        if(!other.GetComponent<PlayerHealth>()) { return; }

        Pistol.AddAmmo(ammoValue);
        Destroy(gameObject);
    }
}
