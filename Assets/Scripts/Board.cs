using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Board : MonoBehaviour
{
    [SerializeField] Vector2 _offset = Vector2.zero;
    [SerializeField] float _fallDownSpeed;

    [SerializeField] Sprite _bg1;
    [SerializeField] Sprite _bg2;

    float _verticalCenter;
    float _horizontalCenter;
    int _height;
    int _width;

    GameObject _food;
    Food[,] _foods;

    private void Update()
    {
        if (InputManager.OnMouseRelease())
        {
            FoodFall();
        }
    }

    public Vector2 CellPositionToWorldPosition(int x, int y)
    {
        return new Vector2(x - _horizontalCenter, y - _verticalCenter) + _offset;
    }

    public void InitBoard(int width, int height, GameObject food)
    {
        _foods = new Food[ width, height];
        _height = height;
        _width = width;
        _food = food;

        _horizontalCenter = (width * 1f - 1) / 2;
        _verticalCenter = (height * 1f - 1) / 2;

        Vector2 position = new Vector2(_horizontalCenter, _verticalCenter);
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject go = new GameObject("Cell");
                go.AddComponent<SpriteRenderer>();
                go.GetComponent<SpriteRenderer>().sprite = (i + j) % 2 == 0 ? _bg1 : _bg2;
                go.transform.position = new Vector2(i, j) - position + _offset;

                go.transform.SetParent(this.transform);
            }
        }
    }

    public void InitFood()
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                if (_foods[i, j] == null)
                {
                    CreateFood(i, j);
                }
            }
        }
    }

    void CreateFood(int i, int j)
    {
        GameObject food = Instantiate(_food, CellPositionToWorldPosition(i, j), Quaternion.identity);
        _foods[i, j] = food.GetComponent<Food>();
    }

    void FoodFall()
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++) // duyet qua bang
            {
                if (_foods[i, j] == null) // neu food =[i, j] null, lay food[i, j +1] keo xuong;
                {
                    for (int newJ = j + 1; newJ < _height; newJ++)
                    {
                        if (_foods[i, newJ] != null)
                        {
                            _foods[i, newJ].FallDown(CellPositionToWorldPosition(i,j), _fallDownSpeed);
                            _foods[i, j] = _foods[i, newJ];
                            _foods[i, newJ] = null;
                            break; 
                        }
                    }

                    if(_foods[i, j] == null)
                    {
                        CreateFood(i, j);
                    }
                }
            }
        }
    }
}


    //private void OnDrawGizmosSelected()
    //{
    //    float horizontalCenter = (_width - 1) / 2;
    //    float verticalCenter = (_height - 1) / 2;

    //    Vector2 position = new Vector2(horizontalCenter, verticalCenter);
    //    for (int i = 0; i < _width; i++)
    //    {
    //        for (int j = 0; j < _height; j++)
    //        {
    //            if ((i + j) % 2 == 0)
    //            {
    //                Gizmos.DrawSphere(new Vector2(i, j) - position + _offset, 0.1f);
    //            }
    //            else
    //            {
    //                Gizmos.DrawSphere(new Vector2(i, j) - position + _offset, 0.1f);
    //            }
    //        }
    //    }
    //}
