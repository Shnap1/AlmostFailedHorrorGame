using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public GameObject target;
    public static Action onTargetCollected;
    bool collected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !collected)
        {
            //Debug.Log("Sphere collided with player");
            collected = true;
            GameLoopManager.TargetCollected();
            // GetComponent<Collider>().enabled = false;
            Destroy(target);
        }
    }

}
