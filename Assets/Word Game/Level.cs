using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public event Action<float> LevelChanged;

    private int currentLevel = 0;
    private float levelStartTime;
    private int timeForLevel;

    public int CurrentLevel { get => currentLevel; set => currentLevel = value; }
    public float LevelStartTime => levelStartTime;
    public int TimeForLevel => timeForLevel;

    public void NextLevel()
    {
        currentLevel++;
        levelStartTime = Time.time;

        UpdateLevel();
    }
    public void NewGame()
    {
        currentLevel = 1;
        levelStartTime = Time.time;

        UpdateLevel();
    }
    public void UpdateLevel()
    {
        LevelChanged?.Invoke(levelStartTime);
    }
}
