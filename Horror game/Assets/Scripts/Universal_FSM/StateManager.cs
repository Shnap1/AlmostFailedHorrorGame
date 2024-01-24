using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateManager : MonoBehaviour
{
    public abstract void InitializeStates();
    public abstract void SwitchStates(BaseStateNEW baseStates);

}
