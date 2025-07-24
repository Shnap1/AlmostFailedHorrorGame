using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsFactory : MonoBehaviour
{


    Dictionary<E_Effect, Effect> effects = new Dictionary<E_Effect, Effect>();

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
    Fire,
    Water,
    Lightning,
    Flesh
}

