using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] int maxHealth;

    private void OnEnable()
    {
        HealthCounter.onPlayerHealthChanged += UpdateHealthUI;
        //UpdateHealthUI()
    }
    private void OnDisable()
    {
        HealthCounter.onPlayerHealthChanged += UpdateHealthUI;
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
