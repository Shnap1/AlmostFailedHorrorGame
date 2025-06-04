using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineExample : MonoBehaviour
{
    public int impact;
    public int healthDepletionRate; // Health lost per second
    public float intervalInSeconds; // How often to reduce health

    StatsCounter playerHealthCounter;

    private Coroutine depleteHealthCoroutine; // REFERENCE to the running coroutine


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerHealthCounter = other.GetComponent<StatsCounter>();

            depleteHealthCoroutine = StartCoroutine(DepleteHealth()); // START  coroutine and store the reference
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && depleteHealthCoroutine != null)
        {
            // Stop the specific coroutine instance
            StopCoroutine(depleteHealthCoroutine);
            depleteHealthCoroutine = null;
        }
    }

    private IEnumerator DepleteHealth()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervalInSeconds);
            DepletePlayerHealth(); // Сам ёбаный метод


        }
    }

    private void DepletePlayerHealth()
    {
        if (playerHealthCounter != null)
        {
            playerHealthCounter.TakeDamage(healthDepletionRate);
        }
    }


}
