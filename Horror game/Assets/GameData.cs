using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData instance;
    public int currentLevel = 0;
    public string currentTestString;
    private int currentScore = 0;

    public Transform player;
    public Transform cam;
    public PatrolPointManager patrolPointManager;


    void OnEnable()
    {
        // GameLoopManager.onPlayTimeSTopped += GetRuntimeData;
        LevelManager.OnLevelCalculated += GetPlayTimeData;

    }
    void OnDisable()
    {
        LevelManager.OnLevelCalculated -= GetPlayTimeData;

    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void GetPlayTimeData(int level)
    {
        currentLevel = level;
        // currentScore = 0;
        Debug.Log("GetPlayTimeData");
        currentTestString = "IDI NAHYII!";
    }
}
