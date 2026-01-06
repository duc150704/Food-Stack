using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESound
{
    None,

    Background,
    
    Select,
    Click,
}

[Serializable]
public struct SoundData
{
    public AudioClip AudioClip;
    public bool IsBackgroundMusic;
    public ESound SoundType;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] GameEvent<EGameState> _onGameStateChanged;

    [SerializeField] AudioSource _musicSource;
    [SerializeField] AudioSource _soundSource;
    [SerializeField] List<SoundData> _soundList = new List<SoundData>();

    Dictionary<ESound, SoundData> _sound = new Dictionary<ESound, SoundData>();

    private void Awake()
    {
        if(Instance != null)
            Destroy(this);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable()
    {
        _onGameStateChanged?.Register(OnGameStateChanged);
    }

    private void OnDisable()
    {
        _onGameStateChanged?.Unregister(OnGameStateChanged);
    }

    private void Start()
    {
        foreach(var sound in _soundList)
        {
            _sound.Add(sound.SoundType, sound);
        }
        _musicSource.volume = 0.8f;
        _soundSource.volume = 0.8f;
    }

    void OnGameStateChanged(EGameState gameState)
    {
        switch (gameState)
        {
            case EGameState.Menu:
                PlayBackgroundMusic(ESound.Background);
                break;
        }
    }

    public void Play(ESound sound)
    {
        if (_sound[sound].IsBackgroundMusic)
            PlayBackgroundMusic(sound);
        else
            PlaySound(sound);
    }

    void PlayBackgroundMusic(ESound sound)
    {
        _musicSource.clip = _sound[sound].AudioClip;
        _musicSource.Play();
    }

    void PlaySound(ESound sound)
    {
        _soundSource.PlayOneShot(_sound[sound].AudioClip);
    }
    
    public void StopMusic()
    {
        _musicSource.Stop();
    }

    public void PauseMusic()
    {
        _musicSource.Pause();
    }

    public void UnPauseMusic()
    {
        _musicSource.UnPause();
    }

    public void MuteSound()
    {
        _soundSource.volume = 0f;
    }

    public void UnMuteSound() 
    {
        _soundSource.volume = 0.8f;
    }
}
