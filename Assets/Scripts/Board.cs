using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Board : MonoBehaviour
{
    [SerializeField] float _width;
    [SerializeField] float _height;

    [SerializeField] GameObject _bg1;
    [SerializeField] GameObject _bg2;

    private void Start()
    {
        InitBoard();
    }

    void InitBoard()
    {
        float verticalCenter = Mathf.Floor(_height / 2);
        float horizontalCenter = Mathf.Floor(_width / 2);

        Vector2 position = new Vector2(0, 0);

        for(int i = 0; i < _width; i++)
        {
            for(int j = 0; j < _height; j++)
            {
                if ((i + j) % 2 == 0)
                {
                    Instantiate(_bg1, new Vector2(i, j) - position, Quaternion.identity);
                }
                else 
                {
                    Instantiate(_bg2, new Vector2(i, j) - position, Quaternion.identity);
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
                    Gizmos.DrawSphere(new Vector2(i, j) - position, 0.1f);
                }
                else
                {
                    Gizmos.DrawSphere(new Vector2(i, j) - position, 0.1f);
                }
            }
        }
    }
}
