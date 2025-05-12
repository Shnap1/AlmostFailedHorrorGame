using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart_State : GAMEFSM_Base_State
{

    public override void EnterState()
    {
        GameData.instance.enemiesKilled = 0;
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
        SM.settingsMenu.PauseStateChecker();
    }

    public override void CheckSwitchState()
    {
        if (!SM.gatesNew.playerInside)
        {
            SM.SwitchState(SM.GameCycle);
        }
    }

}
