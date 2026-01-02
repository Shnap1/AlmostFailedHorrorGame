using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunProjectile : MonoBehaviour
{

    public Transform target;
    public GameObject projectile;


    bool alreadyAttacked = false;
    public float timeBetweenAttacks;
    public float attackTime;


    public Transform ChooseTarget(Transform _target)
    {
        return _target;
    }
    void Start()
    {
        ChooseTarget(target);
    }

    public void StartAttacking()
    {
        StartCoroutine(AttackCoroutine(attackTime));
    }
    public IEnumerator AttackCoroutine(float f)
    {
        AttackTarget();
        yield return new WaitForSeconds(f);

    }

    void OnDestroy()
    {
        StopCoroutine(AttackCoroutine(attackTime));
    }

    [SerializeField]
    private void AttackTarget()
    {
        transform.LookAt(target);

        if (!alreadyAttacked)
        {
            ///Attack code here
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
