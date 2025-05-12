using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameStateManager : MonoBehaviour, IStateManagerNew
{

    [Header("Scripts for states")]
    public SectorSpawn sectorSpawn;
    public GameLoopManager gameLoopManager;
    public LootSpawner lootSpawner;
    // public GateOpener gateOpener;
    public GatesNew gatesNew;
    public PlayerSpawner playerSpawner;
    public HealthBar healthBar;
    public LootCounter lootCounter;
    public SettingsMenu settingsMenu;


    [HideInInspector] public SpawnSectors_State SpawnSectors;
    // [HideInInspector] public SpawnBuilding_State SpawnBuilding; //skipping for now
    [HideInInspector] public SpawnLootAndEnemies_State SpawnLootAndEnemies;
    [HideInInspector] public SpawnPlayer_State SpawnPlayer; //put this above

    [HideInInspector] public GameStart_State GameStart;
    // [HideInInspector] public GoalSelected_State GoalSelected;
    [HideInInspector] public GameCycle_State GameCycle;
    // [HideInInspector] public WaitingForPlayerResult_State WaitingForPlayerResult;


    [HideInInspector] public Death_State Death;
    [HideInInspector] public ObjectiveFailed_State ObjectiveFailed;
    [HideInInspector] public Loose_State Loose;


    [HideInInspector] public Victory_State Victory;
    [HideInInspector] public GetRewards_State GetRewards;

    [HideInInspector] public GoToLobby_State GoToLobby;
    [HideInInspector] public NextGame_State NextGame;

    public GAMEFSM_Base_State currentState;




    public enum GameModes
    {
        KillMonstersMode,
        FootballMode,
        TowerDefense,
        ProtectTargetMode,
        // CustomMode,
    }
    public GameModes currentGameMode;

    public void SetGameMode(GameModes gameMode)
    {
        currentGameMode = gameMode;
        // if (gameMode != currentGameMode)
        // {
        // }
    }
    public void InitializeStates()
    {
        InitializeStatesWithMode(currentGameMode);
    }
    public void InitializeStatesWithMode(GameModes gameMode)
    {
        switch (gameMode)
        {
            case GameModes.KillMonstersMode:
                SpawnSectors = gameObject.AddComponent<SpawnSectors_State>();
                SpawnSectors.InitializeSM(this);

                //SpawnBuilding
                // SpawnBuilding = gameObject.AddComponent<SpawnBuilding_State>();
                // SpawnBuilding.InitializeSM(this);

                //SpawnLoot
                SpawnLootAndEnemies = gameObject.AddComponent<SpawnLootAndEnemies_State>();
                SpawnLootAndEnemies.InitializeSM(this);


                //SpawnPlayer
                SpawnPlayer = gameObject.AddComponent<SpawnPlayer_State>();
                SpawnPlayer.InitializeSM(this);

                //GameStart
                GameStart = gameObject.AddComponent<GameStart_State>();
                GameStart.InitializeSM(this);
                //GoalSelected
                // GoalSelected = gameObject.AddComponent<GoalSelected_State>();
                // GoalSelected.InitializeSM(this);

                //GameCycle
                GameCycle = gameObject.AddComponent<GameCycle_State>();
                GameCycle.InitializeSM(this);

                //WaitingForPlayerResult
                // WaitingForPlayerResult = gameObject.AddComponent<WaitingForPlayerResult_State>();
                // WaitingForPlayerResult.InitializeSM(this);


                //Death
                Death = gameObject.AddComponent<Death_State>();
                Death.InitializeSM(this);

                //ObjectiveFailed
                // ObjectiveFailed = gameObject.AddComponent<ObjectiveFailed_State>();
                // ObjectiveFailed.InitializeSM(this);

                //Loose
                Loose = gameObject.AddComponent<Loose_State>();
                Loose.InitializeSM(this);


                //Victory
                Victory = gameObject.AddComponent<Victory_State>();
                Victory.InitializeSM(this);
                //GetRewards
                // GetRewards = gameObject.AddComponent<GetRewards_State>();
                // GetRewards.InitializeSM(this);

                //GoToLobby
                // GoToLobby = gameObject.AddComponent<GoToLobby_State>();
                // GoToLobby.InitializeSM(this);

                //NextGame
                NextGame = gameObject.AddComponent<NextGame_State>();
                NextGame.InitializeSM(this);
                break;

        }





    }

    public void Switch_IState(IStateNew state)
    {

    }
    public void SwitchState(GAMEFSM_Base_State state)
    {
        if (currentState)
        {
            currentState.ExitState();
        }
        currentState = state;
        currentState.EnterState();
        if (currentState != null)
        {
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
        SetGameMode(GameModes.KillMonstersMode);
        InitializeStates();

        SwitchState(SpawnSectors);
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState();
        }
        else
        {
            Debug.Log("currentState == null");
        }
    }
}
