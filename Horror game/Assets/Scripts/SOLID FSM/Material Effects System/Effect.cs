using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public E_Effect thistype;
    public MatParams matParams = new MatParams();

    public EffectManager effectManager;
    public void SetStartParams(MatParams mp) { matParams = mp; }

    ///<summary>
    ///Subscribe to the EffectManager's events
    ///</summary>
    public virtual void Subscribe(EffectManager effectManager)
    {

    }
    public virtual MatParams SetEffectStats(MatParams matParams) { return matParams; }

    ///<summary>
    ///Is updated from the EffectManager's UpdateEffects method in a coroutine. Should update inside all continously changing methods of the effect/material like CalculateInnerState(MatParams mp);
    /// No need to override.
    ///</summary>
    public virtual MatParams UpdateEffect(MatParams matParams)
    {
        CalculateInnerState(matParams);
        CheckSwitchMaterial();
        return matParams;
    }

    ///<summary>
    ///Calculates stats with formulas in methods, determining when to switch it's material to another material, add an effect to the manager destroy the object etc. It must be overriden for each material.
    ///</summary>
    public virtual MatParams CalculateInnerState(MatParams mp)
    {
        // will contain:
        // formulas for its own stat calculation
        //  CheckSwitchMaterial();
        return mp;
    }

    ///<summary>
    /// Checks if the material should switch to another materialbased on stats via if statements. It must be overriden for each material.
    ///</summary>
    public virtual void CheckSwitchMaterial()
    {
        //example
        if (matParams.oxygenIn < 0.1f) { effectManager.ChangeMaterial(E_Effect.Water); }
    }



    //All reactions with to the existing materials:

    //WATER  with its 3 states
    public virtual void OnIce() { }//🧊
    public virtual void OnWater() { }//💧
    public virtual void OnGas() { }//☁️

    //effects - NOT materials.
    public virtual void OnFire() { }//🔥
    public virtual void OnElectricity() { } //⚡
    public virtual void OnLight() { }//💡
    public virtual void OnRadiation() { }//☢️
    // decay related
    public virtual void OnRust() { }//⚙️
    public virtual void OnRot() { }//🪰
    //others
    public virtual void OnVibration() { }//💓
    public virtual void OnSound() { }//🔊
    public virtual void OnTemperature() { }//🌡️
    public virtual void OnGravity() { }//🪐
    public virtual void OnPressure() { }//🧭
    public virtual void OnHumidity() { }//💧

    public virtual void OnWind() { }//💨
    public virtual void OnSnow() { }//❄️

    //SOIL with its 10 all variations 
    public virtual void OnEarth() { }//🌱
    public virtual void OnLava() { }//🌋
    public virtual void OnMud() { }//💩
    public virtual void OnSand() { }//⌛
    public virtual void OnStone() { }//🪨
    public virtual void OnGlass() { }//🪟
    public virtual void OnDust() { }//🧹
    public virtual void OnCrystal() { }//💎
    public virtual void OnOil() { }//🛢️
    public virtual void OnGasoline() { }//⛽️
    public virtual void OnPlastic() { }//🧸
    public virtual void OnRubber() { }//🏀
    //fliages
    public virtual void OnCoal() { }//🪨⛏️
    public virtual void OnWood() { }//🪵
    public virtual void OnFoliage() { }//🍀
    public virtual void OnPlants() { }//🌿
    public virtual void OnPaper() { }//📜
    public virtual void OnTextile() { }//👚
    //METAL with its 2 states
    public virtual void OnMetal() { }//⚙️
    public virtual void OnMoltenMetal() { }//⚙️🔥

    //
    public virtual void OnAcid() { }//🧪
    public virtual void OnToxicGas() { }//💨🧪


    //universal states of all materials
    public virtual void OnPlasma() { }//🔮


}
