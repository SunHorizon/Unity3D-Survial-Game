using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {
    
    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform[] enemy;
        public Transform[] Boss;
        public int count;
        public float rate;

    }

    public Wave[] waves;
    private int nextWave = 0;

    int randEnemy;

    public float timeBetweenWaves = 5.0f;
    public float waveCountDown;
    private int UIWaveCount;

    public Transform[] spawnPoints;

    private float searchCountDown = 1.0f;

    private SpawnState state = SpawnState.COUNTING;


    //UI Stuff
    public Text WaveNameText;
    public Text WaveCountText;
    private bool wavesDone;

    // Use this for initialization
    void Start () {
        waveCountDown = timeBetweenWaves;
        wavesDone = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (!wavesDone)
        {
            if (state == SpawnState.WAITING)
            {
                if (!EnemyIsAlive())
                {
                    WaveCompleted();

                }
                else
                {
                    return;
                }
            }
            if (waveCountDown <= 0)
            {
                if (state != SpawnState.SPAWNING)
                {
                    StartCoroutine(SpwanWave(waves[nextWave]));
                    WaveCountText.text = "";
                }
            }
            else
            {
                waveCountDown -= Time.deltaTime;
                UIWaveCount = (int)waveCountDown;
                WaveCountText.text = "Next Wave: " + UIWaveCount.ToString();
            }
        }
        else
        {
            WaveCountText.text = "All waves complete";
            if (Character.GameKey)
            {
                Character.FinishedCheck = true;
            }
        }
       
	}

    void WaveCompleted()
    {
        Debug.Log("Wave Completed!");

        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length - 1)
        {      
            wavesDone = true;
            Debug.Log("All waves complete");
            WaveCountText.text = "All waves complete";
        }
        else
        {
            Gun.TotalAmmo = 90;
            nextWave++;
        }
    }
    bool EnemyIsAlive()
    {
        searchCountDown -= Time.deltaTime;
        if(searchCountDown <= 0)
        {
            searchCountDown = 1.0f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
       
        return true;
    }

    IEnumerator SpwanWave (Wave _Wave)
    {
        Debug.Log("Spawning Wave:" + _Wave.name);
        WaveNameText.text = _Wave.name;
        state = SpawnState.SPAWNING;

        if(_Wave.Boss.Length > 0)
        {
            for (int j = 0; j < _Wave.Boss.Length; j++)
            {
                SpawnEnemy(_Wave.Boss[j]);
            }
        }
      
        for (int i = 0; i < _Wave.count; i++)
        {
            if(_Wave.enemy.Length == 1)
            {
                SpawnEnemy(_Wave.enemy[0]);
            }
            else
            {
                randEnemy = Random.Range(0, _Wave.enemy.Length);
                SpawnEnemy(_Wave.enemy[randEnemy]);
            }
            
            yield return new WaitForSeconds(1.0f / _Wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Debug.Log("Spawning Enemy" + _enemy.name);

        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);        
    }


}
