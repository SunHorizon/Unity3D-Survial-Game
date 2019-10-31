using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLose : MonoBehaviour {

    CanvasGroup cg;
    Button again;
    Button Quit;
    public AudioClip GameOver;
    int playCounter;

    // Use this for initialization
    void Start () {

        playCounter = 0;
        again = GameObject.Find("Again_Button2").GetComponent<Button>();
        Quit = GameObject.Find("Quit_Button2").GetComponent<Button>();
        //cg.blocksRaycasts = false;
        cg = GetComponent<CanvasGroup>();
        if (!cg)
        {
            cg = gameObject.AddComponent<CanvasGroup>();
            //cg.alpha = 0.0f;
            cg.blocksRaycasts = false;
            again.interactable = false;
            Quit.interactable = false;
        }
        else
        {
            cg.blocksRaycasts = false;
            again.interactable = false;
            Quit.interactable = false;

        }
        again.onClick.AddListener(NewGame);
        Quit.onClick.AddListener(QuitGame);
    }
	
	// Update is called once per frame
	void Update () {

        if (GameManager.player == false)
        {
            if (Character.DeadCheck)
            {
                playCounter++;
                if (playCounter == 1)
                {
                    AudioManager.instance.PlayMusic(GameOver, 0.5f);
                    AudioManager.instance.sfxMusic.loop = false;
                }
                cg.alpha = 1.0f;
                Time.timeScale = 0;
                cg.blocksRaycasts = true;
                again.interactable = true;
                Quit.interactable = true;
                PauseManager.PausedCheck = false;
            }
            if (Character.FinishedCheck)
            {
                cg.alpha = 0.0f;
                Time.timeScale = 0;
                cg.blocksRaycasts = false;
                again.interactable = false;
                Quit.interactable = false;
            }
            if (PauseManager.PausedCheck)
            {
                cg.blocksRaycasts = false;
                again.interactable = false;
                Quit.interactable = false;
            }
        }
        else
        {
            if (Character.DeadCheck || Character1.DeadCheck)
            {
                playCounter++;
                if (playCounter == 1)
                {
                    AudioManager.instance.PlayMusic(GameOver, 0.5f);
                    AudioManager.instance.sfxMusic.loop = false;
                }
                cg.alpha = 1.0f;
                Time.timeScale = 0;
                cg.blocksRaycasts = true;
                again.interactable = true;
                Quit.interactable = true;
                PauseManager.PausedCheck = false;
            }
            if (Character.FinishedCheck || Character1.FinishedCheck)
            {
                cg.alpha = 0.0f;
                Time.timeScale = 0;
                cg.blocksRaycasts = false;
                again.interactable = false;
                Quit.interactable = false;
            }
            if (PauseManager.PausedCheck)
            {
                cg.blocksRaycasts = false;
                again.interactable = false;
                Quit.interactable = false;
            }
        }
       
    }


    public void QuitGame()
    {
        if (Character.DeadCheck)
        {
            AudioManager.instance.sfxMusic.Stop();
            //SceneManager.UnloadSceneAsync("Level1");
            cg.alpha = 0.0f;
            Time.timeScale = 1;
            cg.blocksRaycasts = false;
            again.interactable = false;
            Quit.interactable = false;
            Character.DeadCheck = false;
            Character.GameKey = false;
            Character.Health = 100;
            SceneManager.LoadScene("Title");
        }
        else
        {
            AudioManager.instance.sfxMusic.Stop();
            //SceneManager.UnloadSceneAsync("Level1");
            cg.alpha = 0.0f;
            Time.timeScale = 1;
            cg.blocksRaycasts = false;
            again.interactable = false;
            Quit.interactable = false;
            Character.DeadCheck = false;
            Character.GameKey = false;
            Character.Health = 100;
            Character1.DeadCheck = false;
            Character1.GameKey = false;
            Character1.Health = 100;
            SceneManager.LoadScene("Title");
        }
    
    }

    public void NewGame()
    {
        if (Character.DeadCheck)
        {
            cg.alpha = 0.0f;
            Time.timeScale = 1;
            cg.blocksRaycasts = false;
            again.interactable = false;
            Quit.interactable = false;
            Character.DeadCheck = false;
            Character.GameKey = false;
            Character.Health = 100;
            SceneManager.LoadScene("Map");
        }
        else
        {
            cg.alpha = 0.0f;
            Time.timeScale = 1;
            cg.blocksRaycasts = false;
            again.interactable = false;
            Quit.interactable = false;
            Character.DeadCheck = false;
            Character.GameKey = false;
            Character.Health = 100;
            Character1.DeadCheck = false;
            Character1.GameKey = false;
            Character1.Health = 100;
            SceneManager.LoadScene("Map");
        }
    
    }
}
