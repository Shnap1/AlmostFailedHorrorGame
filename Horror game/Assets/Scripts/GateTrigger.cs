using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    bool gameBegan = false;
    [SerializeField] GateOpener gateOpener;
    [SerializeField] GameLoopManager gameLoopManager;

    void Awake()
    {
        // GameLoopManager.OnGameUpdate += PlayerGateTrigger;
    }
    void OnDisable()
    {
        // GameLoopManager.OnGameUpdate -= PlayerGateTrigger;
    }

    // void PlayerGateTrigger(GameLoopManager.GameState gameState){
    //     if(gameState == GameLoopManager.GameState.GatesOpen){
    //         gameBegan = true;
    //     }
    //     else if(gameState == GameLoopManager.GameState.LootCollected)
    //     {
    //         gameBegan = false;
    //     }
    // }

    void OnTriggerExit(Collider other)
    {
        // Debug.Log("Game Started  === PLAYER EXITED THE GATE");

        if (other.gameObject.tag == "Player" && GameLoopManager.currentGameState == GameLoopManager.GameState.SpawnPlayer)
        {
            //gateOpener.Close();
            gameLoopManager.UpdateGameState(GameLoopManager.GameState.GameStart);
            // Debug.Log("Game Started  === PLAYER EXITED THE GATE");

        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && GameLoopManager.currentGameState == GameLoopManager.GameState.LootCollected)
        {
            //gateOpener.Close();
            gameLoopManager.UpdateGameState(GameLoopManager.GameState.Victory);
        }
    }
}
