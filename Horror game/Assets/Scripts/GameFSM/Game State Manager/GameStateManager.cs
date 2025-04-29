using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour, IStateManagerNew
{
    [HideInInspector] public SpawnSectors SpawnSectors;
    [HideInInspector] public SpawnBuilding SpawnBuilding;
    [HideInInspector] public SpawnLoot SpawnLoot;
    [HideInInspector] public SpawnPlayer SpawnPlayer;
    [HideInInspector] public GameStart GameStart;
    [HideInInspector] public GoalSelected GoalSelected;
    [HideInInspector] public GameCycle GameCycle;
    [HideInInspector] public WaitingForPlayerResult WaitingForPlayerResult;


    [HideInInspector] public Death Death;
    [HideInInspector] public ObjectiveFailed ObjectiveFailed;
    [HideInInspector] public Loose Loose;


    [HideInInspector] public Victory Victory;
    [HideInInspector] public GetRewards GetRewards;

    [HideInInspector] public GoToLobby GoToLobby;
    [HideInInspector] public NextGame NextGame;

    IStateNew currentState;


    public void InitializeStates()
    {
        SpawnSectors = gameObject.AddComponent<SpawnSectors>();
        SpawnSectors.InitializeSM(this);

        //SpawnBuilding
        SpawnBuilding = gameObject.AddComponent<SpawnBuilding>();
        SpawnBuilding.InitializeSM(this);

        //SpawnLoot
        SpawnLoot = gameObject.AddComponent<SpawnLoot>();
        SpawnLoot.InitializeSM(this);


        //SpawnPlayer
        SpawnPlayer = gameObject.AddComponent<SpawnPlayer>();
        SpawnPlayer.InitializeSM(this);

        //GameStart
        GameStart = gameObject.AddComponent<GameStart>();
        GameStart.InitializeSM(this);
        //GoalSelected
        GoalSelected = gameObject.AddComponent<GoalSelected>();
        GoalSelected.InitializeSM(this);
        //GameCycle
        GameCycle = gameObject.AddComponent<GameCycle>();
        GameCycle.InitializeSM(this);
        //WaitingForPlayerResult
        WaitingForPlayerResult = gameObject.AddComponent<WaitingForPlayerResult>();
        WaitingForPlayerResult.InitializeSM(this);


        //Death
        Death = gameObject.AddComponent<Death>();
        Death.InitializeSM(this);
        //ObjectiveFailed
        ObjectiveFailed = gameObject.AddComponent<ObjectiveFailed>();
        ObjectiveFailed.InitializeSM(this);
        //Loose
        Loose = gameObject.AddComponent<Loose>();
        Loose.InitializeSM(this);


        //Victory
        Victory = gameObject.AddComponent<Victory>();
        Victory.InitializeSM(this);
        //GetRewards
        GetRewards = gameObject.AddComponent<GetRewards>();
        GetRewards.InitializeSM(this);

        //GoToLobby
        GoToLobby = gameObject.AddComponent<GoToLobby>();
        GoToLobby.InitializeSM(this);
        //NextGame
        NextGame = gameObject.AddComponent<NextGame>();
        NextGame.InitializeSM(this);




    }

    public void SwitchState(IStateNew state)
    {
        if (currentState != state)
        {
            currentState = state;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        // player = GameData.instance.player; //TODO: add that 
        // cam = GameData.instance.cam;
    }
    void Start()
    {
        InitializeStates();
        SwitchState(SpawnSectors);
        Debug.Log("CurrentState in StateManager--" + currentState);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
