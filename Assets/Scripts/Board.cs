using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Board : MonoBehaviour
{
    [SerializeField] float _width;
    [SerializeField] float _height;

    [SerializeField] Vector2 _offset = Vector2.zero;

    [SerializeField] Sprite _bg1;
    [SerializeField] Sprite _bg2;

    [SerializeField] GameObject _obj;
    [SerializeField] GameEvent<EGameState> _onGameStateChanged;
    List<Vector2> _foodPosition = new();
    public List<Vector2> FoodPosition
    {
        get => _foodPosition;
    }

    private void OnEnable()
    {
        _onGameStateChanged?.Register(OnGameStateChanged);
    }

    private void OnDisable()
    {
        _onGameStateChanged?.Unregister(OnGameStateChanged);
    }

    void OnGameStateChanged(EGameState state)
    {
        switch (state)
        {
            case EGameState.Play:
                InitBoard();
                InitFood();
                break;
        }
    }

    void InitFood()
    {
        foreach (var item in _foodPosition)
        {
            Instantiate(_obj, item, Quaternion.identity);
        }
    }
    void InitBoard()
    {
        float horizontalCenter = (_width - 1) / 2;
        float verticalCenter = (_height - 1) / 2;

        Vector2 position = new Vector2(horizontalCenter, verticalCenter);
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                if ((i + j) % 2 == 0)
                {
                    GameObject go = new GameObject("Cell");
                    go.AddComponent<SpriteRenderer>();
                    go.GetComponent<SpriteRenderer>().sprite = _bg1;
                    go.transform.position = new Vector2(i, j) - position + _offset;

                    _foodPosition.Add(new Vector2(go.transform.position.x, go.transform.position.y));
                    go.transform.SetParent(this.transform);
                }
                else 
                {
                    GameObject go = new GameObject("Cell");
                    go.AddComponent<SpriteRenderer>();
                    go.GetComponent<SpriteRenderer>().sprite = _bg2;
                    go.transform.position = new Vector2(i, j) - position + _offset;

                    _foodPosition.Add(new Vector2(go.transform.position.x, go.transform.position.y));
                    go.transform.SetParent(this.transform);
                }
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        float horizontalCenter = (_width - 1) / 2;
        float verticalCenter = (_height - 1) / 2;

        Vector2 position = new Vector2(horizontalCenter, verticalCenter);
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                if ((i + j) % 2 == 0)
                {
                    Gizmos.DrawSphere(new Vector2(i, j) - position + _offset, 0.1f);
                }
                else
                {
                    Gizmos.DrawSphere(new Vector2(i, j) - position + _offset, 0.1f);
                }
            }
        }
    }
}
