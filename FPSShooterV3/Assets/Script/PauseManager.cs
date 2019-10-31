using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour {

    public CanvasGroup OptionCg;
    CanvasGroup cg;
    Button resume;
    Button quit;
    Button option;
    public static bool PausedCheck;
    public static bool BackCheck;

    // Use this for initialization
    void Start () {
        resume = GameObject.Find("Button_Resume").GetComponent<Button>();
        option = GameObject.Find("Button_Option").GetComponent<Button>();
        quit = GameObject.Find("Button_Quit").GetComponent<Button>();
        cg = GetComponent<CanvasGroup>();
        if (!cg)
        {
            cg = gameObject.AddComponent<CanvasGroup>();
            //cg.alpha = 0.0f;
            resume.interactable = false;
            quit.interactable = false;
            option.interactable = false;
            cg.blocksRaycasts = false;
        }
        else
        {
            resume.interactable = false;
            quit.interactable = false;
            option.interactable = false;
            cg.blocksRaycasts = false;
        }
        resume.onClick.AddListener(GameResume);
        option.onClick.AddListener(Options);
        quit.onClick.AddListener(QuitGame);
    }


    // Update is called once per frame
    void Update()
    {
        if ((Character.FinishedCheck == false && Character.DeadCheck == false))
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                resume.interactable = true;
                quit.interactable = true;
                option.interactable = true;
                PauseGame();
                cg.blocksRaycasts = true;
                //PausedCheck = true;
            }
            if(BackCheck == true)
            {
                resume.interactable = true;
                quit.interactable = true;
                option.interactable = true;
                cg.blocksRaycasts = true;
                BackCheck = false;
            }
        }
        if (Character.FinishedCheck)
        {
            cg.blocksRaycasts = false;
            resume.interactable = false;
            option.interactable = false;
            quit.interactable = false;
            PausedCheck = false;
        }
        if (Character.DeadCheck)
        {
            cg.blocksRaycasts = false;
            resume.interactable = false;
            option.interactable = false;
            quit.interactable = false;
            PausedCheck = false;
        }

       
    }

    public void QuitGame()
    {
        AudioManager.instance.sfxMusic.Stop();
        //SceneManager.UnloadSceneAsync("Level1");
        SceneManager.LoadScene("Title");
        resume.interactable = false;
        quit.interactable = false;
        option.interactable = false;
        cg.blocksRaycasts = false;
        PausedCheck = false;
    }

    public void GameResume()
    {
        cg.alpha = 0.0f;
        Time.timeScale = 1;
        resume.interactable = false;
        quit.interactable = false;
        option.interactable = false;
        cg.blocksRaycasts = false;
        PausedCheck = false;
    }

    void Options()
    {
        cg.alpha = 0.0f;
        Time.timeScale = 0;
        resume.interactable = false;
        quit.interactable = false;
        option.interactable = false;
        cg.blocksRaycasts = false;
        PausedCheck = false;

        OptionManager.optionManager = true;
        OptionCg.blocksRaycasts = true;
        OptionCg.alpha = 1.0f;
    }

    void PauseGame()
    {
        if (PausedCheck == true && Input.GetKeyDown(KeyCode.Escape) && OptionManager.optionManager == false)
        {
            PausedCheck = false;
        }

        if (cg.alpha == 0.0f && OptionManager.optionManager == false)
        {
            cg.alpha = 1.0f;
            Debug.Log("Checking for timeStep does this even work");
            Time.timeScale = 0;
            PausedCheck = true;
        }
        else
        {
            if(OptionManager.optionManager == false)
            {
                PausedCheck = false;
                resume.interactable = false;
                quit.interactable = false;
                option.interactable = false;
                cg.blocksRaycasts = false;
                cg.alpha = 0.0f;
                Time.timeScale = 1;
            }

        }
    }

}
