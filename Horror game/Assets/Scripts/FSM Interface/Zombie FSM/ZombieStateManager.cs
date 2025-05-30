using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ZombieStateManager : MonoBehaviour, IStateManagerNew
{
    public Transform cam;
    [SerializeField] EnemyHealthBar healthBarUI;
    [HideInInspector] public Zombie_Patrolling_State Patrolling;
    [HideInInspector] public Zombie_Chasing_State Chasing;
    [HideInInspector] public Zombie_Attacking_State Attacking;

    IStateNew currentState;

    public static event Action<IStateNew> onZombieStateChanged;
    [SerializeField] public Transform player;
    [HideInInspector] public Rigidbody rb;
    [SerializeField] EnemyHealthCounter enemyHealthCounter;

    [HideInInspector] public Animator anim;
    public bool seesPlayer = false;

    [SerializeField] AttackCollider attackCollider;

    public int damage;

    public static Action ResetAttackTrigger;
    public static Action TurnOnAttackTrigger;

    Coroutine LookCoroutine;
    public float turnSpeed = 1f;
    bool RememberPlayerCorIsRUNNING;

    public GameObject healthBar;
    public PatrolPointManager patrolPointManager;
    public Zombie_Patrolling_State.Patrollers patrollerType;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        //agent.acceleration = speed;
        agent.speed = speed;

    }

    //TODO create a way to pass objects from the SCENE like main camera and player to instantiated prefabs like enemy.
    public void InitializeStates()
    {
        Patrolling = gameObject.AddComponent<Zombie_Patrolling_State>();
        Patrolling.InitializeSM(this);

        Chasing = gameObject.AddComponent<Zombie_Chasing_State>();
        Chasing.InitializeSM(this);

        Attacking = gameObject.AddComponent<Zombie_Attacking_State>();
        Attacking.InitializeSM(this);

    }


    private void Start()
    {

        player = GameData.instance.player;
        cam = GameData.instance.cam;
        healthBarUI.cam = cam;
        patrolPointManager = GameData.instance.patrolPointManager;

        // Patrolling.SetPatrollingType(patrollerType);

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        InitializeStates();
        currentState = Patrolling; //TODO remove or comment out
        currentState = Chasing; //changed to chasing

        currentState.EnterState();
        agent.speed = speed;

        UpdateDamage(damage);
        onZombieStateChanged?.Invoke(currentState);

        healthBar.SetActive(false);
    }

    public void UpdateDamage(int _damage)
    {
        if (_damage > 0) damage += _damage;
        attackCollider.damage = damage;
    }

    private void Update()
    {
        currentState.UpdateState();

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);


    }

    public void Switch_IState(IStateNew state)
    {
        if (currentState != null)
        {
            if (currentState != state)
            {
                currentState.ExitState();

                currentState = state;
                currentState.EnterState();
            }
        }

        onZombieStateChanged?.Invoke(currentState);
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

    public void TakeDamage(int addedDamage)
    {
        enemyHealthCounter.NewTakeDamage(addedDamage);
        Debug.Log($"ZombieStateManager: TakeDamage(int addedDamage{addedDamage} TOTAL HEALTH = {enemyHealthCounter.totalHealth}");

        if (enemyHealthCounter.totalHealth <= 0)
        {
            DestroyEnemy();
        }
    }

    public void SetPatrollingType(Zombie_Patrolling_State.Patrollers patrollerType)
    {
        // this.patrollerType = patrollerType; // TODO: UNCOMMENT THIS
        this.patrollerType = Zombie_Patrolling_State.Patrollers.playerFollower;

        // Patrolling.SetPatrollingType(this.patrollerType);
        //TODO figure out whether this shit can set the patrollerType in the Zombie_Patrolling_State.cs
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
        while (time < 1)
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
        Debug.Log("DestroyEnemy()");
        GameData.instance.EnemyKilled();
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    public void SwitchState(GAMEFSM_Base_State state)
    {
        // throw new NotImplementedException();
    }

    #endregion

}
