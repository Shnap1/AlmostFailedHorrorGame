using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    E_Effect thistype;
    public MatParams matParams = new MatParams();
    public void Subscribe(EffectManager effectManager)
    {

    }
    public virtual MatParams SetEffectStats(MatParams matParams) { return matParams; }
    public MatParams UpdateEffect(MatParams matParams)
    {
        return matParams;
    }

    public MatParams CalculateEffect(MatParams mp)
    {
        return mp;
    }


    protected virtual void CheckForOtherEffects() { }


    public virtual void OnFire() { }//🔥
    public virtual void OnIce() { }//🧊
    public virtual void OnWater() { }//💧
    public virtual void OnGas() { }//☁️
    public virtual void OnElectricity() { } //⚡

    public virtual void OnWind() { }//💨
    public virtual void OnEarth() { }//🌱
    public virtual void OnLight() { }//💡
    public virtual void OnMetal() { }//⚙️




}
