using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class NavmeshBuilder : MonoBehaviour
{
    public NavMeshSurface surface;
    // Start is called before the first frame update
    void Start()
    {
        // GameLoopManager.OnGameUpdate += BuildNavmesh;


    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.N)) BuildNavmesh(GameLoopManager.GameState.GameStart);
    }
    void OnDestroy()
    {
        // GameLoopManager.OnGameUpdate -= BuildNavmesh;
    }


    public void BuildNavmesh(GameLoopManager.GameState gameState)
    {
        // if (gameState == GameLoopManager.GameState.GameStart)
        // {
        //     if (surface == null) surface = GetComponent<NavMeshSurface>();
        //     if (surface == null) return;
        //     NavMeshData data = surface.navMeshData;
        //     surface.UpdateNavMesh(data);
        // }
    }
}
