using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopManager : MonoBehaviour
{
    public static GameLoopManager instance;
    public GameState currentGameState;
    public static Action<GameState> OnGameStateChanged;
    private void Awake()
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
        UpdateGameState(GameState.Lobby);
    }

    public enum GameState
    {
        GatesOpen,
        LootCollected,
        Victory,
        Lose,
        Lobby
    }
    void UpdateGameState(GameState newGameState)
    {
        if(currentGameState != newGameState)
        {
            currentGameState = newGameState;
            switch (currentGameState)
            {
                case GameState.GatesOpen:
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

            OnGameStateChanged?.Invoke(currentGameState);
        }
    }
    
    void LevelLoadData(int levelNumber){
        
    }

    public void OpenGate(){
        //
    }

    public void CloseGate(){
        //
    }

    void SpawnLoot(){
        //
    }
    
    void SpawnCollectables(){
        //
    }
    
    void SpawnEnemies(){
        //
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

    public GameLevel(int levelNumber){
        this.levelNumber = levelNumber;
    }
}

