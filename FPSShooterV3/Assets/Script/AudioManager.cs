using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {


    public static AudioManager _Instance = null;

    public AudioMixer m_mixer;
    public AudioSource sfxMusic;
    public AudioSetting[] m_audioSettings;
    bool CheckMute;
    float CheckMusicVolume;
    float CheckSFXVolume;
    enum AudioGroups { Music, SFX }

    // Use this for initialization
    void Start () {

        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    public static AudioManager instance
    {
        set { _Instance = value; }
        get { return _Instance; }
    }

    public void PlayMusic(AudioClip clip, float Volume = 1.0f)
    {
        sfxMusic.clip = clip;
        sfxMusic.volume = Volume;
        sfxMusic.Play();
    }

    public void SetMusicVolume(float volume)
    {
        CheckMusicVolume = volume;
        if (CheckMute == false)
        {
            m_audioSettings[(int)AudioGroups.Music].SetExposedParam(volume);
        }

    }
    public void SetSFXVolume(float volume)
    {
        CheckSFXVolume = volume;
        if (CheckMute == false)
        {
            m_audioSettings[(int)AudioGroups.SFX].SetExposedParam(volume);
        }

    }

    public void ToggleVolume(bool volume)
    {
        CheckMute = volume;
        if(CheckMute == true)
        {
            m_audioSettings[(int)AudioGroups.Music].SetExposedParam(-80);
            m_audioSettings[(int)AudioGroups.SFX].SetExposedParam(-80);
        }
        else
        {
            m_audioSettings[(int)AudioGroups.Music].SetExposedParam(CheckMusicVolume);
            m_audioSettings[(int)AudioGroups.SFX].SetExposedParam(CheckSFXVolume);
        }
    }

    [System.Serializable]
    public class AudioSetting
    {
        //public Slider slider;
        //public GameObject redX;
        public string exposedParam;

        public void SetExposedParam(float value)
        {
            AudioManager.instance.m_mixer.SetFloat(exposedParam, value);
            //PlayerPrefs.SetFloat(exposedParam, value);
        }
    }
}
