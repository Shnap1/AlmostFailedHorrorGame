using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodFSMM : MonoBehaviour, IFSM
{

    public List<IState> states = new List<IState>();
    public IState currentState { get; set; }
    public FSMContext context { get; set; }



    void Update()
    {
        UpdateState();
    }
    public void AddStates(IState state)
    {
        states.Add(state);
    }

    public void ChangeState(IState newState)
    {
    }

    public void FixedUpdateState()
    {
    }

    public void GetContext<T>(T context)
    {
    }

    public void InitilizeStates()
    {
        foreach (var state in states)
        {
            state.Initialize(context, this);
        }
    }

    public void UpdateState()
    {
    }
}
