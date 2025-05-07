using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ENEMY_STATE_MANAGER : MonoBehaviour
{
    public enum EnemyEnums
    {
        Chase,
        Attack,
        Patroling
    }

    public enum ChasingType
    {
        Follower, //always knows where the player is and follows it
        PointChecker, //knows the last point player triggerred and goes there
        FreePointWanderer //goes to point that has the least amount of other enemies then goes there, then chooses another free random point


    }
    BaseStateNEW currentState;
    Dictionary<EnemyEnums, BaseStateNEW> enemyStates = new Dictionary<EnemyEnums, BaseStateNEW>();

    public void InitializeStates()
    {
        enemyStates[EnemyEnums.Chase] = new ENEMY_CHASE_STATE(this);
        enemyStates[EnemyEnums.Patroling] = new ENEMY_PATROLLING_STATE(this);
        enemyStates[EnemyEnums.Attack] = new ENEMY_ATTACK_STATE(this);
    }
    private void Start()
    {
        InitializeStates();
        currentState = Patroling();
        Debug.Log("ENEMY MANAGER Start()");
        currentState.EnterState();

        //ResetPositionCoroutine(1);
        StartCoroutine(ResetingPath(1));
    }


    public void SwitchStates(BaseStateNEW newState)
    {
        if (currentState == null) Debug.Log("current Enemy State is null");
        if (newState != currentState)
        {
            currentState.ExitState();
            currentState = newState;
            currentState.EnterState();
        }

    }

    #region ENEMY AI TUTORIL script DATA

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    [SerializeField] int speed;
    //Patroling
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    public bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("PLAYER").transform;
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        //agent.acceleration = speed;


    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        currentState.UpdateState();

        //if (!playerInSightRange && !playerInAttackRange) Patroling();
        //if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        //if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    //public void PatrolingFunction()
    //{
    //    if (!walkPointSet) SearchWalkPoint();

    //    if (walkPointSet)
    //        agent.SetDestination(walkPoint);

    //    Vector3 distanceToWalkPoint = transform.position - walkPoint;

    //    //Walkpoint reached
    //    if (distanceToWalkPoint.magnitude < 1f)
    //        walkPointSet = false;
    ////}
    //public void SearchWalkPoint()
    //{
    //    //Calculate random point in range
    //    float randomZ = Random.Range(-walkPointRange, walkPointRange);
    //    float randomX = Random.Range(-walkPointRange, walkPointRange);

    //    walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

    //    if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
    //        walkPointSet = true;
    //}

    //public void ChasePlayer()
    //{
    //    agent.SetDestination(player.position);
    //}

    public void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

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

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Invoke(nameof(DestroyEnemy), 0.5f);
        }
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

    //public void Fart()
    //{

    //}



    IEnumerator ResetingPath(int patrolingTime)
    {
        yield return new WaitForSeconds(patrolingTime);
        Debug.Log("    IEnumerator ResetingPath(int patrolingTime)\r\n");
    }

    #endregion

    #region STATES FACTORY
    public BaseStateNEW Chase()
    {
        return enemyStates[EnemyEnums.Chase];
    }

    public BaseStateNEW Attack()
    {
        return enemyStates[EnemyEnums.Attack];
    }

    public BaseStateNEW Patroling()
    {
        return enemyStates[EnemyEnums.Patroling];
    }



    #endregion
}
