using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Takes Istates list from the Inspector and adds them to an IFSM from the Inspector or instantiates it if it doesn't exist
/// </summary>   
public interface IFSMBuilder
{
    [SerializeField] public List<IState> States { get; set; }
    [SerializeField] public IFSM FSM { get; set; }

    public void SetFSM(IFSM fSM); //Gets the FSM from the Manager Inspector

    public void SetContext(IFSMContext context)
    {

    }
    public void AddStates(List<IState> states); //Adds specified here states to the FSM by adding them through the IFSM.AddStates(IState states); method in the States list in the FSM
    public void BuildFSM();
}
