using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HealthPU;

public class StatsCounter : MonoBehaviour
{
    public float totalHealth = 1;
    public float TotalHealth { get { return totalHealth; } }

    public float maxHealth = 100;
    public float MaxHealth { get { return maxHealth; } }

    float totalDefence = 0;


    float addedDamage;
    float addedHealth;
    float addedDefence;

    float maxAddedHealth = 1000;
    float maxAddedDefence = 1000;
    float maxAddedDamage = 1000;
    //HealthPU.HealthPUEnum newPUType;
    public PlayerStateMachine playerStateMachine;

    public bool setMaxHealthOnStart = true;

    public static Action<float, float> onPlayerHealthChanged;

    private void Start()
    {
        if (setMaxHealthOnStart) totalHealth = maxHealth;
        onPlayerHealthChanged?.Invoke(totalHealth, MaxHealth);
        if (totalHealth > 0)
        {
            GameData.instance.playerAlive = true;
        }
    }
    public void AddHealth(float addedHealth)
    {
        this.addedHealth = Math.Clamp(addedHealth, 0, maxAddedHealth);
        totalHealth += this.addedHealth;
        totalHealth = Math.Clamp(totalHealth + addedHealth, 0, MaxHealth);
        onPlayerHealthChanged?.Invoke(totalHealth, MaxHealth);

    }

    public void TakeDamage(float newDamage)
    {
        this.addedDamage = Math.Clamp(newDamage, 0, maxAddedDamage);
        if ((totalDefence - this.addedDamage) <= 0)
        {
            //Debug.Log("newDamage = " + newDamage);
            float LeftAfterDefence = this.addedDamage - totalDefence;
            //Debug.Log(" LeftAfterDefence = totalDefence - this.addedDamage; = " + LeftAfterDefence);

            totalDefence = 0;
            totalHealth = Math.Clamp(totalHealth, 0, MaxHealth);
            //Debug.Log("totalHealth" + totalHealth);
        }
        else
        {
            totalDefence -= this.addedDamage;
            //Debug.Log("totalDefence -= this.addedDamage; totalDefence:" + totalDefence);
        }

        if (totalHealth <= 0) //DEAD
        {
            GameData.instance.playerAlive = false;
        }
        onPlayerHealthChanged?.Invoke(totalHealth, maxHealth);
        //Debug.Log("onPlayerHealthChanged?.Invoke(totalHealth, maxHealth); totalHealth=" + totalHealth + " maxHealth= " + maxHealth + "END/");


    }

    public void AddDefence(int addedDefence)
    {
        this.addedDefence = Math.Clamp(addedDefence, 0, maxAddedDefence);
        totalDefence += this.addedDefence;
    }
    public void RemoveDefence(float removeDefence)
    {
        this.addedDefence = Math.Clamp(removeDefence, 0, maxAddedDefence);
        totalDefence -= this.addedDefence;
    }

    //TODO add a reverse SubtractDamange for debuffs

    public void SetSpeed(float walkSpeed, float runSpeed)
    {
        playerStateMachine._currentRunMultiplier = walkSpeed;
        playerStateMachine._currentWalkMultiplier = runSpeed;
    }
    public void AddSpeed(float addSpeed)
    {
        playerStateMachine._currentRunMultiplier += addSpeed;
        playerStateMachine._currentWalkMultiplier = playerStateMachine._currentWalkMultiplier + (addSpeed * 2);
    }
    public void RemoveSpeed(float removeSpeed)
    {
        playerStateMachine._currentRunMultiplier -= removeSpeed;
        playerStateMachine._currentWalkMultiplier = playerStateMachine._currentWalkMultiplier - (removeSpeed * 2);
    }

    public void AddJumpHight(float addJumpHight, float addJumpTime)
    {

        playerStateMachine._currentJumpHeight += addJumpHight;
        playerStateMachine._currentJumpTime += addJumpTime;
        playerStateMachine.SetupJumpVariables();
    }






    //Dictionary<DefenceTypes,int> DefenceDict = new Dictionary<DefenceTypes,int>();

    //public enum DefenceTypes
    //{
    //    Shield,
    //    Armor
    //}
    //void RecievedDamage(int damage, HealthPUEnum damageType)
    //{
    //    newDamage = damage;
    //    newPUType = damageType;
    //}
    //void AllDefence()
    //{
    //    foreach(var item in DefenceDict.Keys)
    //    {
    //        totalDefence += DefenceDict[item];
    //    }
    //}
    //void AllPowerUps()
    //{

    //}



}

