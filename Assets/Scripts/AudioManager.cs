using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] sounds;

    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Sound with name " + name + " not found!");
            return;
        }
        sound.source.Play();
    }

    public void ControlBackgroundVolume(float volume)
    {
        Debug.Log("Vol: " + volume);
        Sound sound = Array.Find(sounds, s => s.name == "Background Music");
        if (sound == null)
        {
            Debug.LogWarning("Sound with name " + name + " not found!");
            return;
        }
        sound.source.volume = volume;
    }

    public float GetBackgroundVolume()
    {
        Sound sound = Array.Find(sounds, s => s.name == "Background Music");
        if (sound == null)
        {
            Debug.LogWarning("Sound with name " + name + " not found!");
            return -1;
        }
        return sound.source.volume;
    }

    public void ControlGameSoundVolume(float volume)
    {
        Debug.Log("Game Vol: " + volume);
        //Sound sound = Array.Find(sounds, s => s.name != "Background Music");
        foreach (Sound sound in sounds)
        {
            if (sound.name != "Background Music")
                sound.source.volume = volume;
        }
        FindObjectOfType<AudioManager>().Play("Button Click");
    }

    public float GetGameSoundVolume()
    {
        // Find any game sound volume other than Background Music since all of them are at same volume
        Sound sound = Array.Find(sounds, s => s.name != "Background Music");
        if (sound == null)
        {
            Debug.LogWarning("Sound with name " + name + " not found!");
            return -1;
        }
        return sound.source.volume;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // destroy if there is another audio manager for the scene
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
    // Start is called before the first frame update
    void Start()
    {
        Play("Background Music");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
