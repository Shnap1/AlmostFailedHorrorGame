using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    public PlayerSpawner _playerTransform;
    void Start()
    {
        // if (GameData.instance.playerSpawner != null && gameObject.activeSelf == true)
        // {

        //     GameData.instance.playerSpawner.spawnPoints.Add(transform);
        // }
        _playerTransform = GameData.instance.GetPlayerSpawner();
        _playerTransform.spawnPoints.Add(transform);



    }
    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.U))
    //     {
    //         _playerTransform = GameData.instance.GetPlayerSpawner();
    //         _playerTransform.spawnPoints.Add(transform);
    //     }
    // }


}
