using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public GameObject target;
    public static Action onTargetCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Sphere collided with player");
            GetComponent<Collider>().enabled = false;
            GameLoopManager.TargetCollected();
            Destroy(target);
        }
    }

}
