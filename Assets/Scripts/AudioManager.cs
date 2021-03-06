using UnityEngine.Audio;
using UnityEngine;
using System;
public class AudioManager : Singleton<AudioManager>
{
    public Sound[] sounds;
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }

    }
    void Start()
    {
        Play("Day1");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
    public void PlayOneShot(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.PlayOneShot(s.clip);
    }

    
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
       
    }
}
