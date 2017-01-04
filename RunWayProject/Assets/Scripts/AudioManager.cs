using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundEffects { ObstacleCollisionSoundEffect, CoinSoundEffect, PowerUpSoundEffect, GameOverSoundEffect, GUITapSoundEffect }

/// <summary>
/// 游戏音效管理
/// </summary>
public class AudioManager : QSingleton<AudioManager>
{
    public AudioClip backgroundMusic;
    public AudioClip obstacleCollision;
    public AudioClip coinCollection;
    public AudioClip powerUpCollection;
    public AudioClip gameOver;
    public AudioClip guiTap;

    public float backgroundMusicVolume;
    public float soundEffectsVolume;

    private AudioSource backgroundAudio;
    private AudioSource soundEffectsAudio;



    public void Start()
    {
        AudioSource[] sources = Camera.main.GetComponents<AudioSource>();
        if(sources == null || sources.Length == 0)
        {
            Debug.Log("ERROR ==> 摄像机上未有音源");
            return;
        }

        backgroundAudio = sources[0];
        soundEffectsAudio = sources[1];

        backgroundAudio.clip = backgroundMusic;
        backgroundAudio.loop = true;
        backgroundAudio.volume = Mathf.Clamp01( backgroundMusicVolume );

        soundEffectsAudio.volume = Mathf.Clamp01( soundEffectsVolume );
    }

    public void playBackgroundMusic( bool play )
    {
        if(backgroundAudio != null)
        {
            if(play)
            {
                backgroundAudio.Play();
            }
            else
            {
                backgroundAudio.Pause();
            }
        }else
        {
            Debug.Log("ERROR ==> 无背景音源");
        }
    }

    public void playSoundEffect( SoundEffects soundEffect )
    {
        AudioClip clip = null;
        float pitch = 1;
        switch(soundEffect)
        {
            case SoundEffects.ObstacleCollisionSoundEffect:
                clip = obstacleCollision;
                break;

            case SoundEffects.CoinSoundEffect:
                clip = coinCollection;
                break;

            case SoundEffects.PowerUpSoundEffect:
                clip = powerUpCollection;
                break;

            case SoundEffects.GameOverSoundEffect:
                clip = gameOver;
                break;

            case SoundEffects.GUITapSoundEffect:
                clip = guiTap;
                break;
        }

        soundEffectsAudio.pitch = pitch;
        soundEffectsAudio.clip = clip;
        soundEffectsAudio.Play();
    }
}
