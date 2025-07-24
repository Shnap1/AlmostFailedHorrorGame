using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFSM
{
    public void GetContext<T>(T context);
    public void AddStates(IState states); // add states to a list
    public void InitilizeStates(); // initilize states by looping through the list

    public void ChangeState(IState newState);
    public void UpdateState();
    public void FixedUpdateState();
}
