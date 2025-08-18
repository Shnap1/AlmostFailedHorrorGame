using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RodFSMStateTwo : MonoBehaviour, IState
{
    object _context;
    IFSM _fsm;
    public void Initialize<T>(T context, IFSM fsm) where T : FSMContext
    {
        _context = context;
        _fsm = fsm;
    }

    public void OnEnter()
    {
        Debug.Log("RodFSMState 2");
    }
    public void CheckSwitch()
    {

    }



    public void OnUpdate()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {

        }
    }
    public void OnExit()
    {
        throw new System.NotImplementedException();
    }
}
