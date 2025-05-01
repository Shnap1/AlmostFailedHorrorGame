using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public class SpawnSectors_State : GAMEFSM_Base_State
{
    public SectorSpawn sectorSpawn;
    public bool sectorsFinishedSpawning;

    void Start()
    {
        EnterState();
    }
    public override void EnterState()
    {
        sectorSpawn = SM.sectorSpawn;
        sectorSpawn.SpawnRandomSectors();
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
        sectorsFinishedSpawning = sectorSpawn.ReferenceState();
        if (sectorsFinishedSpawning)
        {
            SM.SwitchState(SM.SpawnBuilding);
        }
    }

}
