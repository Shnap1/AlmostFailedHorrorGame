using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.AI;
using Unity.AI.Navigation;


public class GameLoopManager : MonoBehaviour
{
    public static GameLoopManager instance;
    public static GameState currentGameState;
    public string curGamStateText;
    //public static Action<GameState> OnGameStateChanged;
    [SerializeField] LootSpawner powerUpSpawner;
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] NavMeshSurface surface;

    public static event Action<GameState> OnGameUpdate;
    public static Func<string, string> onGameStateChanger;
    [SerializeField] string currentTestString;

    public int number_of_TARGETS_to_collect;
    public int current_number_of_TARGETS_collected;

    public Stopwatch stopwatch;
    public float gameTime;

    public static event Action<float, bool> onPlayTimeSTopped;
    public static event Action<GameLoopManager.GameState, int, int> onTargetCollected;

    void OnEnable()
    {

    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        // DontDestroyOnLoad(gameObject);
        // DontDestroyOnLoad(powerUpSpawner.gameObject);
        // DontDestroyOnLoad(enemySpawner.gameObject);

        if (Time.timeScale == 0) Time.timeScale = 1;

    }

    void Start()
    {
        UpdateGameState(GameState.SpawnPlayer);
        // UnityEngine.Debug.Log($"Start() ---------- UpdateGameState(GameState.{currentGameState})");
        // testString = onGameStateChanger?.Invoke("test String from event") ?? "nothing";
        if (currentTestString != null)
        {
            currentTestString = onGameStateChanger?.Invoke("test String from event") ?? "nothing";
        }

    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>

    // public GameLevel(int levelNumber, int enemiesToSpawn, int lootToSpawn, int collectablesToSpawn)

    string TestFuncValueChecker(string strToBeChecked)
    {
        if (strToBeChecked != null && strToBeChecked != currentTestString)
        {
            currentTestString = strToBeChecked;
            return strToBeChecked;
        }
        else
        {
            UnityEngine.Debug.Log("Your teststring is either null or equal to currentTestString");
        }
        return null;

    }
    List<GameLevel> gameLevels = new List<GameLevel>()
    {
        new GameLevel(levelNumber: 0, enemiesToSpawn: 1, lootToSpawn: 1, collectablesToSpawn: 1),

    };

    GameLevel currentLevel = new GameLevel(levelNumber: 1, enemiesToSpawn: 5, lootToSpawn: 4, collectablesToSpawn: 5);

    public enum GameState
    {
        SpawnSectors,
        SpawnBuildings,
        SpawnLoot,
        SpawnPlayer,
        GameStart,
        LootCollected,

        Victory,
        Lose,
        Lobby
    }
    public void UpdateGameState(GameState newGameState)
    {
        // UnityEngine.Debug.Log($"UpdateGameState({newGameState})");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (currentGameState != newGameState)
        {
            currentGameState = newGameState;
            switch (currentGameState)
            {
                case GameState.SpawnPlayer:
                    LoadLevelData(currentLevel);
                    current_number_of_TARGETS_collected = 0;
                    // SpawnEnemies(currentLevel.enemiesToSpawn);
                    // SpawnLoot(currentLevel.lootToSpawn);
                    // SpawnCollectables(currentLevel.collectablesToSpawn);
                    // UnityEngine.Debug.Log("Gates Opened");

                    // Cursor.lockState = CursorLockMode.Locked;
                    // Cursor.visible = false;

                    break;
                case GameState.GameStart:
                    // surface.BuildNavMesh();
                    SpawnEnemies(currentLevel.enemiesToSpawn);
                    //TODO replace SpawnEnemies() with SpawnEnemiesByLevel() 
                    //setting how many TARGETS to collect
                    number_of_TARGETS_to_collect = currentLevel.number_of_TARGETS_to_spawn;

                    //Spawning LOOT
                    LootSpawner.PowerUpSpawn(currentLevel.number_of_LOOT_to_spawn, LootSpawner.LootType.healthPU);

                    //Spawning ARTIFACTS
                    LootSpawner.PowerUpSpawn(currentLevel.number_of_TARGETS_to_spawn, LootSpawner.LootType.target);

                    stopwatch = Stopwatch.StartNew();

                    break;
                case GameState.LootCollected:
                    //TODO open gate 
                    break;
                case GameState.Victory:
                    //TODO add victory screen, kill enemies in gate boxCollider, add EXP and load next level 
                    StopStopWatch(gameWon: true);
                    break;
                case GameState.Lose:
                    //TODO add lose screen with 1) Retry button loading the same level 2) reload state 3) Lobby button to load lobby level
                    StopStopWatch(gameWon: false);
                    break;
                case GameState.Lobby:
                    //TODO add PLAY button in the corner that would open UI with options to load next level and scene that would change state to GatesOpen

                    break;
                default:
                    break;
            }

            //OnGameStateChanged?.Invoke(currentGameState);
        }
        OnGameUpdate?.Invoke(currentGameState);
        onTargetCollected?.Invoke(currentGameState, number_of_TARGETS_to_collect, current_number_of_TARGETS_collected);
        curGamStateText = currentGameState.ToString();

    }

    void LoadLevelData(GameLevel level)
    {
        currentLevel = level;
    }




    void SpawnCollectables(int collectablesToSpawn)
    {
        //TODO make collectables script
        // PowerUpSpawner.PowerUpSpawnStatic(collectablesToSpawn);
    }

    void SpawnEnemies(int enemiesToSpawn)
    {
        enemySpawner.SpawnEnemies(enemiesToSpawn);
        // UnityEngine.Debug.Log($"SPAWN ENEMIES({enemiesToSpawn})");
    }
    void GoToLobby()
    {
        //
    }

    public static void StaticFunctionTest(GameState newGameState)
    {
        // currentGameState = newGameState;
        instance.UpdateGameState(newGameState);

    }

    public static void TargetCollected()
    {
        instance.CheckIfALLTargetsCollected();
    }
    public void CheckIfALLTargetsCollected()
    {
        current_number_of_TARGETS_collected++;
        if (current_number_of_TARGETS_collected >= number_of_TARGETS_to_collect && currentGameState == GameState.GameStart)
        {
            UpdateGameState(GameState.LootCollected);
        }
        //onTargetCollected?.Invoke(instance.current_number_of_TARGETS_collected);
        onTargetCollected?.Invoke(currentGameState, number_of_TARGETS_to_collect, current_number_of_TARGETS_collected);

    }

    void StopStopWatch(bool gameWon)
    {
        stopwatch.Stop();
        gameTime = (float)stopwatch.Elapsed.TotalSeconds;
        onPlayTimeSTopped?.Invoke(gameTime, gameWon);
    }
}

public class GameLevel
{
    public int levelNumber;
    public int enemiesToSpawn;
    public int number_of_LOOT_to_spawn;
    public int number_of_TARGETS_to_spawn;


    public GameLevel(int levelNumber, int enemiesToSpawn, int lootToSpawn, int collectablesToSpawn)
    {
        this.levelNumber = levelNumber;
        this.enemiesToSpawn = enemiesToSpawn;
        this.number_of_LOOT_to_spawn = lootToSpawn;
        this.number_of_TARGETS_to_spawn = collectablesToSpawn;
    }

}

