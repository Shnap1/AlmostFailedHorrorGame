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
        PlayerStateMachine.onPlayerCreated += GetPlayerTransform;
        MainCameraScript.onCamCreated += GetCamTransform;
        PatrolPointManager.onPatrolPointManagerCreated += AssignPatrolPointManager;

    }
    void OnDisable()
    {
        LevelManager.OnLevelCalculated -= GetPlayTimeData;
        PlayerStateMachine.onPlayerCreated -= GetPlayerTransform;
        MainCameraScript.onCamCreated -= GetCamTransform;
        PatrolPointManager.onPatrolPointManagerCreated -= AssignPatrolPointManager;



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
    public void GetCamTransform(Transform cam)
    {
        this.cam = cam;
        Debug.Log("CAM TEST - GetCamTransform");
    }

    public void GetPlayerTransform(Transform player)
    {
        this.player = player;
    }

    void AssignPatrolPointManager(PatrolPointManager manager) => patrolPointManager = manager;
    public PatrolPointManager GetPatrolPointManager()
    {
        if (patrolPointManager == null)
        {
            patrolPointManager = GameObject.Find("Patrol Point Manager").GetComponent<PatrolPointManager>();
        }
        else if (patrolPointManager != null)
        {
            return patrolPointManager;
        }
        return null;
    }

}
