using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    //[HideInInspector] public int enemiesToPierce;
    [HideInInspector] public ProjectileDamage owner;

    public abstract void Init();

}
