using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour, IStateManagerNew
{
    SpawnSectors SpawnSectors;
    public void InitializeStates()
    {
        SpawnSectors = gameObject.AddComponent<SpawnSectors>();
        SpawnSectors.InitializeSM(this);

        //SpawnBuilding
        //SpawnLoot
        //SpawnPlayer
        //GameStart
        //GoalSelected
        //GameCycle
        //WaitingForPlayerResult


        //Death
        //ObjectiveFailed
        //Loose


        //Victory
        //GetRewards

        //GoToLobby
        //NextGame


    }

    public void SwitchState(IStateNew state)
    {

    }

    // Start is called before the first frame update
    void Awake()
    {
        // player = GameData.instance.player; //TODO: add that 
        // cam = GameData.instance.cam;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
