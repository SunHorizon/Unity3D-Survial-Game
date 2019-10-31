using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameFinished : MonoBehaviour {



    CanvasGroup cg;
    Button again;
    Button quit;
    public AudioClip GameWin;
    int playCounter;

    // Use this for initialization
    void Start () {
        again = GameObject.Find("Again_Button1").GetComponent<Button>();
        quit = GameObject.Find("Quit_Button1").GetComponent<Button>();
        cg = GetComponent<CanvasGroup>();
        if (!cg)
        {
            cg = gameObject.AddComponent<CanvasGroup>();
            //cg.alpha = 0.0f;
            cg.blocksRaycasts = false;
            again.interactable = false;
            quit.interactable = false;
        }
        else
        {
            cg.blocksRaycasts = false;
            again.interactable = false;
            quit.interactable = false;
        }
        again.onClick.AddListener(NewGame);
        quit.onClick.AddListener(QuitGame);
    }
	
	// Update is called once per frame
	void Update () {

        if(GameManager.player == false)
        {
            if (Character.FinishedCheck && Character.GameKey)
            {
                playCounter++;
                if (playCounter == 1)
                {
                    AudioManager.instance.PlayMusic(GameWin, 0.5f);
                    AudioManager.instance.sfxMusic.loop = false;
                }
                cg.alpha = 1.0f;
                Time.timeScale = 0;
                cg.blocksRaycasts = true;
                again.interactable = true;
                quit.interactable = true;
                PauseManager.PausedCheck = false;
            }
            if (Character.DeadCheck)
            {
                cg.blocksRaycasts = false;
                again.interactable = false;
                quit.interactable = false;
            }
            if (PauseManager.PausedCheck)
            {
                cg.blocksRaycasts = false;
                again.interactable = false;
                quit.interactable = false;
            }
        }
        else
        {
            if ((Character1.FinishedCheck || Character.FinishedCheck) && (Character.GameKey || Character1.GameKey))
            {
                playCounter++;
                if (playCounter == 1)
                {
                    AudioManager.instance.PlayMusic(GameWin, 0.5f);
                    AudioManager.instance.sfxMusic.loop = false;
                }
                cg.alpha = 1.0f;
                Time.timeScale = 0;
                cg.blocksRaycasts = true;
                again.interactable = true;
                quit.interactable = true;
                PauseManager.PausedCheck = false;
            }
            if (Character.DeadCheck || Character1.DeadCheck)
            {
                cg.blocksRaycasts = false;
                again.interactable = false;
                quit.interactable = false;
            }
            if (PauseManager.PausedCheck)
            {
                cg.blocksRaycasts = false;
                again.interactable = false;
                quit.interactable = false;
            }
        }
       
    }

    public void QuitGame()
    {
        if (GameManager.player == false)
        {
            AudioManager.instance.sfxMusic.Stop();
            //SceneManager.UnloadSceneAsync("Level1");
            cg.alpha = 0.0f;
            Time.timeScale = 1;
            cg.blocksRaycasts = false;
            again.interactable = false;
            quit.interactable = false;
            Character.FinishedCheck = false;
            Character.GameKey = false;
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
            quit.interactable = false;
            Character.FinishedCheck = false;
            Character.GameKey = false;
            Character1.FinishedCheck = false;
            Character1.GameKey = false;
            SceneManager.LoadScene("Title");
        }  
    }

    public void NewGame()
    {
        if (GameManager.player == false)
        {
            cg.alpha = 0.0f;
            Time.timeScale = 1;
            cg.blocksRaycasts = false;
            again.interactable = false;
            quit.interactable = false;
            Character.FinishedCheck = false;
            Character.GameKey = false;
            SceneManager.LoadScene("Map");
        }
        else
        {
            cg.alpha = 0.0f;
            Time.timeScale = 1;
            cg.blocksRaycasts = false;
            again.interactable = false;
            quit.interactable = false;
            Character.FinishedCheck = false;
            Character.GameKey = false;
            Character1.FinishedCheck = false;
            Character1.GameKey = false;
            SceneManager.LoadScene("Map");
        }
       
    }
}
