using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EGameState
{
    None,
    Menu,
    Play,
    Pause,
    Resume,
    Lose,
    Win,
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] GameEvent<EGameState> _onGameStateChanged;

    EGameState _currentGameState = EGameState.None;

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ChangeState(EGameState gameState)
    {
        if (_currentGameState == gameState)
            return;
        _currentGameState = gameState;
        _onGameStateChanged?.Notify(_currentGameState);
    }
}
