using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FoodType
{
    Donut,
    Cookie,
    Banana,
    IceScream,
    Candy,
    Cake,
    SmallCake,
}

[CreateAssetMenu(fileName = "FoodData", menuName = "Food")]
public class FoodData : ScriptableObject
{
    public Sprite Sprite;
    public FoodType Type;
}
