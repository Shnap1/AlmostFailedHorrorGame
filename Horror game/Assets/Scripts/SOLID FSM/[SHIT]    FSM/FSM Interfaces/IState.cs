using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IState
{
    // public T context { get; set; }
    //public IFSM fsm { get; set; }

    public void Initialize<T>(T context, IFSM fsm) where T : FSMContext;

    public void OnEnter();
    public void OnUpdate();
    public void OnExit();
    public void CheckSwitch();
    // public void SwitchState();
}
