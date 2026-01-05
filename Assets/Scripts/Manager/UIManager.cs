using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEditor.U2D;
using System;
using UnityEditor;

public interface ISceneUI
{
    void OnGameStateChanged(EGameState state);
}
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    ISceneUI _currentUI;

    [SerializeField] GameObject _sceneCover;

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

    private void Start()
    {
        GameManager.OnGameStateChanged += OnChangedState;
        Invoke("Init", 0.2f);
    }

    public void Register(ISceneUI ui) =>
        _currentUI = ui;

    public void OnChangedState(EGameState state)
    {
        _currentUI?.OnGameStateChanged(state);
    }

    public void Init() =>
        GameManager.Instance.ChangeState(EGameState.Menu);

    public void StartCover() =>
        _sceneCover.transform.DOScale(new Vector3(30, 30, 1), 0.3f);

    public void EndCover() =>
        _sceneCover.transform.DOScale(new Vector3(0, 0, 1), 0.3f);
}