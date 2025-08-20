using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            SetupAudioManager();
        }
        else if (instance != this) 
        {
            Destroy(gameObject);
        }
    }

    public void SetupAudioManager()
    {
        instance = this;
        DontDestroyOnLoad(gameObject); // when new scene is loaded all gameobj are destroyed, this
                                       // prevents that, so we can have the Audioman everywhere
    }

    public AudioSource menuMusic, bossMusic, levelCompleteMusic;
    public AudioSource[] levelTracks;

    public AudioSource[] allSFX;

    void StopMusic()
    {
        menuMusic.Stop();
        bossMusic.Stop();
        levelCompleteMusic.Stop();

        foreach(AudioSource track in levelTracks)
        {
            track.Stop();
        }
    }

    public void PlayMenuMusic()
    {
        StopMusic();
        menuMusic.Play();
    }

    public void PlayBossMusic()
    {
        StopMusic();
        bossMusic.Play();
    }

    public void PlayLevelCompleteMusic()
    {
        StopMusic();
        levelCompleteMusic.Play();
    }

    public void PlayLevelMusic(int trackToPlay)
    {
        StopMusic();
        levelTracks[trackToPlay].Play();    
    }

    public void PlaySFX(int sfxToPlay)
    {
        allSFX[sfxToPlay].Stop();
        allSFX[sfxToPlay].Play();
    }

    public void PlaySFXPitched(int sfxToPlay, float pitchMin = 0.75f, float pitchMax = 1.25f)
    {
        allSFX[sfxToPlay].Stop();

        allSFX[sfxToPlay].pitch = Random.Range(pitchMin, pitchMax);

        allSFX[sfxToPlay].Play();
    }

}

