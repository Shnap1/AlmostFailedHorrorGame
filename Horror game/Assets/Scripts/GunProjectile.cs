using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunProjectile : Ability
{

    public bool attackOnStart = true;
    public bool attackOn;

    public Transform target;
    public GameObject projectile;

    public float timeBetweenAttacks;

    [SerializeField] public float forwardForce = 40f;
    [SerializeField] public float upwardForce = 10f;

    public Transform ChooseTarget(Transform newTarget)
    {
        target = newTarget;
        return target;
    }
    void Start()
    {
        // ChooseTarget(target);
        // if (attackOnSTart) StartAttacking();
        attackOn = attackOnStart;
        StartCoroutine(AttackCoroutine(timeBetweenAttacks));
    }

    [ContextMenu("Restart Attacking Coroutine")]
    public void RestartAttackingCoroutine()
    {
        StopCoroutine(AttackCoroutine(timeBetweenAttacks));
        StopAllCoroutines();
        attackOn = false;

        attackOn = true;
        StartCoroutine(AttackCoroutine(timeBetweenAttacks));
        Debug.Log("Restart Attacking Coroutine");
    }


    // public void StartAttacking()
    // {
    //     StartCoroutine(AttackCoroutine(1));
    // }
    public IEnumerator AttackCoroutine(float time)
    {
        while (attackOn)
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
        rb.AddForce(transform.forward * forwardForce, ForceMode.Impulse);
        rb.AddForce(transform.up * upwardForce, ForceMode.Impulse);
    }
    void OnDestroy()
    {
        StopCoroutine(AttackCoroutine(timeBetweenAttacks));
    }

}
