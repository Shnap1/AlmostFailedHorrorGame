using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] int maxHealth;
    [SerializeField] Text currentGameStateText;
    public GameLoopManager gameLoopManager;

    private void OnEnable()
    {
        HealthCounter.onPlayerHealthChanged += UpdateHealthUI;
        // GameLoopManager.OnGameUpdate += UpdateStateText;
        GameLoopManager.onTargetCollected += UpdateStateText;
        //UpdateHealthUI()
    }
    private void Start()
    {
        // UpdateStateText(GameLoopManager.currentGameState);
    }
    private void OnDisable()
    {
        HealthCounter.onPlayerHealthChanged += UpdateHealthUI;
        // GameLoopManager.OnGameUpdate -= UpdateStateText;
        GameLoopManager.onTargetCollected -= UpdateStateText;
    }

    void UpdateStateText(GameLoopManager.GameState gameState, int number_of_TARGETS_to_collect, int number_of_TARGETS_collected)
    {
        currentGameStateText.text = gameState.ToString();
        switch (gameState)
        {
            case GameLoopManager.GameState.GatesOpen:
                currentGameStateText.text = "Go through the gates to collect all targets";
                break;
            case GameLoopManager.GameState.GameStart:
                currentGameStateText.text = $"{gameLoopManager.current_number_of_TARGETS_collected} targets out of {gameLoopManager.number_of_TARGETS_to_collect} collected. Collect all targets";
                break;
            case GameLoopManager.GameState.LootCollected:
                currentGameStateText.text = "Loot Collected. RUN BACK TO THE GATE!";
                break;
            case GameLoopManager.GameState.Lobby:
                currentGameStateText.text = "You are now in the lobby. Go to the gates to start the game";
                break;
            default:
                break;
        }
    }

    void UpdateHealthUI(int playerHealth, int playerHealthMax)
    {
        healthSlider.value = playerHealth;
        healthSlider.maxValue = playerHealthMax;
    }

    //private void Start()
    //{
    //    healthSlider = GetComponentInChildren<Slider>();
    //    UpdateHealthUI(100,100);
    //}
}
