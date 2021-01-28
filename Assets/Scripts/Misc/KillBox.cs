using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if(playerHealth != null) {
            Debug.Log("Player takes 50 damage");
            playerHealth.TakeDamage(50);
        }
    }

}
