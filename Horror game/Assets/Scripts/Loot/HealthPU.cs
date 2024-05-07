using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPU : MonoBehaviour, IPowerUp
{
    bool wasUsed = false;
    [Range(0, 100)] public int healthPoint;
    public PowerUpSpawner powerUpSpawner;

    private void Start()
    {
        //powerUpSpowner.AddPowerUp(gameObject);
    }
    public void DoAction(int amount, GameObject DoActionTo)
    {
        if (DoActionTo.GetComponent<HealthCounter>())
        {
            DoActionTo.GetComponent<HealthCounter>().AddHealth(amount);

            Debug.Log($"DoAction in {this.name}: {amount}");
        }

        //if (DoActioTo.GetComponent<PlayerStateMachine>())
        //{
        //    DoActioTo.GetComponent<PlayerStateMachine>().AddHealth(amount);

        //    Debug.Log($"DoAction in {this.name}: {amount}");
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Sphere collided with player");
        if (other.CompareTag("Player") && !wasUsed)
        {
            DoAction(healthPoint, other.gameObject); wasUsed = true;
            GetComponent<Collider>().enabled = false;
            Destroy(gameObject, 5f);
        }
    }

}
