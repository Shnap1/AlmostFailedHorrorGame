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
        PlayerStateMachine.onPlayerCreated += GetPlayerTransform;
        LevelManager.OnLevelCalculated += GetPlayTimeData;
        MainCameraScript.onCamCreated += GetCamTransform;
        PatrolPointManager.onPatrolPointManagerCreated += GetPatrolPointManager;

    }
    void OnDisable()
    {
        LevelManager.OnLevelCalculated -= GetPlayTimeData;
        PlayerStateMachine.onPlayerCreated -= GetPlayerTransform;
        MainCameraScript.onCamCreated -= GetCamTransform;
        PatrolPointManager.onPatrolPointManagerCreated -= GetPatrolPointManager;



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
        // Debug.Log("GetPlayTimeData");
        currentTestString = "IDI NAHYII!";
    }
    void GetCamTransform(Transform cam) => this.cam = cam;

    void GetPlayerTransform(Transform player) => this.player = player;
    void GetPatrolPointManager(PatrolPointManager manager) => patrolPointManager = manager;

}
