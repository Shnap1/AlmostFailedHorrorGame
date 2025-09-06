using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Effect : MonoBehaviour//Made it abstract
{
    [HideInInspector] protected E_Effect thistype;
    public MatParams matParams;

    public EffectManager effectManager;

    public E_Effect GetEffectType() { return thistype; }

    void Start()
    {
        if (matParams == null)
        {
            Debug.Log($"MatParams on is null");
            // matParams = GetComponent<MatParams>();
            // thistype = matParams.thistype;
        }
        // CheckEffectManager();
    }

    // public void CheckEffectManager()
    // {
    //     if (gameObject.TryGetComponent<EffectManager>(out EffectManager em)) effectManager = em;
    //     else if (gameObject.GetComponent<EffectManager>() == null)
    //         em = gameObject.AddComponent<EffectManager>();
    //     effectManager = em;
    // }

    ///<summary>
    ///Subscribe to the EffectManager's events
    ///</summary>
    public virtual void Subscribe(EffectManager effectManager)
    {

    }
    public virtual void SetEffectStats(MatParams newMatParams)
    {
        matParams = newMatParams;
    }

    /// <summary>
    /// Choose wich effect to activate on another Material based on this  material.
    /// Must override in each material/effect
    /// </summary>
    /// Should contain:
    //example with OnAcid()🧪 if THIS effect is an acid
    // effectToInterractWith.OnAcid();
    // InterractOtherConditions(effectToInterractWith, matParams);
    // DepleteResource(matParams.acidPerSecond, matParams.currentAcidInside);
    public abstract void Interract(Effect effectToInterractWith);

    /// <summary>
    /// Creates a reaction product like gas when water and fire interract as a game object
    /// </summary>
    public void MakeReactionProduct()
    {

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
    public void DepleteResource(float resPerSecond, float currentResInside)
    {
        // subtracts the resource from the material IF it is greater than the resource per second and 0
        if (currentResInside >= resPerSecond && resPerSecond > 0) matParams.currentResourceInside -= matParams.curResourcePerSecond;

        // if the resource is less than 0, set it to 0
        else if (currentResInside <= 0) currentResInside = 0;
    }
    ///<summary>
    ///Is updated from the EffectManager's UpdateEffects method in a coroutine. Should update inside all continously changing methods of the effect/material like CalculateInnerState(MatParams mp);
    /// No need to override.
    ///</summary>
    public virtual MatParams UpdateEffect() //🔁
    {
        CalculateInnerState();
        CheckSwitchMaterial();
        AllVFXcontrol();//TODO might have to move it to an update() method for propper vfx animationS
        CheckInnerReaction();
        return matParams;
    }


    public bool CheckInnerReaction()
    {
        bool isReacting;
        if (matParams.currentHealth == matParams.lastHealth || matParams.currentWeight == matParams.lastWeight || matParams.currentSize == matParams.lastSize)
        {
            matParams.lastHealth = matParams.currentHealth;
            matParams.lastWeight = matParams.currentWeight;
            matParams.lastSize = matParams.currentSize;

            isReacting = false;
            return isReacting;
        }
        else
        {
            matParams.lastHealth = matParams.currentHealth;
            matParams.lastWeight = matParams.currentWeight;
            matParams.lastSize = matParams.currentSize;

            isReacting = true;
            return isReacting;
        }
    }


    ///<summary>
    /// Checks if the material should switch to another materialbased on stats via if statements. It must be overriden for each material.
    ///</summary>
    public virtual void CheckSwitchMaterial()
    {
        //example
        if (matParams.currentOxygenInside < 0.1f) { effectManager.ChangeMainMaterial(E_Effect.Water); }
        //or add all the if statements into separate methods. For example:
        // ToWater();
    }


    ///<summary>
    ///Calculates stats with formulas in methods, determining when to switch it's material to another material, add an effect to the manager destroy the object etc. It must be overriden for each material.
    ///</summary>
    public virtual void CalculateInnerState()
    {
        // will contain:
        // formulas for its own stat calculation
        //  CheckSwitchMaterial();
    }

    public virtual void LiquidTemperatureCalc(MatParams mp, EffectManager EM)//in here add formulas for calculstions
    {
        if (EM.curResourcePerSecond > 0)
        {
            var tempTimesMass = EM.curResourcePerSecond * matParams.curResourceTemperature + matParams.currentWeight * matParams.currentTemp;
            matParams.currentTemp = tempTimesMass / (mp.curResourcePerSecond + mp.currentWeight);

            AddLiquidWeight(mp, EM);//todo Probably not needed here
        }
    }
    public void AddLiquidWeight(MatParams mp, EffectManager EM)
    {
        matParams.currentWeight += mp.curResourcePerSecond;
    }

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


    //Under is a list of metods that this material passively REACTS to, not actively acts.
    //All REACTIONS to all existing materials:

    //WATER  with its 3 states
    public virtual void OnIce(MatParams matParams) { }//🧊
    public virtual void OnWater(MatParams matParams) { }//💧
    public virtual void OnGas(MatParams matParams) { }//☁️

    //effects - NOT materials.
    public virtual void OnFire(MatParams OtherEffectMatParams) { }//🔥
    public virtual void OnElectricity(MatParams OtherEffectMatParams) { } //⚡
    public virtual void OnLight(MatParams OtherEffectMatParams) { }//💡
    public virtual void OnRadiation(MatParams OtherEffectMatParams) { }//☢️
    // decay related
    public virtual void OnRust(MatParams OtherEffectMatParams) { }//⚙️
    public virtual void OnRot(MatParams OtherEffectMatParams) { }//🪰
    //others
    public virtual void OnVibration(MatParams OtherEffectMatParams) { }//💓
    public virtual void OnSound(MatParams OtherEffectMatParams) { }//🔊
    public virtual void OnTemperature(MatParams OtherEffectMatParams) { }//🌡️
    public virtual void OnGravity(MatParams OtherEffectMatParams) { }//🪐
    public virtual void OnPressure(MatParams OtherEffectMatParams) { }//🧭
    public virtual void OnHumidity(MatParams OtherEffectMatParams) { }//💧

    public virtual void OnWind(MatParams OtherEffectMatParams) { }//💨
    public virtual void OnSnow(MatParams OtherEffectMatParams) { }//❄️

    //SOIL with its 10 all variations 
    public virtual void OnEarth(MatParams OtherEffectMatParams) { }//🌱
    public virtual void OnLava(MatParams OtherEffectMatParams) { }//🌋
    public virtual void OnMud(MatParams OtherEffectMatParams) { }//💩
    public virtual void OnSand(MatParams OtherEffectMatParams) { }//⌛
    public virtual void OnStone(MatParams OtherEffectMatParams) { }//🪨
    public virtual void OnGlass(MatParams OtherEffectMatParams) { }//🪟
    public virtual void OnDust(MatParams OtherEffectMatParams) { }//🧹
    public virtual void OnCrystal(MatParams OtherEffectMatParams) { }//💎
    public virtual void OnOil(MatParams OtherEffectMatParams) { }//🛢️
    public virtual void OnGasoline(MatParams OtherEffectMatParams) { }//⛽️
    public virtual void OnPlastic(MatParams OtherEffectMatParams) { }//🧸
    public virtual void OnRubber(MatParams OtherEffectMatParams) { }//🏀
    //fliages
    public virtual void OnCoal(MatParams OtherEffectMatParams) { }//🪨⛏️
    public virtual void OnWood(MatParams OtherEffectMatParams) { }//🪵
    public virtual void OnFoliage(MatParams OtherEffectMatParams) { }//🍀
    public virtual void OnPlants(MatParams OtherEffectMatParams) { }//🌿
    public virtual void OnPaper(MatParams OtherEffectMatParams) { }//📜
    public virtual void OnTextile(MatParams OtherEffectMatParams) { }//👚
    //METAL with its 2 states
    public virtual void OnMetal(MatParams OtherEffectMatParams) { }//⚙️
    public virtual void OnMoltenMetal(MatParams OtherEffectMatParams) { }//⚙️🔥

    //
    public virtual void OnAcid(MatParams OtherEffectMatParams) { }//🧪
    public virtual void OnToxicGas(MatParams OtherEffectMatParams) { }//💨🧪


    //universal states of all materials
    public virtual void OnPlasma(MatParams OtherEffectMatParams) { }//🔮
}
