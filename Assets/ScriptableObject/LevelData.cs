using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public enum EWinCondition
{
    ReachScore,
    TimeLimit,
}

[CreateAssetMenu(fileName = "LevelData", menuName = "Level Data")]
public class LevelData : ScriptableObject
{
    public string Name;
    public int Level;

    public int BoardWidth;
    public int BoardHeight;

    public GameObject Food;

    public EWinCondition WinCondition;
}
