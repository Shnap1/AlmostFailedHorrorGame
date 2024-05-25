using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    int currentLevel;
    public int LevelCalculator(float timeOfaRun, bool Victory)
    {
        if (Victory)
        {
            // LVL = 1 - Expert
            if (InRangeCheck(timeOfaRun, 0f, 4f)) return 1;
            // LVL = 2 - Balanced
            else if (InRangeCheck(timeOfaRun, 4f, 9f)) return 2;
            // LVL = 3 - Expert
            else if (InRangeCheck(timeOfaRun, 9f, 100f)) return 3;
            else return 3;
        }
        else if (!Victory)
        {
            // LVL = 1 - Standard
            if (InRangeCheck(timeOfaRun, 0f, 4f)) return 1;
            // LVL = 2 - Balanced
            else if (InRangeCheck(timeOfaRun, 4f, 9f)) return 2;
            // LVL = 3 - Expert
            else if (InRangeCheck(timeOfaRun, 9f, 15f)) return currentLevel;
            else return 3;
        }
        return currentLevel;
    }

    public bool InRangeCheck(float timeOfRun, float minTime, float maxTime)
    {
        if (timeOfRun >= minTime && timeOfRun <= maxTime) return true;
        else return false;
    }
}
