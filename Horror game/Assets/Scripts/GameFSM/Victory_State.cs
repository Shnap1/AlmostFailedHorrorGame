using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        CheckSwitchState();
    }

    public override void CheckSwitchState()
    {
        if (SM.settingsMenu.loadNextScenePressed)
        {
            // SM.settingsMenu.LoadNextMission();
            SM.settingsMenu.loadNextScenePressed = false;
            GameData.instance.enemiesKilled = 0;

            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene


        }
    }

}
