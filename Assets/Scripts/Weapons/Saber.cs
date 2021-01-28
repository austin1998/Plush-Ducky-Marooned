using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saber : Weapon {

    Animator animator;
    // Special case to programmatically set fire rate, as it is animation based
    [SerializeField] AnimationClip swingAnimation;

    const string SWING_TRIGGER_NAME = "Swing";

    protected override void Start() {
        base.Start();
        animator = GetComponent<Animator>();
        timeBetweenAttacks = swingAnimation.length;
    }

    public override void Attack() {
        base.Attack();
        Debug.Log("Swang saber");
        // FIXME: When we get animations, this is going to need to actually call an animation,
        // FIXME: Which will, in turn, trigger the damage
        //((MeleeDamage)damageType).SetHitboxActive(1);
        animator.SetTrigger(SWING_TRIGGER_NAME);
    }

}
