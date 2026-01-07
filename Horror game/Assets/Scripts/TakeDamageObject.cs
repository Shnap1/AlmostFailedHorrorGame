using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TakeDamageObject : MonoBehaviour, Ishootable
{
    public UnityEvent<int> onTakeDamage;
    // public UnityEvent<int, GunType> onTakeDamageWithGunType;
    public UnityEvent onDead;

    public int hp = 10;
    void Start()
    {

    }

    public void TakeDamage(int damageHP)
    {
        Debug.Log("TakeDamage in TakeDamageObject");
        if (hp > 0)
        {
            hp -= damageHP;
            onTakeDamage.Invoke(damageHP);
        }
        if (hp <= 0)
        {
            onDead.Invoke();
        }
    }

    public void TakeDamage(int damageHP, GunType gunType)
    {
        TakeDamage(damageHP);
    }

}
