using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventStation : MonoBehaviour, Ishootable
{
    [Header("COLLISIONS")]
    public UnityEvent<GameObject> onTrigger;
    public UnityEvent<GameObject> onExit;

    public UnityEvent<GameObject> onCollision;
    public UnityEvent<GameObject> onCollisionExit;

    [Header("DAMAGE")]
    public UnityEvent<int> onTakeDamage;
    public UnityEvent<int, GunType> onTakeDamageWithGunType;

    private void OnTriggerEnter(Collider other)
    {
        onTrigger.Invoke(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        onExit.Invoke(other.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        onCollision.Invoke(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        onCollisionExit.Invoke(collision.gameObject);
    }



    public void TakeDamage(int damageHP)
    {
        onTakeDamage.Invoke(damageHP);
    }

    public void TakeDamage(int damageHP, GunType gunType)
    {
        onTakeDamageWithGunType.Invoke(damageHP, gunType);
    }
}
