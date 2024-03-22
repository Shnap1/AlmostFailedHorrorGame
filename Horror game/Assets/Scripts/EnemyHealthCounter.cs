using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthCounter : MonoBehaviour
{
    [SerializeField] int totalHealth = 1;
    [SerializeField] GameObject healthBar;
    private EnemyHealthBar enemyHealthBar;
    [SerializeField] int maxHealth = 100;

    int totalDefence;

    int addedDamage;
    int addedHealth;
    int addedDefence;

    int maxAddedHealth = 1000;
    int maxAddedDefence = 1000;
    int maxAddedDamage = 1000;

    private void Start()
    {
        enemyHealthBar = healthBar.GetComponent<EnemyHealthBar>();
        enemyHealthBar.UpdateHealthUI(totalHealth, maxHealth);
    }
    public void AddHealth(int addedHealth)
    {
        this.addedHealth = Math.Clamp(addedHealth, 0, maxAddedHealth);
        totalHealth += this.addedHealth;
        totalHealth = Math.Clamp(totalHealth + addedHealth, 0, maxHealth);
        enemyHealthBar.UpdateHealthUI(totalHealth, maxHealth);
    }
    public void AddDefence(int addedDefence)
    {
        this.addedDefence = Math.Clamp(addedDefence, 0, maxAddedDefence);
        totalDefence += this.addedDefence;
    }
    public void TakeDamage(int newDamage)
    {
        this.addedDamage = Math.Clamp(newDamage, 0, maxAddedDamage);
        if ((totalDefence - this.addedDamage) <= 0)
        {
            int LeftAfterDefence = totalDefence - this.addedDamage;
            totalDefence = 0;
            totalHealth = Math.Clamp(totalHealth - LeftAfterDefence, 0, maxHealth);
        }
        else
        {
            totalDefence -= this.addedDamage;
        }
        enemyHealthBar.UpdateHealthUI(totalHealth, maxHealth);
    }
}
