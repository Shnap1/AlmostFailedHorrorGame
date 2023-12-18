using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager_X : MonoBehaviour
{
    BaseState_X currentState;
    public StateOne StateOne = new StateOne();
    public StateTwo StateTwo = new StateTwo();
    public StateTree StateTree = new StateTree();

    public MeshRenderer cubeRenderer;

    private void Start()
    {
        cubeRenderer = GetComponent<MeshRenderer>();

        if(currentState == null)
        {
            currentState = StateOne;
            currentState.EnterState(this);
        }
    }
    public void Update() 
    {
        currentState.UpdateState(this);

    }
    public void SwithcState(BaseState_X newState)
    {
        if (newState != currentState)
        {
            currentState.ExitState(this);
            currentState = newState;
            currentState.EnterState(this);
        }
        else { Debug.Log($"{this.name} switched to the same state"); }
    }
}
