using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public int damage = 1;
    [SerializeField]ZombieStateManager SM;
    SphereCollider collider;
    //public static Action<int>onAttackColliderTriggered;
    //List<Collider> players;
    List<GameObject> hitList = new List<GameObject>();
    bool canAttack;
    private void OnEnable()
    {
        Zombie_Attacking_State.onAttack += ActivateAttackCollider;
        ZombieStateManager.ResetAttackTrigger += ResetAttack;
        ZombieStateManager.TurnOnAttackTrigger += TurnOnAttackTrigger;
    }
    private void OnDisable()
    {
        Zombie_Attacking_State.onAttack -= ActivateAttackCollider;
        ZombieStateManager.ResetAttackTrigger -= ResetAttack;
        ZombieStateManager.TurnOnAttackTrigger += TurnOnAttackTrigger;

    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            if (!hitList.Contains(other.gameObject) && canAttack)
            {
                hitList.Add(other.gameObject);
                //Debug.Log("OnTriggerEnter" + damage);
                //if (!players.Contains(other))///
                //{
                //    players.Add(other);
                //}
                //other.GetComponent<PlayerStateMachine>().TakeDamage(damage);
                other.GetComponent<HealthCounter>().TakeDamage(damage);
            }

        }

    }
    void Start()
    {
        damage = gameObject.GetComponentInParent<ZombieStateManager>().damage;
        collider = GetComponent<SphereCollider>();

    }

    //private void ActivateAttackCollider(bool isActive)
    //{
    //    collider.enabled = isActive;
    //    if (!isActive && players!=null)
    //    {
    //        players.Clear();
    //    }
    //}

    private void ActivateAttackCollider(bool isActive)
    {
        collider.enabled = isActive;

    }



    void TurnOnAttackTrigger() { canAttack = true; }
    void ResetAttack() { canAttack = false; hitList.Clear();}
}
