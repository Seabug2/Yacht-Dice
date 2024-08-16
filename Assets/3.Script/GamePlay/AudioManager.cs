using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{

    public AudioMixer audioMixer;
    public AudioSource BGMsound;

    [SerializeField] AudioClip clickclip;
    [SerializeField] AudioClip loseclip;
    [SerializeField] AudioClip winclip;
    [SerializeField] AudioClip BGMclip;

    Slider slider;
   

    private void Awake()
    {
       
        slider = GetComponent<Slider>();
        //BGMSoundPlay(BGMclip);
        //SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    //{
    //    BGMSoundPlay(BGMclip);
    //}

    public void BGMSoundPlay(AudioClip clip)
    {

        BGMsound.outputAudioMixerGroup = audioMixer.FindMatchingGroups("BGM")[0];
        BGMsound.clip = clip;
        BGMsound.loop = true;
        BGMsound.volume = 0.1f;
        BGMsound.Play();
    }
    public void BGMVolume(float val)
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(val) * 20);
    }
    public void MasterVolume(float val)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(val) * 20);
    }
    public void SFXVolume(float val)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(val) * 20);
    }

    //public void WinSoundPlay()
    //{
    //    GameObject go = new GameObject("WinSound");
    //    AudioSource audiosource = go.AddComponent<AudioSource>();
    //    audiosource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("SFX")[0];
    //    audiosource.clip = winclip;
    //    audiosource.Play();
    //
    //    Destroy(go, winclip.length);
    //}
    //public void LoseSoundPlay()
    //{
    //    GameObject go = new GameObject("LoseSound");
    //    AudioSource audiosource = go.AddComponent<AudioSource>();
    //    audiosource.clip = loseclip;
    //    audiosource.Play();
    //
    //    Destroy(go, loseclip.length);
    //}
    //public void ClickSoundPlay()
    //{
    //    GameObject go = new GameObject("clickSound");
    //    AudioSource audiosource = go.AddComponent<AudioSource>();
    //    audiosource.clip = clickclip;
    //    audiosource.Play();
    //
    //    Destroy(go, clickclip.length);
    //}

    public void PlaySFX(AudioClip clip)
    {
        AudioSource audiosource = GetComponent<AudioSource>();
        if (audiosource != null && clip != null)
        {
            audiosource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("SFX")[0];
            audiosource.PlayOneShot(clip);
        }
    }
}
