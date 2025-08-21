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

    public Effect objectMaterial;
    public Material materialSkin;


    // .............................................................................
    // [Header("BASIC")]
    [Header("Reaction rates")]
    [HideInInspector] public float reactionRate_fast = 1f;
    [HideInInspector] public float reactionRate_slow = 10f;
    public float currentReactionRate;
    [Header("SPENDING RATE")]
    public float waterPerSecond;
    public float gasPerSecond;
    public float fuelPerSecond;
    public float electricityPerSecond;

    public List<EffectManager> ContactedObjects = new List<EffectManager>();


    [HideInInspector] public Coroutine reactionCoroutine;
    [Header("current properties")]
    public float currentHealth;
    public float currentWeight;
    public float currentSize;
    public float shatteringPoint;

    // Temperature properties
    [Header("TEMPERATURE")]
    public float currentTemp;
    //
    public float lastHealth = 0;
    public float lastWeight = 0;
    public float lastSize = 0;
    //
    //Chemichal properties
    [Header("CHEMICAL")]
    public float currentWaterInside;
    public float currentGasInside;
    public float currentFuelInside;
    public float currentElectricityInside;

    //...............................................................................



    //➕🔥
    public virtual void AddEffect(Effect effect)
    {
        if (effect != null && effects.Contains(effect))
        {
            effects.Add(effect);
        }
    }

    //❌🔥
    public virtual void RemoveEffect(Effect effect)
    {
        if (effect == null) return;
        if (effects.Contains(effect))
            effects.Remove(effect);
        else
            Debug.Log($"Effect {effect.name} not found - can't remove");
    }

    //🔄️🔥
    public virtual void UpdateEffects()
    {
        foreach (Effect effect in effects)
        {
            effect.UpdateEffect(effect.matParams); //
            // 🔥
        }
    }

    //📊🔥
    public virtual void SetCustomEffectStats(MatParams matParams, Effect effect)
    {
        effect.SetEffectStats(matParams);
    }

    //🧹🔥
    public void ClearEffects()
    {
        effects.Clear();
    }

    public virtual void ChangeMaterial(E_Effect effectEnum)
    {
        if (objectMaterial.thistype == effectEnum) return;
        objectMaterial = effectsFactory.GetEffectFromDictionary(effectEnum);

    }



    // dddddddddddddddddddddddddddddddddddddddddddddd

    public void DepleteResource(float resPerSecond, float currentResInside)
    {
        if (currentResInside >= resPerSecond && resPerSecond > 0) currentResInside -= resPerSecond;
        else if (currentResInside <= 0) currentResInside = 0;
    }

    public virtual void ApplyEffects(EffectManager materialToInfluence) //TODO - that is OLD and. Need to rewrite  rewrite
    {
        // only works for materials, NOT  NPCs or PLAYERs. Needs to be fixed. Either by 1) rewriting PLAYER/NPC reaction logic or 2) creating a new material for them, or 3) separate method here for them
        // Debug.Log("ApplyEffects in Water MS");

        foreach (EffectManager material in ContactedObjects)
        {
            // materialToInfluence.OnWater(currentWeight, waterPerSecond, currentTemp);
            // DepleteResource(waterPerSecond, currentWaterInside);

            // if (materialStates[MaterialStatesE.Freezing])
            // {
            //     materialToInfluence.OnIce(currentTemp);
            // }
            // if (materialStates[MaterialStatesE.Electrifying])
            // {
            //     materialToInfluence.OnElectricity();
            //     DepleteResource(electricityPerSecond, currentElectricityInside);
            // }
        }
    }

    public IEnumerator ApplyEffects_Enumerator(EffectManager materialToInfluence, float time)
    {
        while (true)
        {
            time = ChangeReactionRate();
            ApplyEffects(materialToInfluence);
            // Debug.Log("ApplyEffects_Enumerator");
            yield return new WaitForSeconds(time);
        }
    }

    public float ChangeReactionRate()
    {
        if (lastHealth == currentHealth || lastWeight == currentWeight || lastSize == currentSize)
        {
            lastHealth = currentHealth;
            lastWeight = currentWeight;
            lastSize = currentSize;
            return reactionRate_slow;
        }
        else
        {
            lastHealth = currentHealth;
            lastWeight = currentWeight;
            lastSize = currentSize;
            return reactionRate_fast;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        EffectManager otherEM = null;
        if (other.gameObject.GetComponent<EffectManager>() != null)
        {
            otherEM = other.gameObject.GetComponent<EffectManager>();

            currentReactionRate = reactionRate_fast;
            otherEM.currentReactionRate = otherEM.reactionRate_fast;

            if (reactionCoroutine != null) // 2 ----- coroutine is already running so just adding contactedGameObject list 
            {
                if (!ContactedObjects.Contains(otherEM)) { ContactedObjects.Add(otherEM); }
            }
            else if (reactionCoroutine == null && ContactedObjects.Count <= 0) // 1 ---- the 1st Contacted Game object turns on the coroutine
            {
                if (!ContactedObjects.Contains(otherEM)) { ContactedObjects.Add(otherEM); }
                reactionCoroutine = StartCoroutine(ApplyEffects_Enumerator(otherEM, reactionRate_fast));
            }
        }
        //TODO: rewrite. Described more specifically in ApplyEffects()
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player contacted with Water");
        }
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy contacted with Water");
        }
        // materialStates[MaterialStatesE.Burning] = true; //TODO set the material state AND/OR if condition
    }
    void OnTriggerExit(Collider other)
    {
        EffectManager otherEM = null;
        if (other.gameObject.GetComponent<EffectManager>() != null)
        {
            otherEM = other.gameObject.GetComponent<EffectManager>();

            currentReactionRate = reactionRate_fast;
            otherEM.currentReactionRate = otherEM.reactionRate_fast;

            if (reactionCoroutine != null && ContactedObjects.Count >= 1) // 1 ----- coroutine is already running and theres more then 1 contactedGameObject list 
            {
                if (ContactedObjects.Contains(otherEM)) { ContactedObjects.Remove(otherEM); }
            }
            else if (reactionCoroutine != null && ContactedObjects.Count <= 0) // 2 ---- the LAST contactedGameObject isextracted so it turns off the coroutine
            {
                if (ContactedObjects.Contains(otherEM)) { ContactedObjects.Remove(otherEM); }
                StopCoroutine(reactionCoroutine);
                reactionCoroutine = null;
            }
        }
    }

}
