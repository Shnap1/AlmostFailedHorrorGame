using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Loose_State : GAMEFSM_Base_State
{
    public override void EnterState()
    {
        SM.settingsMenu.ShowLooseUI();
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
        if (SM.settingsMenu.restartGamePressed)
        {
            SM.settingsMenu.restartGamePressed = false;
            GameData.instance.enemiesKilled = 0;

            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene

        }
        else if (SM.settingsMenu.exitGamePressed)
        {
            SM.settingsMenu.exitGamePressed = false;
            SceneManager.LoadScene("MainMenu");
        }
    }

}
