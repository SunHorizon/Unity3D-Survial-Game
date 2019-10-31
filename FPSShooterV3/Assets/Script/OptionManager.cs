using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionManager : MonoBehaviour {

    public CanvasGroup pauseCg;
    CanvasGroup cg;
    Button Back;
    Slider Volume;
    Slider SFXVolume;
    Toggle mute;

    public static bool optionManager;
    // Use this for initialization
    void Start () {
        Back = GameObject.Find("Button_Back").GetComponent<Button>();
        Volume = GameObject.Find("Slider_Volume").GetComponent<Slider>();
        SFXVolume = GameObject.Find("Slider_SFXVolume").GetComponent<Slider>();
        mute = GameObject.Find("Toggle_Mute").GetComponent<Toggle>();
        cg = GetComponent<CanvasGroup>();
        if (!cg)
        {
            cg = gameObject.AddComponent<CanvasGroup>();
            Back.interactable = false;
            mute.interactable = false;
            cg.blocksRaycasts = false;
            Volume.interactable = false;
            SFXVolume.interactable = false;
        }
        else
        {
            Back.interactable = false;
            mute.interactable = false;
            cg.blocksRaycasts = false;
            Volume.interactable = false;
            SFXVolume.interactable = false;
        }
        Back.onClick.AddListener(BacktoPause);
    }

    // Update is called once per frame
    void Update () {

		if(optionManager == true)
        {
            Back.interactable = true;
            Volume.interactable = true;
            mute.interactable = true;
            cg.blocksRaycasts = true;
            SFXVolume.interactable = true;
        }
	}

    void BacktoPause()
    {
        cg.alpha = 0.0f;
        Time.timeScale = 0;
        cg.blocksRaycasts = false;
        Back.interactable = false;
        mute.interactable = false;
        Volume.interactable = false;
        SFXVolume.interactable = false;

        optionManager = false;
        pauseCg.alpha = 1.0f;
        pauseCg.interactable = true;
        PauseManager.PausedCheck = true;
        PauseManager.BackCheck = true;
    }
}
