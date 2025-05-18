using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameCycle_State : GAMEFSM_Base_State
{
    public int enemiesToKill;

    void OnEnable()
    {
        GameData.onEnemyKilled += UpdateUIText;

    }
    void OnDisable()
    {
        GameData.onEnemyKilled -= UpdateUIText;

    }

    public override void EnterState()
    {
        GameData.instance.enemiesKilled = 0;
        GameData.instance.playerAlive = true;

        enemiesToKill = SM.gameLoopManager.currentLevel.enemiesToSpawn;
        UpdateUIText();
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
        CheckSwitchState();
        SM.settingsMenu.PauseStateChecker();

    }

    void UpdateUIText()
    {
        if (SM.healthBar.curLanguage == HealthBar.GameLanguages.en)
        {
            SM.healthBar.currentGameStateText.text = $"{GameData.instance.enemiesKilled} of  {enemiesToKill} monsters killed. Kill all monsters.";
        }
        else if (SM.healthBar.curLanguage == HealthBar.GameLanguages.ru)
        {
            SM.healthBar.currentGameStateText.text = $"{GameData.instance.enemiesKilled} из {enemiesToKill} монстров убито. Убей всех монстров.";
        }
        else if (SM.healthBar.curLanguage == HealthBar.GameLanguages.tr)
        {
            SM.healthBar.currentGameStateText.text = $"Öldürülen  {enemiesToKill} canavardan {GameData.instance.enemiesKilled}'ı. Bütün canavarları öldür.";
        }
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
