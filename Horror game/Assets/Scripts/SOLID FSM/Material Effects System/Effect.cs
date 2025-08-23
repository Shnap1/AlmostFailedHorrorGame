using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public E_Effect thistype;
    public MatParams matParams;

    public EffectManager effectManager;



    public void SetStartParams(MatParams mp) { matParams = mp; }

    ///<summary>
    ///Subscribe to the EffectManager's events
    ///</summary>
    public virtual void Subscribe(EffectManager effectManager)
    {

    }
    public virtual MatParams SetEffectStats(MatParams matParams) { return matParams; }

    /// <summary>
    /// Choose wich effect to activate on another Material based on thisn material.
    /// Must override in each material/effect
    /// </summary>
    public virtual void Interract(Effect effectToInterractWith, MatParams matParams)
    {
        //example with OnAcid() if THIS effect is an acid
        effectToInterractWith.OnAcid();
        InterractOtherConditions(effectToInterractWith, matParams);
    }

    /// <summary>
    /// Contains special conditions for interraction usually with player, characters, NPCs etc
    /// Can be overriden in each material/effect
    /// </summary>
    public virtual void InterractOtherConditions(Effect effectToInterractWith, MatParams matParams)
    {
        //example
        // if (effectToInterractWith.gameObject.tag == "Player")
        // Debug.Log("Player touched with acid");
    }

    ///<summary>
    ///Is updated from the EffectManager's UpdateEffects method in a coroutine. Should update inside all continously changing methods of the effect/material like CalculateInnerState(MatParams mp);
    /// No need to override.
    ///</summary>
    public virtual MatParams UpdateEffect(MatParams matParams)
    {
        CalculateInnerState(matParams);
        CheckSwitchMaterial();
        AllVFXcontrol();//TODO might have to move it to an update() method for propper vfx animationS
        return matParams;
    }


    ///<summary>
    /// Checks if the material should switch to another materialbased on stats via if statements. It must be overriden for each material.
    ///</summary>
    public virtual void CheckSwitchMaterial()
    {
        //example
        if (matParams.oxygenIn < 0.1f) { effectManager.ChangeMaterial(E_Effect.Water); }
        //or add all the if statements into separate methods. For example:
        // ToWater();
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

    public virtual void TemperatureCalc() { }//in here add formulas for calculstions
    public virtual void PressureCalc() { }
    public virtual void MoistureCalc() { }
    public virtual void StrengthCalc() { }
    public virtual void WeightCalc() { }
    public virtual void DecayCalc() { }
    public virtual void BioDecayCalc() { }
    public virtual void ElasticityCalc() { }


    // SOUND + VISUAL EFFECTS
    public virtual void AllVFXcontrol() { } //contains the following 3 methods
    public virtual void SwitchMat() { }
    public virtual void SwitchVFX() { }
    public virtual void SwitchSFX() { }

    //playing as a reaction to collision with other objects
    public virtual void PlayVFX() { }
    public virtual void PlaySFX() { }


    //All REACTIONS to all existing materials:

    //WATER  with its 3 states
    public virtual void OnIce(MatParams matParams) { }//🧊
    public virtual void OnWater(MatParams matParams) { }//💧
    public virtual void OnGas(MatParams matParams) { }//☁️

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
