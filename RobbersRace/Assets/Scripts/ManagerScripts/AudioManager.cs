using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] Sounds;
    // Start is called before the first frame update

    private void Start()
    {
      
        foreach (Sound s in Sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.audioClip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    private void Awake()
    {
        Instance = this;

    }

 
    public void Play(string name)
    {
        Sound s=Array.Find(Sounds, sound => sound.name == name);
        
        s.source.Play();
      
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.name == name);
        s.source.Stop();
    }


}
