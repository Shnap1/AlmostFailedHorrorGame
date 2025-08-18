using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class RodFSMBuilderStandard : MonoBehaviour, IFSMBuilder
{
    [SerializeField] public List<IState> States { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public IFSM FSM { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void AddStates(List<IState> states)
    {
        throw new System.NotImplementedException();
    }

    public void BuildFSM()
    {
        throw new System.NotImplementedException();
    }

    public void SetFSM(IFSM fSM)
    {
        throw new System.NotImplementedException();
    }

    public void SetContext(FSMContext context)
    {

    }


    void Start()
    {

    }

    void Update()
    {

    }
}
