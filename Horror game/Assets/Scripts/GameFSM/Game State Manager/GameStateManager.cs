using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour, IStateManagerNew
{

    [Header("Scripts for states")]
    public SectorSpawn sectorSpawn;

    [HideInInspector] public SpawnSectors_State SpawnSectors;
    [HideInInspector] public SpawnBuilding_State SpawnBuilding;
    [HideInInspector] public SpawnLoot_State SpawnLoot;
    [HideInInspector] public SpawnPlayer_State SpawnPlayer;
    [HideInInspector] public GameStart_State GameStart;
    [HideInInspector] public GoalSelected_State GoalSelected;
    [HideInInspector] public GameCycle_State GameCycle;
    [HideInInspector] public WaitingForPlayerResult_State WaitingForPlayerResult;


    [HideInInspector] public Death_State Death;
    [HideInInspector] public ObjectiveFailed_State ObjectiveFailed;
    [HideInInspector] public Loose_State Loose;


    [HideInInspector] public Victory_State Victory;
    [HideInInspector] public GetRewards_State GetRewards;

    [HideInInspector] public GoToLobby_State GoToLobby;
    [HideInInspector] public NextGame_State NextGame;

    GAMEFSM_Base_State currentState;





    public void InitializeStates()
    {
        SpawnSectors = gameObject.AddComponent<SpawnSectors_State>();
        SpawnSectors.InitializeSM(this);

        //SpawnBuilding
        SpawnBuilding = gameObject.AddComponent<SpawnBuilding_State>();
        SpawnBuilding.InitializeSM(this);

        //SpawnLoot
        SpawnLoot = gameObject.AddComponent<SpawnLoot_State>();
        SpawnLoot.InitializeSM(this);


        //SpawnPlayer
        SpawnPlayer = gameObject.AddComponent<SpawnPlayer_State>();
        SpawnPlayer.InitializeSM(this);

        //GameStart
        GameStart = gameObject.AddComponent<GameStart_State>();
        GameStart.InitializeSM(this);
        //GoalSelected
        GoalSelected = gameObject.AddComponent<GoalSelected_State>();
        GoalSelected.InitializeSM(this);
        //GameCycle
        GameCycle = gameObject.AddComponent<GameCycle_State>();
        GameCycle.InitializeSM(this);
        //WaitingForPlayerResult
        WaitingForPlayerResult = gameObject.AddComponent<WaitingForPlayerResult_State>();
        WaitingForPlayerResult.InitializeSM(this);


        //Death
        Death = gameObject.AddComponent<Death_State>();
        Death.InitializeSM(this);
        //ObjectiveFailed
        ObjectiveFailed = gameObject.AddComponent<ObjectiveFailed_State>();
        ObjectiveFailed.InitializeSM(this);
        //Loose
        Loose = gameObject.AddComponent<Loose_State>();
        Loose.InitializeSM(this);


        //Victory
        Victory = gameObject.AddComponent<Victory_State>();
        Victory.InitializeSM(this);
        //GetRewards
        GetRewards = gameObject.AddComponent<GetRewards_State>();
        GetRewards.InitializeSM(this);

        //GoToLobby
        GoToLobby = gameObject.AddComponent<GoToLobby_State>();
        GoToLobby.InitializeSM(this);
        //NextGame
        NextGame = gameObject.AddComponent<NextGame_State>();
        NextGame.InitializeSM(this);




    }

    public void Switch_IState(IStateNew state)
    {

    }
    public void SwitchState(GAMEFSM_Base_State state)
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
        // Debug.Log("CurrentState in StateManager--" + currentState);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();
    }
}
