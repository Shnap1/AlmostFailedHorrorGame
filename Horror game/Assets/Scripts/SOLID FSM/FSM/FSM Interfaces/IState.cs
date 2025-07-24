using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void Initialize();

    public void OnEnter();
    public void OnUpdate();
    public void OnExit();
    public void CheckSwitch();
    public void SwitchState();
}
