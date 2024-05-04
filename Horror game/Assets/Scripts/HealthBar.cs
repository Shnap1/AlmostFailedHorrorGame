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
        GameLoopManager.OnGameUpdate += UpdateStateText;
        //UpdateHealthUI()
    }
    private void Start() 
    {
        UpdateStateText(GameLoopManager.currentGameState);
    }
    private void OnDisable()
    {
        HealthCounter.onPlayerHealthChanged += UpdateHealthUI;
        GameLoopManager.OnGameUpdate -= UpdateStateText;
    }

    void UpdateStateText(GameLoopManager.GameState gameState)
    {
        currentGameStateText.text = gameState.ToString();
    }
    void UpdateHealthUI(int playerHealth,int playerHealthMax)
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
