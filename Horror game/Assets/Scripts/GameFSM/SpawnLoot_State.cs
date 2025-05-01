using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLootAndEnemies_State : GAMEFSM_Base_State
{
    public override void EnterState()
    {

    }

    void SpawningLoot()
    {
        SM.gameLoopManager.SpawnLootAndEnemies();
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
    }

    public override void CheckSwitchState()
    {
    }

}
