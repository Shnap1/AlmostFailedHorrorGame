using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageObject : MonoBehaviour, Ishootable
{

    public int hp = 10;
    void Start()
    {

    }

    public void TakeDamage(int damageHP)
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(int damageHP, GunType gunType)
    {
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
