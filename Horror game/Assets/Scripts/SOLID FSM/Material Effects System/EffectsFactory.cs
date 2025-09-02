using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsFactory : MonoBehaviour
{

    Dictionary<E_Effect, Effect> effects;

    /// <summary>
    /// holds all the effects and to be used in EffectManager and needs to be enhanced with every new effect with the same enum in E_Effect
    /// </summary>
    public void InitializeEffectsDictionary()
    {
        //     effects.Add(E_Effect.Fire , new FireEffect());
        //     effects.Add(E_Effect.Water, new WaterEffect());
        //     effects.Add(E_Effect.Lightning, new LightningEffect());
        //     effects.Add(E_Effect.Flesh, new FleshEffect());

    }

    public Effect GetEffectFromDictionary(E_Effect effectType)
    {
        return effects[effectType];
    }

}

public enum E_Effect
{
    Ice,
    Water,
    Gas,

    Fire,
    Electricity,
    Light,
    Radiation,

    Rust,
    Rot,

    Vibration,
    Sound,
    Temperature,
    Gravity,
    Pressure,
    Humidity,

    Wind,
    Snow,

    Earth,
    Lava,
    Mud,
    Sand,
    Stone,
    Glass,
    Dust,
    Crystal,
    Oil,
    Gasoline,
    Plastic,
    Rubber,

    Coal,
    Wood,
    Foliage,
    Plants,
    Paper,
    Textile,
    Metal,
    OnMoltenMetal,

    Acid,
    ToxicGas,

    Plasma,

    Flesh
}



