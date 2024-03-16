using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] int maxHealth;
    [SerializeField] Transform cam;

    public void UpdateHealthUI(int enemyHealth, int enemyHealthMax)
    {
        healthSlider.value = enemyHealth;
        healthSlider.maxValue = enemyHealthMax;
    }
    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
