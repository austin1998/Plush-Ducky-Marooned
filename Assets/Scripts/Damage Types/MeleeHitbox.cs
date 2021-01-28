using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHitbox : MonoBehaviour
{
    [SerializeField] private MeleeDamage meleeDamage;

    private void OnTriggerEnter(Collider other)
    {
        meleeDamage.TriggerEntered(other);
    }
}
