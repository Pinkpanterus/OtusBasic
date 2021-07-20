using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySound : MonoBehaviour
{
    public static PlaySound instance;
    
    private AudioSource[] audioSources;
    
    [SerializeField] 
    private List<DataSound> dataSounds = new List<DataSound>();

    
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
 
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        
        audioSources = GetComponents<AudioSource>();
    }

    public void Play(string clipName, int audioSourceIndex)
    {
        var audioClip = GetAudioClip(clipName);
        audioSources[audioSourceIndex].clip = audioClip;
        audioSources[audioSourceIndex].Play();
    }

    public void Stop(int audioSourceIndex)
    {
        audioSources[audioSourceIndex].Stop();
    }

    public void SetLoop(bool isLoop, int audioSourceIndex)
    {
        audioSources[audioSourceIndex].loop = isLoop;
    }

    private AudioClip GetAudioClip(string clipName)
    {
        AudioClip clip = null;
        
        foreach (var sound in dataSounds)
        {
            if (sound.name == clipName)
                clip = sound.audioClip;
        }

        return clip;
    }

    [Serializable]
    private class DataSound
    {
        public string name;
        public AudioClip audioClip;
    }
}
