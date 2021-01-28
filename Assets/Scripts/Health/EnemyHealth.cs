using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : Health
{

    [SerializeField] int enemyPayout = 100;
    // The amount of time between an enemy death and them disappearing
    [SerializeField] float deathTimer = 1.5f;
    [SerializeField] AudioSource deathSound;
    Shop moneyStuff;
    // TODO: Very basic, just to test
    protected override void Die()
    {
        moneyStuff = FindObjectOfType<Shop>();
        GameLogic.Instance.addScore(enemyPayout);
        moneyStuff.AddMoney(enemyPayout);

        // Turn off the enemy AI
        var enemyAI = GetComponent<EnemyController>();
        enemyAI.StopAllCoroutines();
        enemyAI.enabled = false;
        GetComponent<NavMeshAgent>().speed = 0;
        // Play the death sound attatched to this object
        deathSound.Play();
        // Set isDead so no more damage is taken
        isDead = true;
        // Set a timer to wait to destroy the object
        //StartCoroutine(WaitAndDestroy());

        GetComponent<EnemyController>().PlayDeathAnimation();
    }

    //IEnumerator WaitAndDestroy() {
    //    yield return new WaitForSeconds(deathTimer);
    //    Destroy(gameObject);
    //}
}