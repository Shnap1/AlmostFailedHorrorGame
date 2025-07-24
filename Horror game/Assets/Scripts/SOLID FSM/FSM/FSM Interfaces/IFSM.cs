using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFSM
{
    /// <summary>
    /// 
    /// </summary>

    // public List<IState> states { get; set; }
    // public IState currentState { get; set; }
    // public T context { get; set; }

    public void GetContext<T>(T context);
    public void AddStates(IState states); // add states to a list
    public void InitilizeStates(); // initilize states by looping through the list

    public void ChangeState(IState newState);
    public void UpdateState();
    public void FixedUpdateState();
}
