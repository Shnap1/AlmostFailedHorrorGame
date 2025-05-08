using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory_State : GAMEFSM_Base_State
{
    public override void EnterState()
    {
        SM.settingsMenu.ShowVictoryUI();
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
    }

    public override void CheckSwitchState()
    {
        if (SM.settingsMenu.loadNextScenePressed)
        {
            SM.settingsMenu.LoadNextMission();
        }
    }

}
