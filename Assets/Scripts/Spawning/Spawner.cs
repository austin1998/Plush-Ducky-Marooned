using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject spawnee;
    [SerializeField] bool stopSpawning = false;
    [SerializeField] float spawnTime = 1;
    [SerializeField] float spawnDelay = 5;
    [SerializeField] int maxSpawn;
    [SerializeField] float enemyHealthIncrease = 25.0f;

    private int enemysSpawned = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        //spawnTime = 1;
        //spawnDelay = 5;
        enemysSpawned = 0;
        stopSpawning = false;
        //InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnObject()
    {
        //base.newEnemy();
        if (stopSpawning||maxSpawn <= enemysSpawned)
        {
            CancelInvoke("SpawnObject");
        }
        enemysSpawned++;
        if(maxSpawn <= enemysSpawned - 1)
        {
            stopSpawning = true;
        }

        //needs to be the last thing in the method
        if (!stopSpawning)
        {
            GameObject enemySpawned = Instantiate(spawnee, transform.position, transform.rotation);
            enemySpawned.GetComponent<EnemyHealth>().SetMaxHealth(enemySpawned.GetComponent<EnemyHealth>().GetMaxHealth() + (enemyHealthIncrease * (Rounds.Instance.roundNum - 1)));
        }
        
    }
    
    public void startNewRound()
    {
        stopSpawning = false;
        enemysSpawned = 0;
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
    }

    //getters
    public int getEnemysSpawned()
    {
        return enemysSpawned;
    }

    //setters
    public void setMaxSpawn(int max)
    {
        maxSpawn = max;
    }

    
}
