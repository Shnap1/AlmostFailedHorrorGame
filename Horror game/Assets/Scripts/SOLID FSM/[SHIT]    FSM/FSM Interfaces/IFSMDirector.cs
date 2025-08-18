using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// not really necessary
/// </summary>
public interface IFSMDirector
{
    public abstract IFSMBuilder FSMBuilder { get; set; }
    public abstract IFSM FSM { get; set; }
    public abstract void BuildFSM(IFSMBuilder builder, IFSM fsm); // calls the builder's GetFSM(IFSM fSM) method that inside calls the .AddStates() adding all the states to the FSM




}
