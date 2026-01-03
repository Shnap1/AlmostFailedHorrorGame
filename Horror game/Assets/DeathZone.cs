using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    List<Collider> colliders = new List<Collider>();
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
            StartCoroutine(AttackBreak(zoneDamageCooldown, other));
            isTriggered = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            colliders.Remove(other);
        }
        if (colliders.Count <= 0 && isTriggered)
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
            yield return new WaitForSeconds(time);
            // collider.GetComponent<StatsCounter>().AddHealth(-1);
            Debug.Log("Attack Break");
        }
    }

}
