using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFSMBuilder
{
    [SerializeField] public List<IState> States { get; set; }
    [SerializeField] public IFSM FSM { get; set; }

    public void GetFSM(IFSM fSM); //Gets the FSM from the Manager Inspector
    public void AddStates(List<IState> states); //Adds specified here states to the FSM by adding them through the AddStates(IState states); method in the States list in the FSM
    public void BuildFSM();
}
