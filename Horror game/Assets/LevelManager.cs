using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static event Action<int> OnLevelCalculated;
    void OnEnable()
    {
        GameLoopManager.onPlayTimeSTopped += LevelCalculator;
        // Debug.Log("GameLoopManager.onPlayTimeSTopped += LevelCalculator;");
    }

    void OnDisable()
    {
        GameLoopManager.onPlayTimeSTopped -= LevelCalculator;
    }
    int currentLevel;
    public void LevelCalculator(float timeOfaRun, bool Victory)
    {
        if (Victory)
        { //TODO change appropriate max and min's of timeOfaRun below to correct values in seconds
            // LVL = 1 - Expert
            if (InRangeCheck(timeOfaRun, 0f, 4f)) currentLevel = 1;
            // LVL = 2 - Balanced
            else if (InRangeCheck(timeOfaRun, 4f, 9f)) currentLevel = 2;
            // LVL = 3 - Expert
            else if (InRangeCheck(timeOfaRun, 9f, 100f)) currentLevel = 3;
            else currentLevel = 3;
        }
        else if (!Victory)
        {
            // LVL = 1 - Standard
            if (InRangeCheck(timeOfaRun, 0f, 4f)) currentLevel = 1;
            // LVL = 2 - Balanced
            else if (InRangeCheck(timeOfaRun, 4f, 9f)) currentLevel = 2;
            // LVL = 3 - Expert
            else if (InRangeCheck(timeOfaRun, 9f, 15f)) { }
            else currentLevel = 3;
        }
        OnLevelCalculated?.Invoke(currentLevel);
        // Debug.Log("LEVEL CALCULATED!!!!!!!!!!!!!!!");
    }

    public bool InRangeCheck(float timeOfRun, float minTime, float maxTime)
    {
        if (timeOfRun >= minTime && timeOfRun <= maxTime) return true;
        else return false;
    }

    // public static int ReturnLevel(int level)
    // {
    //     return level;
    // }
}
