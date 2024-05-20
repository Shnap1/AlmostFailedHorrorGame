using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class EnemyHealthCounter : MonoBehaviour
{
    [SerializeField] int totalHealth = 1;
    [SerializeField] GameObject healthBar;
    private EnemyHealthBar enemyHealthBar;
    [SerializeField] int maxHealth = 100;

    [SerializeField] ZombieStateManager zombieStateManager;
    [SerializeField] TMP_Text followerType;
    [SerializeField] TMP_Text currentEnemyStateText;


    int totalDefence;

    int addedDamage;
    int addedHealth;
    int addedDefence;

    int maxAddedHealth = 1000;
    int maxAddedDefence = 1000;
    int maxAddedDamage = 1000;

    void OnEnable()
    {
        ZombieStateManager.onZombieStateChanged += UpdZombieStateInfo;
        Zombie_Patrolling_State.onPatrollingTypeSet += UpdEnemyInfo;
    }
    void OnDisable()
    {
        ZombieStateManager.onZombieStateChanged -= UpdZombieStateInfo;
        Zombie_Patrolling_State.onPatrollingTypeSet -= UpdEnemyInfo;
    }
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

    public void UpdEnemyInfo(Zombie_Patrolling_State.Patrollers patrollerType)
    {
        followerType.text = patrollerType.ToString();
    }

    public void UpdZombieStateInfo(IStateNew zombieState)
    {
        currentEnemyStateText.text = zombieState.ToString();
    }
}
