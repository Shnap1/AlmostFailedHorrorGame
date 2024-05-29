using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Diagnostics;


public class GameLoopManager : MonoBehaviour
{
    public static GameLoopManager instance;
    [SerializeField] public static GameState currentGameState;
    //public static Action<GameState> OnGameStateChanged;
    [SerializeField] LootSpawner powerUpSpawner;
    [SerializeField] EnemySpawner enemySpawner;

    public static event Action<GameState> OnGameUpdate;
    public static Func<string, string> onGameStateChanger;
    [SerializeField] string currentTestString;

    public int number_of_TARGETS_to_collect;
    public int current_number_of_TARGETS_collected;

    public Stopwatch stopwatch;
    public float gameTime;

    public static event Action<float, bool> onPlayTimeSTopped;

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
        UpdateGameState(GameState.GatesOpen);
        UnityEngine.Debug.Log($"Start() ---------- UpdateGameState(GameState.{currentGameState})");
        // testString = onGameStateChanger?.Invoke("test String from event") ?? "nothing";

        if (currentTestString != null)
        {
            currentTestString = onGameStateChanger?.Invoke("test String from event") ?? "nothing";
        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {

    }
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
        new GameLevel(0, 1, 1, 1),
        new GameLevel(1, 1, 1, 1),
        new GameLevel(2, 2, 2, 2),
        new GameLevel(3, 3, 3, 3),
        new GameLevel(4, 4, 4, 4)
    };

    GameLevel currentLevel = new GameLevel(1, 5, 5, 3);

    public enum GameState
    {
        GatesOpen,
        GameStart,
        LootCollected,
        Victory,
        Lose,
        Lobby
    }
    public void UpdateGameState(GameState newGameState)
    {
        if (currentGameState != newGameState)
        {
            currentGameState = newGameState;
            switch (currentGameState)
            {
                case GameState.GatesOpen:
                    OpenGate();
                    LoadLevelData(currentLevel);
                    current_number_of_TARGETS_collected = 0;
                    // SpawnEnemies(currentLevel.enemiesToSpawn);
                    // SpawnLoot(currentLevel.lootToSpawn);
                    // SpawnCollectables(currentLevel.collectablesToSpawn);
                    break;
                case GameState.GameStart:

                    SpawnEnemies(currentLevel.enemiesToSpawn);
                    //TODO replace SpawnEnemies() with SpawnEnemiesByLevel() 
                    //setting how many TARGETS to collect
                    number_of_TARGETS_to_collect = currentLevel.number_of_TARGETS_to_spawn;
                    //Spawning LOOT
                    LootSpawner.PowerUpSpawn(currentLevel.number_of_LOOT_to_spawn, LootSpawner.LootType.healthPU);
                    //Spawning TARGETS
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
    }

    void LoadLevelData(GameLevel level)
    {
        currentLevel = level;
    }

    public void OpenGate()
    {
        //
    }

    public void CloseGate()
    {
        //
    }


    void SpawnCollectables(int collectablesToSpawn)
    {
        //TODO make collectables script
        // PowerUpSpawner.PowerUpSpawnStatic(collectablesToSpawn);
    }

    void SpawnEnemies(int enemiesToSpawn)
    {
        enemySpawner.SpawnEnemies(enemiesToSpawn);
        UnityEngine.Debug.Log($"SPAWN ENEMIES({enemiesToSpawn})");
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
        instance.CheckIfTargetsCollected();
    }
    public void CheckIfTargetsCollected()
    {
        current_number_of_TARGETS_collected++;
        if (current_number_of_TARGETS_collected == number_of_TARGETS_to_collect)
        {
            UpdateGameState(GameState.LootCollected);
        }
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

