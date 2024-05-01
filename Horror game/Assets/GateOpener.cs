using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpener : MonoBehaviour
{
    public Animator animator;
    bool gateOpen;

    void Start()
    {
        animator = GetComponent<Animator>();
        GameLoopManager.OnGameUpdate += Open;
        GameLoopManager.OnGameUpdate += Close;
    }
    void OnDestroy()
    {
        GameLoopManager.OnGameUpdate -= Open;
        GameLoopManager.OnGameUpdate -= Close;
    }

    public void Open(GameLoopManager.GameState gameState){
        if(gameState == GameLoopManager.GameState.GatesOpen || gameState == GameLoopManager.GameState.LootCollected )
        {
            gateOpen = true;
            animator.SetBool("openGate", true);
            Debug.Log("void Open(GameLoopManager.GameState gameState)");
        }
    }

    public void Close(GameLoopManager.GameState gameState){
        if(gameState == GameLoopManager.GameState.GameStart || gameState == GameLoopManager.GameState.Victory)
        {
        gateOpen = false;
        animator.SetBool("openGate", false);
        }
    }

}
