using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPU : MonoBehaviour, IPowerUp
{
    bool wasUsed = false;
    [Range(0,100)]public int healthPoint;
    public PowerUpSpowner powerUpSpowner;

    private void Start()
    {
        //PowerUpSpowner.AddPowerUp(gameObject);
        powerUpSpowner.AddPowerUp(gameObject);
    }
    public void DoAction(int amount, GameObject DoActioTo)
    {
        if (DoActioTo.GetComponent<HealthCounter>())
        {
            DoActioTo.GetComponent<HealthCounter>().AddHealth(amount);

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
        if (other.CompareTag("Player") && !wasUsed) { DoAction(healthPoint, other.gameObject); wasUsed = true; } 
        GetComponent<Collider>().enabled = false;
    }



    //public enum HealthPUEnum
    //{
    //    TypeAss,
    //    TypeTitt,
    //}

    //public HealthPUEnum thisPUType;
}
