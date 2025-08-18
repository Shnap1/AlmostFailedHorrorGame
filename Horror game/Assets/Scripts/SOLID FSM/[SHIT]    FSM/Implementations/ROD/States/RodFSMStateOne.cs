using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodFSMStateOne : MonoBehaviour, IState
{
    public FSMContext _context { get; set; }
    public IFSM _fsm { get; set; }
    bool touchedAnotherobject = false;
    public void CheckSwitch()
    {
        if (touchedAnotherobject)
        {
            _fsm.ChangeState(new RodFSMStateTwo()); //change the context
        }
    }

    public void Initialize<T>(T context, IFSM fsm) where T : FSMContext
    {
        _context = context;
        _fsm = fsm;
    }

    public void OnEnter()
    {
        touchedAnotherobject = false;
    }

    public void OnExit()
    {
        touchedAnotherobject = false;
    }

    public void OnUpdate()
    {
        CheckSwitch();
    }
}
