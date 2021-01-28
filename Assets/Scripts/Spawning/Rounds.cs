using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
//using System.Media;
using System.Collections.Specialized;
using System.IO;

public class Rounds : MonoSingleton<Rounds>
{

    [SerializeField] Spawner[] spawners;
    [SerializeField] int roundPayout = 1000;
    [SerializeField] TextMeshProUGUI roundText;
    [SerializeField] TextMeshProUGUI roundTitle;
    [SerializeField] TextMeshProUGUI scoreAdditionText;

    private int newEnemyCount = 0;
    [SerializeField] public int roundNum = 0;
    private float timeBetweenChecks = 0.0f;
    [SerializeField] private float timeBetweenRounds = 30.0f;
    private float timerForRounds = 0.0f;
    private bool roundSkip = false;
    private float dealTime = 0.0f;
    private float timeBetweenSpace = 0.0f;
    private int roundCheck = 0;

    public List<GameObject> enemysAlive = new List<GameObject>();
    Shop moneyStuff;

    // Used to update the round countdown
    public static Action<int> OnCountdownChanged;
    public static Action OnCountdownFinished;
    public static Action OnCountdownStarted;

    void Start()
    {
        //spawners.AddRange(GameObject.FindGameObjectsWithTag("Spawner"));
        spawners = FindObjectsOfType<Spawner>();
        moneyStuff = FindObjectOfType<Shop>();
        Debug.Log("got all spawners: " + spawners.Length);
        enemysAlive.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        //newRound();

        // Updates the round text
        roundText.text = "Round " + roundNum;
        roundTitle.enabled = false;
        scoreAdditionText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //does every second
        dealTime = Time.deltaTime;
        timeBetweenChecks += dealTime;
        if ((timeBetweenChecks >= 1.0f))
        {
            //Debug.Log("Time between checks ellapsed");
            timeBetweenChecks = 0.0f;
            enemysAlive.Clear();
            enemysAlive.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
            //Debug.Log("Enemy count updated " + enemysAlive.Count);
            if (enemysAlive.Count <= 0 && roundCheck == roundNum)
            {
                timerForRounds += 1;

                OnCountdownChanged((int)(timeBetweenRounds - timerForRounds));
                if(roundNum != 0)
                {
                    OnCountdownStarted();
                }

                //tell the player its an itermition 
                if ((timerForRounds >= timeBetweenRounds) || roundNum == 0)
                {
                    timerForRounds = 0;
                    newRound();
                }
                

            }
        }
        
        timeBetweenSpace += dealTime;
        if(timeBetweenSpace >= 10.0f)
        {
            timeBetweenSpace = 0.0f;
            roundCheck = roundNum;
        }
        if (enemysAlive.Count <= 0)
        {
            timeBetweenSpace = 0.0f;
            if(roundCheck == roundNum && roundNum != 0)
            {
                roundSkip = true;
            }

            if (Input.GetKeyDown("space") && roundSkip)
            {
                roundSkip = false;
                timerForRounds = 0;
                newRound();
            }
        }
    }

    void newRound()
    {

        OnCountdownFinished();

        FindObjectOfType<TreasureHuntMain>().roll();
        GameLogic.Instance.addScore(roundPayout);
        moneyStuff.AddMoney(roundPayout);
        roundNum++;
        Debug.Log("Round number " + roundNum);

        // Updates the round text
        roundText.text = "Round " + roundNum;
        StartCoroutine(ShowRoundTitle("Round " + roundNum, 2));
        calculateEnemyAmount();
        Spawner[] currentSpawns = findBestSpawns();
        for (int i = 0; i < spawners.Length; i++)
        {
            //add if in range of player here
            spawners[i].setMaxSpawn(0);
        }
        for (int i = 0; i < currentSpawns.Length; i++)
        {
            //add if in range of player here
            currentSpawns[i].setMaxSpawn(newEnemyCount);
        }
        for (int i = 0; i < spawners.Length; i++)
        {
            //add if in range of player here
            spawners[i].startNewRound();
        }

    }

    /*public void newEnemy()
    {
        enemysLeft++;
        
    }*/

    // Finds the two closest spawns to the player
    Spawner[] findBestSpawns()
    {

        Vector3 playerPos = GameObject.FindWithTag("MainCamera").transform.position;
        float distance = 0;
        Spawner[] spawnHold = new Spawner[3];
        Spawner[] sends = new Spawner[2];
        float[] returnsDistance = { float.MaxValue, float.MaxValue, float.MaxValue };
        for (int i = 0; i < spawners.Length; i++)
        {
            if(playerPos.y >= spawners[i].transform.position.y - 10 && playerPos.y <= spawners[i].transform.position.y + 12)
            {
                distance = Vector3.Distance(playerPos, spawners[i].transform.position);
            }
            else
            {
                distance = Vector3.Distance(playerPos, spawners[i].transform.position) * 5;
            }
                
            float maximum = Mathf.Max(returnsDistance);
            if (maximum > distance)
            {
                // Found a closer spawn, update
                spawnHold[Array.IndexOf(returnsDistance, maximum)] = spawners[i];
                returnsDistance[Array.IndexOf(returnsDistance, maximum)] = distance;
            }
        }
        float minimum = Mathf.Min(returnsDistance);
        int j = 0;
        for (int i = 0; i < spawnHold.Length; i++)
        {
            if(Array.IndexOf(returnsDistance, minimum) != i)
            {
                sends[j] = spawnHold[i];
                j++;
            }
            
        }
        return sends;
    }

    void calculateEnemyAmount()
    {
        newEnemyCount = (int)(Mathf.Ceil(Mathf.Floor(80.0f * Mathf.Pow(1.02f, roundNum)) - 78.0f) / 2) + 1;
        Debug.Log("Max enemys: " + newEnemyCount * 2);
    }
    IEnumerator ShowRoundTitle(string message, float delay)
    {
        roundTitle.text = message;
        roundTitle.enabled = true;
        scoreAdditionText.enabled = true;
        yield return new WaitForSeconds(delay);
        roundTitle.enabled = false;
        scoreAdditionText.enabled = false;
    }
}
