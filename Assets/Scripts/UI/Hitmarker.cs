using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitmarker : MonoBehaviour
{
    [SerializeField] string FIRE_TRIGGER = "HitmarkerFire";

    private Animator animator;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        DamageType.OnHitRegistered += HitRegistered;
    }

    private void HitRegistered()
    {
        //Debug.Log("HitReg");
        animator.SetTrigger(FIRE_TRIGGER);
        audioSource.Play();
    }

    private void OnDisable()
    {
        DamageType.OnHitRegistered -= HitRegistered;
    }
}
