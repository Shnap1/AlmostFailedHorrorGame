using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopManager : MonoBehaviour
{
    public static GameLoopManager instance;
    [HideInInspector]public GameState currentGameState;
    //public static Action<GameState> OnGameStateChanged;
    [SerializeField] PowerUpSpawner powerUpSpawner;
    [SerializeField] EnemySpawner enemySpawner;

    public static event Action<GameState> OnGameUpdate;
    private void Awake()
    {
        // if (instance == null)
        // {
        //     instance = this;
        //     DontDestroyOnLoad(gameObject);
        // }
        // else
        // {
        //     Destroy(gameObject);
        // }
    }

    void Start()
    {
        UpdateGameState(GameState.GatesOpen);
        Debug.Log($"Start() ---------- UpdateGameState(GameState.{currentGameState})");
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(Input.GetKey(KeyCode.O)){
            UpdateGameState(GameState.GatesOpen);
            Debug.Log($"UpdateGameState({currentGameState})");
        }
        else if(Input.GetKey(KeyCode.U)){
            UpdateGameState(GameState.LootCollected);
            Debug.Log($"UpdateGameState({currentGameState})");
        }
    }
// public GameLevel(int levelNumber, int enemiesToSpawn, int lootToSpawn, int collectablesToSpawn)
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
        if(currentGameState != newGameState)
        {
            currentGameState = newGameState;
            switch (currentGameState)
            {
                case GameState.GatesOpen:
                    OpenGate();
                    LoadLevelData(currentLevel);
                    SpawnEnemies(currentLevel.enemiesToSpawn);
                    SpawnLoot(currentLevel.lootToSpawn);
                    SpawnCollectables(currentLevel.collectablesToSpawn);
                    break;
                case GameState.LootCollected:
                    break;
                case GameState.Victory:
                    break;
                case GameState.Lose:
                    break;
                case GameState.Lobby:
                    break;
                default:
                    break;
            }

            //OnGameStateChanged?.Invoke(currentGameState);
            OnGameUpdate?.Invoke(currentGameState);
        }
    }
    
    void LoadLevelData(GameLevel level){
        currentLevel = level;
    }

    public void OpenGate(){
        //
    }

    public void CloseGate(){
        //
    }

    void SpawnLoot(int lootToSpawn){
        for (int i = 0; i < lootToSpawn; i++)
        {
            powerUpSpawner.RandomSpawnPowerUp();
        }
    }
    
    void SpawnCollectables(int collectablesToSpawn){
        //
    }
    
    void SpawnEnemies(int enemiesToSpawn){
        enemySpawner.SpawnEnemies(enemiesToSpawn);
    }
    void GoToLobby(){
        //
    }
    

}

public class GameLevel
{
    public int levelNumber;
    public int enemiesToSpawn;
    public int lootToSpawn;
    public int collectablesToSpawn;


    public GameLevel(int levelNumber, int enemiesToSpawn, int lootToSpawn, int collectablesToSpawn)
    {
        this.levelNumber = levelNumber;
        this.enemiesToSpawn = enemiesToSpawn;
        this.lootToSpawn = lootToSpawn;
        this.collectablesToSpawn = collectablesToSpawn;
    }

}

