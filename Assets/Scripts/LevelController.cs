using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] List<LevelData> _levelDatas = new List<LevelData>();
    [SerializeField] GameEvent<EGameState> _onGameStateChangeed;
    [SerializeField] Board _board;

    int _currentLevel = 0;

    private void OnEnable()
    {
        _onGameStateChangeed?.Register(OnChangedGameState);
    }

    private void OnDisable()
    {
        _onGameStateChangeed?.Unregister(OnChangedGameState);
    }

    private void OnChangedGameState(EGameState state)
    {
        switch (state) 
        {
            case EGameState.Play:
                LoadData();
                break;
        }
    }

    private void LoadData()
    {
        _board.InitBoard(_levelDatas[_currentLevel].BoardWidth, _levelDatas[_currentLevel].BoardHeight, _levelDatas[_currentLevel].Food);
        _board.InitFood();
    }

    private void NextLevel() => _currentLevel++;
}
