using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckKills : MonoBehaviour {

    public Text playerKillsText;
    public Text playerKillsText2;
    public static int playerkills1;
    public static int playerkills2;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        playerKillsText.text = "Kills: " + playerkills1.ToString();
        Debug.Log(playerkills1);
        Debug.Log(playerkills2);
        playerKillsText2.text = "Kills: " + playerkills2.ToString();
        if(playerkills1 > 5)
        {
            Character.FinishedCheck = true;
            Character.GameKey = true;
            playerkills1 = 0;
        }
        if (playerkills2 > 5)
        {
            Character1.FinishedCheck = true;
            Character1.GameKey = true;
            playerkills2 = 0;
        }
    }
}
