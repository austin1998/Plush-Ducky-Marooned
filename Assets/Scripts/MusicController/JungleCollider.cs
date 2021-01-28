using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JungleCollider : MonoBehaviour
{
    public bool inside;
    public int colliderCount;

    void OnTriggerEnter(Collider other) {
        // Debug.Log("tag: " + gameObject.tag);
        if (gameObject.tag == "Jungle" && other.tag == "Player") {
            gameObject.transform.GetComponentInParent<JungleMusicCounter>().Enter(other);
        }
    }

    void OnTriggerExit(Collider other) {
        //Debug.Log("tag: " + gameObject.tag);
        if (gameObject.tag == "Jungle" && other.tag == "Player") {
            gameObject.transform.GetComponentInParent<JungleMusicCounter>().Exit(other);
        }
    }
}
