using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nuclearSmokePowerUp : MonoBehaviour
{
    public bool reactorActive;
    public ParticleSystem nuclearSmokeParticle; // Assign this in the Inspector
    public int impact;

    public int healthDepletionRate; // Health lost per second
    public float intervalInSeconds; // How often to reduce health
    public HealthCounter playerHealthCounter;

    private Coroutine depleteHealthCoroutine; // Reference to the running coroutine

    void Start()
    {
        ActivateReactor(reactorActive);
    }

    void OnTriggerEnter(Collider other)
    {
        if (reactorActive)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                playerHealthCounter = other.GetComponent<HealthCounter>();
                // Start the coroutine and store the reference
                depleteHealthCoroutine = StartCoroutine(DepleteHealth());
            }
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.GetComponent<ZombieStateManager>().UpdateDamage(impact);
            }
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
            DepletePlayerHealth();
        }
    }

    private void DepletePlayerHealth()
    {
        if (playerHealthCounter != null)
        {
            playerHealthCounter.TakeDamage(healthDepletionRate);
        }
    }

    public void ActivateReactor(bool _reactorActive)
    {
        reactorActive = _reactorActive;
        if (reactorActive)
        {
            nuclearSmokeParticle.Play();
        }
        else
        {
            nuclearSmokeParticle.Stop();
        }
    }
}