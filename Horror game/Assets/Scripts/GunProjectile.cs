using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunProjectile : MonoBehaviour
{

    public bool attackOnSTart = true;
    public Transform target;
    public GameObject projectile;

    public float timeBetweenAttacks = 1f;

    public Transform ChooseTarget(Transform newTarget)
    {
        target = newTarget;
        return target;
    }
    void Start()
    {
        // ChooseTarget(target);
        // if (attackOnSTart) StartAttacking();
        StartCoroutine(AttackCoroutine(1));
    }


    // public void StartAttacking()
    // {
    //     StartCoroutine(AttackCoroutine(1));
    // }
    public IEnumerator AttackCoroutine(float time)
    {
        while (true)
        {
            AttackTarget();
            // Debug.Log("Attacking");
            yield return new WaitForSeconds(time);
        }
    }


    private void AttackTarget()
    {
        transform.LookAt(target);
        Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
        rb.AddForce(transform.up * 8f, ForceMode.Impulse);
    }
    void OnDestroy()
    {
        StopCoroutine(AttackCoroutine(timeBetweenAttacks));
    }

}
