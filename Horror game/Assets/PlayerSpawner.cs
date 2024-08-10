using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public bool ShouldSpawnPlayer = false;
    public CinemachineFreeLook vcam;
    public static PlayerSpawner instance;

    public GameLoopManager gameLoopManager;
    public CameraDynamics cameraDynamics;

    public Transform Player;

    public Transform ExampleLookatObject;
    public GameObject Gate;
    public List<Transform> spawnPoints = new List<Transform>();

    void OnEnable()
    {
        GameLoopManager.OnGameUpdate += SpawnPlayer;
    }
    void OnDisable()
    {
        GameLoopManager.OnGameUpdate -= SpawnPlayer;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (ShouldSpawnPlayer) SpawnStartingGates();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.T)) SpawnStartingGates();
    }

    public void SpawnStartingGates()
    {
        if (spawnPoints.Count == 0)
        {
            Debug.LogWarning("No spawn points defined");
            return;
        }

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        Gate.SetActive(true);
        Gate.transform.position = spawnPoint.position;
        var instatiatedPlayer = Instantiate(Player, spawnPoint.position, Quaternion.identity);
        // cameraDynamics.focusObjectTransform = Player.transform;
        cameraDynamics.SetLooktObject(instatiatedPlayer);
        // vcam.LookAt = Player.transform;
        // vcam.Follow = Player.transform;
        // Player.transform.position = spawnPoint.position;
    }
    public void SpawnExitGates()
    {
        if (Gate.activeSelf == false)
        {

            Gate.SetActive(true);
        }

    }

    public void SpawnPlayer()
    {

    }

    public void SpawnPlayer(GameLoopManager.GameState gameState)
    {

    }



}
