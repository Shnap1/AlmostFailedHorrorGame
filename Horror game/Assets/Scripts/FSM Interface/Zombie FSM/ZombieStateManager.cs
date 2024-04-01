using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ZombieStateManager : MonoBehaviour, IStateManagerNew
{
    [HideInInspector] public Zombie_Patrolling_State Patrolling;
    [HideInInspector] public Zombie_Chasing_State Chasing;
    [HideInInspector] public Zombie_Attacking_State Attacking;

    IStateNew currentState;
    [SerializeField] public Transform player;
    [HideInInspector] public Rigidbody rb;
    [SerializeField] EnemyHealthCounter enemyHealthCounter;

    [HideInInspector] public Animator anim;
    public bool seesPlayer = false;

    [SerializeField] AttackCollider attackCollider;

    public int damage = 5;

    public static Action ResetAttackTrigger;
    public static Action TurnOnAttackTrigger;

    Coroutine LookCoroutine;
    public float turnSpeed = 1f;
    bool RememberPlayerCorIsRUNNING;

    public GameObject healthBar;

    public void InitializeStates()
    {
        Patrolling = gameObject.AddComponent<Zombie_Patrolling_State>();
        Patrolling.InitializeSM(this);

        Chasing = gameObject.AddComponent<Zombie_Chasing_State>();
        Chasing.InitializeSM(this);

        Attacking = gameObject.AddComponent <Zombie_Attacking_State>();
        Attacking.InitializeSM(this);

    }

    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        InitializeStates();
        currentState = Patrolling;
        currentState.EnteState();
        agent.speed = speed;

        UpdateDamage();
    }

    void UpdateDamage()
    {
        attackCollider.damage = damage;
    }

    private void Update()
    {
        currentState.UpdaterState();

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);


    }

    public void SwitchState(IStateNew state)
    {
        if (currentState != null)
        {
            if (currentState != state)
            {
                currentState.ExitState();

                currentState = state;
                currentState.EnteState();
            }
        }

    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        //Debug.Log("OnTriggerEnter;");
    //        if (!seesPlayer)
    //        {
    //            seesPlayer = true;
    //            StopCoroutine(RememberPlayer());
    //        }
    //    }
    //}
    
    //private void OnTriggerExit(Collider other)
    //{
        
    //    if (other.CompareTag("Player"))
    //    {
    //        //Debug.Log("OnTriggerExit");
    //        //Debug.Log("OnTriggerExit");

    //        if (seesPlayer && !RememberPlayerCorIsRUNNING)
    //        {
    //            Debug.Log("OnTriggerExit  ----- StartCoroutine(RememberPlayer())");
    //            StartCoroutine(RememberPlayer());
    //        }
    //    }
    //}

    void TakeDamage(int addedDamage)
    {
        enemyHealthCounter.TakeDamage(addedDamage);
    }

    //IEnumerator RememberPlayer()
    //{
    //    //seePlayer = true;
    //    //Debug.Log("WaitForSeconds(10)");

    //    //if (seePlayer)
    //    //{
    //    //    yield return new WaitForSeconds(20);
    //    //    StopCoroutine(RememberPlayer());
    //    //    Debug.Log("seePlayer = false");
    //    //    seePlayer = false;
    //    //}
    //    while (seesPlayer)
    //    {
    //        RememberPlayerCorIsRUNNING = true;
    //        Debug.Log("RememberPlayerCorIsRUNNING = true;");
    //        yield return new WaitForSeconds(5);
    //        Debug.Log("seePlayer = false");
    //        seesPlayer = false;
    //        RememberPlayerCorIsRUNNING = false;
    //        Debug.Log("RememberPlayerCorIsRUNNING = false;");

    //    }
    //}

    
    public void StartRotating()
    {
        if (LookCoroutine != null)
        {
            StopCoroutine(LookCoroutine);
        }
        LookCoroutine = StartCoroutine(LookAt());
    }
    
    IEnumerator LookAt()
    {
        Quaternion lookRotation = Quaternion.LookRotation(player.position - transform.position);
        float time = 0;
        while(time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);
            time += Time.deltaTime * turnSpeed;
            yield return null;
        }
    }

    #region ENEMY AI TUTORIL script DATA

    public NavMeshAgent agent;


    public LayerMask whatIsGround, whatIsPlayer;

    //public float health;

    public int speed;
    public int chaseSpeed;
    //Patroling
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    public bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float attackRange, sightRange;
    public bool playerInAttackRange, playerInSightRange;


    private void Awake()
    {
        player = GameObject.Find("PLAYER").transform;
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        //agent.acceleration = speed;
        agent.speed = speed;

    }



    public void TurnOnTriggerFunc()
    {
        TurnOnAttackTrigger?.Invoke();
    }
    public void ResetTriggerFunc()
    {
        ResetAttackTrigger?.Invoke();
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    #endregion

}
