using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Event")]
public class GameEvent : ScriptableObject
{
    List<Action> _actions = new List<Action>();

    public void Notify()
    {
        foreach (var item in _actions)
        {
            item?.Invoke();
        }
    }
    
    public void Register(Action action)
    {
        if (_actions.Contains(action))
            return;
        _actions.Add(action);
    }

    public void Unregister(Action action) 
    {
        if (!_actions.Contains(action))
            return;
        _actions.Remove(action);
    }
}

public class GameEvent<T> : ScriptableObject
{
    List<Action<T>> _action = new List<Action<T>>();

    public void Notify(T value)
    {
        foreach (var item in _action)
        {
            item?.Invoke(value);
        }
    }

    public void Register(Action<T> action)
    {
        if (_action.Contains(action))
            return;
        _action.Add(action);
    }

    public void Unregister(Action<T> action)
    {
        if (!_action.Contains(action))
            return;
        _action.Remove(action);
    }
}

[CreateAssetMenu(fileName = "EGameStateEvent", menuName = "EGameState Event")]
public class GameStateEvent : GameEvent<EGameState> { }
