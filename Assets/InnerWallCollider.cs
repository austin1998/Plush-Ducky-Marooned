using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerWallCollider : MonoBehaviour
{
    public bool inside;
    public int colliderCount;

    void OnTriggerEnter(Collider other) {
        Debug.Log("touch inner");
        if (gameObject.tag == "InnerWall") {
            gameObject.transform.parent.GetComponentInParent<CaveSoundController>().InnerWallTriggered(other);
        }
    }
}
