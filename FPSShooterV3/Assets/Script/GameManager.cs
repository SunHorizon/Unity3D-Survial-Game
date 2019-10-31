using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    Canvas hubTitle;
    Canvas hubOPtion;
    Button startBtn;
    Button quitBtn;
    Button OptionBtn;
    Button BackBtn;
    public AudioClip TitleMusic;
    public AudioSource TitleSource;

    Canvas HubPlayer;
    Button PlayerOne;
    Button PlayerTwo;
    public static bool player;

    //SoundManager sm;

    // Use this for initialization
    void Start () {

        hubTitle = GameObject.Find("Canvas_Title").GetComponent<Canvas>();
        hubOPtion = GameObject.Find("Canvas_Option").GetComponent<Canvas>();
        HubPlayer = GameObject.Find("Canvas_Player").GetComponent<Canvas>();
        hubOPtion.enabled = false;
        HubPlayer.enabled = false;

        if (!TitleSource)
        {
            TitleSource = gameObject.AddComponent<AudioSource>();
            TitleSource.playOnAwake = false;
            TitleSource.loop = false;
            Debug.Log("No AudioSource found on " + TitleSource);
        }
        if (TitleSource)
        {
            TitleSource.clip = TitleMusic;
            TitleSource.volume = 0.5f;
            TitleSource.Play();
            TitleSource.loop = true;
        }

        startBtn = GameObject.Find("Button_Start").GetComponent<Button>();
        OptionBtn = GameObject.Find("Button_Option").GetComponent<Button>();
        BackBtn = GameObject.Find("Button_Back").GetComponent<Button>();
        PlayerOne = GameObject.Find("Button_PlayerOne").GetComponent<Button>();
        PlayerTwo = GameObject.Find("Button_PlayerTwo").GetComponent<Button>();
        GameObject temp = GameObject.Find("Button_Quit");
        if (temp)
        {
            quitBtn = temp.GetComponent<Button>();
        }
        startBtn.onClick.AddListener(StartGame);
        quitBtn.onClick.AddListener(QuitGame);
        OptionBtn.onClick.AddListener(OptionSettings);
        BackBtn.onClick.AddListener(BackToTitle);
        PlayerOne.onClick.AddListener(PlayerOneSettings);
        PlayerTwo.onClick.AddListener(PlayerOTwoSettings);
    }
	

    void OptionSettings()
    {
        hubTitle.enabled = false;
        hubOPtion.enabled = true;
    }
    void BackToTitle()
    {
        hubTitle.enabled = true;
        hubOPtion.enabled = false;
    }

    void PlayerOneSettings()
    {
        TitleSource.Stop();
        player = false;
        SceneManager.LoadScene("Map");
        Time.timeScale = 1;
        //SceneManager.LoadScene(1);
        
    }
    void PlayerOTwoSettings()
    {
        TitleSource.Stop();
        player = true;
        SceneManager.LoadScene("Map");
        Time.timeScale = 1;
        //SceneManager.LoadScene(1);
    }

    public void StartGame()
    {
        hubTitle.enabled = false;
        HubPlayer.enabled = true;
    }

    public void QuitGame()
    {
        TitleSource.Stop();
        Debug.Log("Quitting....");
        Application.Quit();
    }
}
