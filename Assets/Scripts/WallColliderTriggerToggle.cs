using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallColliderTriggerToggle : MonoBehaviour
{
    [Tooltip("Whether or not these colliders should be triggers")]
    [SerializeField] bool isTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Collider collider in GetComponentsInChildren<Collider>())
        {
            collider.isTrigger = this.isTrigger;
        }
    }
}
