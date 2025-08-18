using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public E_Effect thistype;
    public MatParams matParams = new MatParams();

    public EffectManager effectManager;
    public void SetStartParams(MatParams mp) { matParams = mp; }

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
    public virtual void CheckSwitchMaterial()
    {
        //example
        if (matParams.oxygenIn < 0.1f) { effectManager.ChangeMaterial(E_Effect.Water); }
    }


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
