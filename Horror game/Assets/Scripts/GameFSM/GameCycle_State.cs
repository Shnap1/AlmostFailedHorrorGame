using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameCycle_State : GAMEFSM_Base_State
{
    public int enemiesToKill;

    public override void EnterState()
    {
        enemiesToKill = SM.gameLoopManager.currentLevel.enemiesToSpawn;
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
        CheckSwitchState();
        SM.settingsMenu.PauseStateChecker();

    }

    public override void CheckSwitchState()
    {
        if (enemiesToKill == GameData.instance.enemiesKilled)
        {
            SM.SwitchState(SM.Victory);
        }
        else if (GameData.instance.playerAlive == false)
        {
            SM.SwitchState(SM.Death);
        }
        else if (SM.settingsMenu.exitGamePressed)
        {
            SM.settingsMenu.exitGamePressed = false;
            SceneManager.LoadScene("MainMenu");
        }
    }

}
