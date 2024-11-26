using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZSMReference : MonoBehaviour
{
    // Start is called before the first frame update
    public ZombieStateManager ZSM;

    public void TakeDamage(int damage)
    {
        Debug.Log("ZSMReference TakeDamage(int damage)");
        ZSM.TakeDamage(damage);

    }
}
