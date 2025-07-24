using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    E_Effect thistype;
    public void Subscribe(EffectManager effectManager)
    {

    }
    public virtual MatParams SetEffectStats(MatParams matParams) { return matParams; }
    public MatParams UpdateEffect(MatParams matParams)
    {
        return matParams;
    }

    public MatParams CalculateEffec(MatParams mp)
    {
        return mp;
    }


    protected virtual void CheckForOtherEffects() { }
}
