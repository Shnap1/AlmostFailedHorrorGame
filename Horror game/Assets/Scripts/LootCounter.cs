using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootCounter : MonoBehaviour
{
    // Start is called before the first frame update

    public int numbOfTargetCollectables = 0;
    public int maxTargetCollectables = 5;

    //public static event Action OnAllLootCollected;

    public GameLoopManager gameLoopManager;

    void Start()
    {
        var player = this.gameObject;
    }


    public void AddLoot()
    {

        if (numbOfTargetCollectables + 1 <= maxTargetCollectables)
        {
            numbOfTargetCollectables += 1;
        }

        if (numbOfTargetCollectables == maxTargetCollectables)
        {
            //
            //OnAllLootCollected?.Invoke();
            gameLoopManager.UpdateGameState(GameLoopManager.GameState.SpawnPlayer);
        }

    }
}
