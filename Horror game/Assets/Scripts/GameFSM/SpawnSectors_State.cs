using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public class SpawnSectors_State : GAMEFSM_Base_State
{
    public SectorSpawn sectorSpawn;

    void Start()
    {
        EnterState();
    }
    public override void EnterState()
    {
        sectorSpawn = SM.sectorSpawn;
        // sectorSpawn.SpawnRandomSectors();
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
