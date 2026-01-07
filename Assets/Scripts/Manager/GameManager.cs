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
    IDragable _currentDragable;
    GameObject _currentDragObj;

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

    private void Update()
    {
        if (InputManager.OnMouseClick())
        {
            Vector2 _mousePosition = InputManager.GetMousePosition();
            RaycastHit2D hit = Physics2D.Raycast(new Vector3(_mousePosition.x, _mousePosition.y, 5f), Vector3.forward);
            if (hit.transform != null && hit.transform.TryGetComponent<IDragable>(out var dragable))
            {
                _currentDragObj = hit.transform.gameObject;
                _currentDragable = dragable;
                _currentDragable.OnDragStart();
            }
        }

        if (InputManager.OnMouseHold() && _currentDragable != null)
        {
            Vector2 _mousePosition = InputManager.GetMousePosition();
            _currentDragable.OnDragging(_mousePosition);
        }

        if (InputManager.OnMouseRelease())
        {
            //_holdingGameObject = null;
            _currentDragable?.OnDragEnd();
            _currentDragable = null;
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
