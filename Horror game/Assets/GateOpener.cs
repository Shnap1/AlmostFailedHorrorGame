using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpener : MonoBehaviour
{
    public static GateOpener instance;

    public Animator animator;
    [SerializeField] bool gateOpen;
    public GameObject gate;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
        // animator = GetComponent<Animator>();
        GameLoopManager.OnGameUpdate += OpenCloseGate;
        GameLoopManager.OnGameUpdate += OpenCloseGate;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.O))
        {
            OpenGate();
            animator.SetBool("testBool", true);

        }
        else if (Input.GetKey(KeyCode.C))
        {
            CloseGate();
            animator.SetBool("testBool", false);
        }
    }
    void OnDestroy()
    {
        GameLoopManager.OnGameUpdate -= OpenCloseGate;
        GameLoopManager.OnGameUpdate -= OpenCloseGate;
    }

    void OpenCloseGate(GameLoopManager.GameState gameState)
    {
        switch (gameState)
        {
            case GameLoopManager.GameState.GatesOpen:
                OpenGate();
                break;
            case GameLoopManager.GameState.GameStart:
                CloseGate();
                break;
            case GameLoopManager.GameState.LootCollected:
                OpenGate();
                break;
            case GameLoopManager.GameState.Victory:
                CloseGate();
                break;
            default:
                break;
        }
    }
    public void OpenGate()
    {
        {
            if (gateOpen == false) animator.SetBool("openGate", true);

            gateOpen = true;
            animator.SetBool("openGate", true);
            // Debug.Log($"OpenGate({gateOpen})");
            animator.SetBool("testBool", true);

        }
    }

    public void CloseGate()
    {
        {

            gateOpen = false;
            animator.SetBool("openGate", false);
            // Debug.Log($"CloseGate({gateOpen})");
            animator.SetBool("testBool", false);


        }
    }

}
