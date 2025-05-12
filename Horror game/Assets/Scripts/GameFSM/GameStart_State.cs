using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart_State : GAMEFSM_Base_State
{
    public override void EnterState()
    {
        GameData.instance.enemiesKilled = 0;
        GameData.instance.playerAlive = true;

        Debug.Log("EnterState in Gamestart");
        if (SM.healthBar.curLanguage == HealthBar.GameLanguages.en)
        {
            SM.healthBar.currentGameStateText.text = "Go beyond the safe zone.";
        }
        else if (SM.healthBar.curLanguage == HealthBar.GameLanguages.ru)
        {
            SM.healthBar.currentGameStateText.text = "Выйди за границы безопасной зоны.";
        }
        else if (SM.healthBar.curLanguage == HealthBar.GameLanguages.tr)
        {
            SM.healthBar.currentGameStateText.text = "Güvenli bölgenin ötesine geçin.";
        }
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
