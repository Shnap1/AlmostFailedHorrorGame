using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeathZone : Ability
{
    public int damagePonts = 1;
    public float damagePercent = 1;
    public float leaveHP = 1;
    public float leaveHPPercent = 1;


    public enum DamageMode { Percent, Points, leaveHP, leaveHPPercent, kill };
    public DamageMode currentDamageMode;
    [SerializeField] List<Collider> colliders = new List<Collider>();
    public float zoneDamageCooldown;
    public bool isTriggered = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            colliders.Add(other);
        }
        if (colliders.Count == 1 && !isTriggered)
        {
            isTriggered = true;
            StartCoroutine(AttackBreak(zoneDamageCooldown, other));
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            colliders.Remove(other);
        }
        if (colliders.Count <= 0)
        {
            StopCoroutine(AttackBreak(zoneDamageCooldown, other));
            StopAllCoroutines();
            isTriggered = false;

        }
    }

    IEnumerator AttackBreak(float time, Collider collider)
    {
        while (isTriggered)
        {
            DealDamage(collider);
            yield return new WaitForSeconds(time);
            // Debug.Log("AttackBreak");
        }
    }

    public void SetDamageMode(DamageMode mode)
    {
        currentDamageMode = mode;
    }

    void DealDamage(Collider collider)
    {
        var totalEnemyHealth = collider.GetComponent<StatsCounter>().TotalHealth;

        switch (currentDamageMode)
        {
            case DamageMode.Percent:
                var damageInHP = (totalEnemyHealth / 100) * damagePercent;
                collider.GetComponent<StatsCounter>().AddHealth(-damageInHP);
                break;
            case DamageMode.Points:
                collider.GetComponent<StatsCounter>().AddHealth(-damagePonts);
                break;
            case DamageMode.leaveHP:
                var damageToTake = totalEnemyHealth - leaveHP;
                collider.GetComponent<StatsCounter>().AddHealth(-damageToTake);
                isTriggered = false;
                break;
            case DamageMode.leaveHPPercent:
                var damageToTakePercentInHP = (totalEnemyHealth / 100) * (100 - leaveHPPercent);
                if (totalEnemyHealth <= 1) damageToTakePercentInHP = 1;

                collider.GetComponent<StatsCounter>().AddHealth(-damageToTakePercentInHP);
                isTriggered = false;
                break;
            case DamageMode.kill:
                collider.GetComponent<StatsCounter>().AddHealth(-totalEnemyHealth);
                isTriggered = false;
                break;
            default:
                break;
        }
    }

}
