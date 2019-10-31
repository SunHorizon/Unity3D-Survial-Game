using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameHub : MonoBehaviour
{

    Canvas hub;
    // Use this for initialization
    void Start () {
        hub = gameObject.GetComponent<Canvas>();
    }
	
	// Update is called once per frame
	void Update () {

		if(PauseManager.PausedCheck == true)
        {
            hub.enabled = false;

        }else if (Character.FinishedCheck == true)
        {
            hub.enabled = false;
        }
        else if (Character.DeadCheck == true)
        {
            hub.enabled = false;
        }
        else if (OptionManager.optionManager == true)
        {
            hub.enabled = false;
        }
        else
        {
            hub.enabled = true;
        }
    }
}
