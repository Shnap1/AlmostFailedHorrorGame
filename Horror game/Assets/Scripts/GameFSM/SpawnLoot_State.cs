using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLootAndEnemies_State : GAMEFSM_Base_State
{
    bool finished;
    public override void EnterState()
    {
        // SpawningLoot();
        finished = false;
        SM.gameLoopManager.LoadLevelData(SM.gameLoopManager.currentLevel);
        SM.gameLoopManager.SpawnLootAndEnemies();
        SM.playerSpawner.SpawnStartingGates();//spawns gates and player
        finished = true;

    }

    void SpawningLoot()
    {


    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
        CheckSwitchState();
    }

    public override void CheckSwitchState()
    {
        if (finished)
        {
            SM.SwitchState(SM.GameCycle);
        }
    }

}
