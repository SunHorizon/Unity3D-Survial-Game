using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawnPoint : MonoBehaviour
{

    public GameObject[] Enemy;
    // creating and array of varibles
    public Vector3 spawnValues;
    // this is the bounders of how far and often shpaes can spawn
    public float spawnwaitTime;
    public Transform EnemySpawnPoint1;
    // how often shapes spawn
    public float MostWait;
    //set longest wait period time
    public float leastWait;
    //min wait time
    public int startWaiting;
    // int for waiting 
    public bool stop;
    //int that for stop
    public int EnemySpawned = 0;
    public int maxEnemy;

    int randEnemy;


    // Use this for initialization
    void Start()
    {
        StartCoroutine(waitSpawner());
    }

    // Update is called once per frame
    void Update()
    { 
        spawnwaitTime = Random.Range(leastWait, MostWait);
        if (EnemySpawned >= maxEnemy)
        {
            stop = true;
        }
        else
        {
            stop = false;
        }
        
    }

    IEnumerator waitSpawner()
    {
        yield return new WaitForSeconds(startWaiting);

        while (stop == false)
        // while not set to stop 
        {
            randEnemy = Random.Range(0, 0);
            //this will spawn a randome enemy from 1-3
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y , Random.Range(-spawnValues.z, spawnValues.z));
            Debug.Log(spawnPosition);
    
             Instantiate(Enemy[randEnemy], spawnPosition  + transform.TransformPoint(0, 0, 0) , gameObject.transform.rotation);

            EnemySpawned++;
            yield return new WaitForSeconds(spawnwaitTime);
        }
    }
}
