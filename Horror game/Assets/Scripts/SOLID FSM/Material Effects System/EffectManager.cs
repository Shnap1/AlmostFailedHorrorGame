using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //📊🔥
    public void SetCustomEffectStats(MatParams matParams, Effect effect)
    {
        effect.SetEffectStats(matParams);
    }

    //❌🔥
    public void RemoveEffect(Effect effect) //➖🔥
    {
        effects.Remove(effect);
    }

    //🧹🔥
    public void ClearEffects()
    {
        effects.Clear();
    }

    //🔄️🔥
    public void UpdateEffects()
    {
        foreach (Effect effect in effects)
        {
            // effect.UpdateEffect(); 🔥
        }
    }

}
