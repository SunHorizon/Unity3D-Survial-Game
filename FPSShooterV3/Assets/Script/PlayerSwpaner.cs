using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwpaner : MonoBehaviour {


    public Transform PlayerOneSpawn;
    public Transform PlayerTwoSpawn;
    public Transform[] PlayerTwo;
    public Transform PlayerOne;

    // Use this for initialization
    void Start () {
		
        if(GameManager.player == true)
        {
            PlayerTwoSpawnCheck();
        }
        else
        {
            PlayerOneSpawnCheck();
        }

	}
	
    void PlayerOneSpawnCheck()
    {
        Instantiate(PlayerOne, PlayerOneSpawn.position, PlayerOneSpawn.rotation);
    }

    void PlayerTwoSpawnCheck()
    {
        Instantiate(PlayerTwo[0], PlayerOneSpawn.position, PlayerOneSpawn.rotation);
        Instantiate(PlayerTwo[1], PlayerTwoSpawn.position, PlayerTwoSpawn.rotation);
    }

}
