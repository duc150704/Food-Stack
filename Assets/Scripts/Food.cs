using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, IClickable, IDragable
{
    [SerializeField] FoodData[] _foodData;

    SpriteRenderer _spriteRenderer;
    bool _isFalling = false;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _spriteRenderer.sprite = _foodData[Random.Range(0, _foodData.Length)].Sprite;
    }

    public void FallDown(Vector2 target, float speed)
    {
        if (_isFalling) { return; }
        StartCoroutine(FallDownIE(target, speed));
    }


    private IEnumerator FallDownIE(Vector2 target, float speed)
    {
        _isFalling = true;
        Debug.Log("1");
        while(Vector2.Distance(this.transform.position, target) > 0.01f)
        {
            Debug.Log("2");
            this.transform.position = Vector2.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
            yield return null;
        }

        this.transform.position = target;
        _isFalling = false ;
    }

    public void OnClick()
    {
        
    }

    public void OnDragStart()
    {
        this.transform.position = InputManager.GetMousePosition();
    }

    public void OnDragging(Vector2 position)
    {
        this.transform.position = position;
    }

    public void OnDragEnd()
    {
        
    }
}
