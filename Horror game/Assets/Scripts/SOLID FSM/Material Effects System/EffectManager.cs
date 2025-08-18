using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// A class attached to a material to handle all the effects and their interraction with that material
/// </summary>
public class EffectManager : MonoBehaviour
{
    List<Effect> effects = new List<Effect>();
    public EffectsFactory effectsFactory = new EffectsFactory();

    //➕🔥
    public void AddEffect(Effect effect)
    {
        if (effect != null && effects.Contains(effect))
        {
            effects.Add(effect);
        }
        ;
    }

    //❌🔥
    public void RemoveEffect(Effect effect) //➖🔥
    {
        if (effect == null) return;
        if (effects.Contains(effect))
            effects.Remove(effect);
        else
            Debug.Log($"Effect {effect.name} not found - can't remove");
    }

    //🔄️🔥
    public void UpdateEffects()
    {
        foreach (Effect effect in effects)
        {
            effect.UpdateEffect(effect.matParams); //
            // 🔥
        }
    }

    //📊🔥
    public void SetCustomEffectStats(MatParams matParams, Effect effect)
    {
        effect.SetEffectStats(matParams);
    }

    //🧹🔥
    public void ClearEffects()
    {
        effects.Clear();
    }
}
