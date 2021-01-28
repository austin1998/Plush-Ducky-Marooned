using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterWallCollider : MonoBehaviour
{
    public bool inside;
    public int colliderCount;

    void OnTriggerEnter(Collider other) {
        Debug.Log("touch outter");
        if (gameObject.tag == "OuterWall") {
            gameObject.transform.parent.GetComponentInParent<CaveSoundController>().OuterWallTriggered(other);
        }
    }
}
