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
    public AudioSource audiosource;

    [SerializeField] AudioClip clickclip;
    [SerializeField] AudioClip loseclip;
    [SerializeField] AudioClip winclip;
    [SerializeField] AudioClip BGMclip;

    Slider slider;
   

    private void Awake()
    {
        AudioSource audiosource = gameObject.GetComponent<AudioSource>();
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

        audiosource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("BGM")[0];
        audiosource.clip = clip;
        audiosource.loop = true;
        audiosource.volume = 0.1f;
        audiosource.Play();
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
        if (clip != null)
        {
            audiosource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("SFX")[0];
            audiosource.PlayOneShot(clip);
        }
    }
}
