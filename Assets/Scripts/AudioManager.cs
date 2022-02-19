using UnityEngine.Audio;
using System;
using UnityEngine;

//Credit to Brackeys youtube tutorial on Audio managers, as the majority of this code and learning how to use it was made by him.


public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManager instance;
    //AudioManager

    private float rate;
    private Sound playing;

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        Play("Theme");

    }

    public void stopOtherSongs()
    {
        foreach (Sound s in sounds)
        {
            if (s.source.isPlaying && ! s.name.Equals("Theme"))
            {
                s.source.Stop();
            }
        }
    }

    public bool isPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s.source.isPlaying)
        {
            return true;
        }
        return false;
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        
        s.source.Play();
        
        return;
    }

    public void Play(string name, float duration)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }

        s.source.Play();
        s.source.volume = 1f;
        fade(name, duration);
        return;
    }

    public void Play(string name, bool late)
    {
        if (late)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found");
                return;
            }

            s.source.Play();
            s.source.volume = 1f;
            LateFade(name);
            return;
        }
    }

    public void LateFade(string name) //fades out last 20% of song
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        float time = s.source.clip.length * 0.9f;
        float left = s.source.clip.length * 0.1f;
        this.playing = s;
        this.rate = 1f / (( left / 0.1f));
        InvokeRepeating("reduce", time, 0.1f);
    }

    

  

   


    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        s.source.Stop();
       
    }


    public void fade(string name, float rate)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        this.rate = 1f / ((rate / 0.1f));
        this.playing = s;
        InvokeRepeating("reduce", 0f, 0.1f);
    }

    private void reduce()
    {
        playing.source.volume -= rate;
        //Debug.Log(playing.source.volume);
        if(playing.source.volume <= 0)
        {
            playing.source.volume = 0;
            playing.source.Stop();
            CancelInvoke();
        }
    }
}