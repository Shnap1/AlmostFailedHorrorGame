using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatesNew : MonoBehaviour
{
    public PlayerSpawner playerSpawner;
    void OnTriggerExit(Collider other)
    {
        // Debug.Log("Game Started  === PLAYER EXITED THE GATE");

        if (other.gameObject.tag == "Player" && GameLoopManager.currentGameState == GameLoopManager.GameState.SpawnPlayer)
        {
            playerSpawner.gameLoopManager.UpdateGameState(GameLoopManager.GameState.GameStart);
            // Debug.Log("Game Started  === PLAYER EXITED THE GATE");

        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && GameLoopManager.currentGameState == GameLoopManager.GameState.LootCollected)
        {
            //gateOpener.Close();
            playerSpawner.gameLoopManager.UpdateGameState(GameLoopManager.GameState.Victory);
        }
    }
}
