using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MenuSceneUI : MonoBehaviour, ISceneUI
{
    [SerializeField] Button _playButton;
    [SerializeField] Button _exitButton;

    bool _isPlayingMusic = true;
    bool _IsMutingSound = false;

    [SerializeField] Button _musicButton;
    [SerializeField] Button _soundButton;

    [SerializeField] Sprite _muteSoundIcon;
    [SerializeField] Sprite _unmuteSoundIcon;
    [SerializeField] Sprite _pauseMusicIcon;
    [SerializeField] Sprite _unpauseMusicIcon;

    private void Start()
    {
        _musicButton.onClick.AddListener(MusicButton);
        _soundButton.onClick.AddListener(SoundButton);
        _playButton.onClick.AddListener(
        () => {
            ButtonAnim(_playButton, OnPlayButtonClicked);
        });

        _exitButton.onClick.AddListener(
        () => {
            ButtonAnim(_exitButton, OnExitButtonClicked);
        });

        UIManager.Instance.Register(this);
    }

    public void OnGameStateChanged(EGameState state)
    {
        switch (state)
        {
            case EGameState.Menu:
                Debug.Log("Menu UI");
                break;
        }
    }

    void OnPlayButtonClicked()
    {
        ScenesController.Instance.LoadScene(EScenes.MainScene, () =>
        {
            GameManager.Instance.ChangeState(EGameState.Play);
        });
    }

    void OnExitButtonClicked()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }

    void ButtonAnim(Button button, Action onComplete = null)
    {
        button.interactable = false;
        Sequence buttonAnim = DOTween.Sequence();

        SoundManager.Instance.Play(ESound.Select);
        buttonAnim.Append(button.transform.DOScale(new Vector3(0.8f, 0.8f, 1f), 0.1f))
            .Append(button.transform.DOScale(new Vector3(1f, 1f, 1f), 0.1f))
            .OnComplete(
                () =>
                {
                    onComplete?.Invoke();
                    button.interactable = true;
                }
            );
    }

    void MusicButton()
    {
        SoundManager.Instance.Play(ESound.Click);
        if (_isPlayingMusic == true)
        {
            PauseMusic();
        }
        else
        {
            UnPauseMusic();
        }
    }

    void SoundButton()
    {
        SoundManager.Instance.Play(ESound.Click);
        if (_IsMutingSound == true)
        {
            UnMuteSound();
        }
        else
        {
            MuteSound();
        }
    }

    void UnPauseMusic()
    {
        _isPlayingMusic = true;
        _musicButton.image.sprite = _unpauseMusicIcon;
        SoundManager.Instance.UnPauseMusic();
    }

    void PauseMusic()
    {
        _isPlayingMusic = false;
        _musicButton.image.sprite = _pauseMusicIcon;
        SoundManager.Instance.PauseMusic();
    }

    void MuteSound()
    {
        _IsMutingSound = true;
        _soundButton.image.sprite = _muteSoundIcon;
        SoundManager.Instance.MuteSound();
    }

    void UnMuteSound()
    {
        _IsMutingSound = false;
        _soundButton.image.sprite = _unmuteSoundIcon;
        SoundManager.Instance.UnMuteSound();
    }
}
