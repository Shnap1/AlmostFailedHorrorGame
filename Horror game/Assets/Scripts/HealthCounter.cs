using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HealthPU;

public class HealthCounter : MonoBehaviour
{
    [SerializeField] int totalHealth = 1;
    [SerializeField] int maxHealth = 100;

    int totalDefence = 0;


    int addedDamage;
    int addedHealth;
    int addedDefence;

    int maxAddedHealth = 1000;
    int maxAddedDefence = 1000;
    int maxAddedDamage = 1000;
    //HealthPU.HealthPUEnum newPUType;

    public static Action<int, int> onPlayerHealthChanged;

    private void Start()
    {
        onPlayerHealthChanged?.Invoke(totalHealth, maxHealth);
        if (totalHealth > 0)
        {
            GameData.instance.playerAlive = true;
        }
    }
    public void AddHealth(int addedHealth)
    {
        this.addedHealth = Math.Clamp(addedHealth, 0, maxAddedHealth);
        totalHealth += this.addedHealth;
        totalHealth = Math.Clamp(totalHealth + addedHealth, 0, maxHealth);
        onPlayerHealthChanged?.Invoke(totalHealth, maxHealth);

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
            //Debug.Log("newDamage = " + newDamage);
            int LeftAfterDefence = this.addedDamage - totalDefence;
            //Debug.Log(" LeftAfterDefence = totalDefence - this.addedDamage; = " + LeftAfterDefence);

            totalDefence = 0;
            totalHealth = Math.Clamp(totalHealth - LeftAfterDefence, 0, maxHealth);
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

