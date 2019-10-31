using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour {

    public enum SpawnState { SPAWNING, WAITING };

    public Transform[] powerUps;
    public Transform[] PowerUpSpawnPoints;

    public float RateTimer;
    private float Rate;

    private int powerUpsRange;
    private int SpawnRange;
    private float searchCountDown = 1.0f;

    private SpawnState state = SpawnState.WAITING;

    private bool checkWait;
    // Use this for initialization
    void Start () {
       
        if (RateTimer <= 0)
        {
            RateTimer = 30.0f;
            Debug.Log("RateTimer not set. Setting to: " + RateTimer);
        }
        Rate = RateTimer;
        checkWait = false;
    }

    // Update is called once per frame
    void Update() {

        if(!OptionManager.optionManager && !PauseManager.PausedCheck)
        {
            if (state == SpawnState.WAITING)
            {
                if (!PowerUpIsActive())
                {
                    checkWait = true;

                }

            }
            if (checkWait)
            {
                Rate -= Time.fixedDeltaTime;
                //Debug.Log(Rate);
            }

            if (Rate < 0)
            {
                SpawnPowerUp();
                checkWait = false;
                Rate = RateTimer;

            }
        }      
    }


    bool PowerUpIsActive()
    {
        searchCountDown -= Time.deltaTime;
        if (searchCountDown <= 0)
        {
            searchCountDown = 1.0f;
            if (GameObject.FindGameObjectWithTag("PowerUp") == null)
            {
                return false;
            }
        }

        return true;
    }

    void SpawnPowerUp()
    {
        state = SpawnState.SPAWNING;
        if (powerUps.Length > 0)
        {
            powerUpsRange = Random.Range(0, powerUps.Length);
            SpawnRange = Random.Range(0, PowerUpSpawnPoints.Length);
            Instantiate(powerUps[powerUpsRange], PowerUpSpawnPoints[SpawnRange].position, PowerUpSpawnPoints[SpawnRange].rotation);
            Debug.Log("Spawned Power Up" + powerUps[powerUpsRange].name);
        }
        state = SpawnState.WAITING;
    }

}
