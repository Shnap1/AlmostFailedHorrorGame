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
        if (hp > 0)
        {
            hp -= damageHP;
        }
    }

    public void TakeDamage(int damageHP, GunType gunType)
    {
        TakeDamage(damageHP);
    }

}
