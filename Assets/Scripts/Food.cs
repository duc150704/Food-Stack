using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] FoodData[] _foodData;

    SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _spriteRenderer.sprite = _foodData[Random.Range(0, _foodData.Length)].Sprite;
    }

    public void FallDown() =>
        StartCoroutine(FallDownIE());

    private IEnumerator FallDownIE()
    {
        throw new System.NotImplementedException();
    }
}
