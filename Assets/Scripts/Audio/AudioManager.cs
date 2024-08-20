using UnityEngine;
using System;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] bgmSounds;
    public Sound[] sounds;

    public static AudioManager instance;

    public static float bgMusicVolume = .3f;
    public static float effectsMusicVolume = .5f;

    Sound actualBGM;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in bgmSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    private void Start()
    {
        PlayBGM("MenuLoop");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("No se encontró el audio!");
            return;
        }
        s.source.Play();
    }
    public void PlayBGM(string name)
    {
        actualBGM = Array.Find(bgmSounds, bgmSounds => bgmSounds.name == name);
        if (actualBGM == null)
        {
            Debug.LogError("No se encontró el audio!");
            return;
        }
        actualBGM.source.Play();
    }
    public void updateBGMusic(string newTheme)
    {
        if (actualBGM.name != newTheme)
        {
            actualBGM.source.Stop();
            PlayBGM(newTheme);
            updateBGValume(bgMusicVolume);
        }
    }
    public void updateBGValume(float volume)
    {
        bgMusicVolume = volume;
        actualBGM.source.volume = volume;
    }
    public void updateSfxVolume(float volume)
    {
        effectsMusicVolume = volume;
        foreach (Sound s in sounds)
        {
            s.source.volume = volume;
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            updateBGMusic("MenuLoop");
        }
        else if(SceneManager.GetActiveScene().name == "Playground_Jaime")
        {

            updateBGMusic("FightLoop");
        }
        else if (SceneManager.GetActiveScene().name == "Intro")
        {
            //Bajamos la musica la musica
            updateBGValume(0.1f);
        }
    }

}
