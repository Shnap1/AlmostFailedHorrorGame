using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// A class attached to a material to handle all the effects and their interraction with that material
/// </summary>
public class EffectManager : MonoBehaviour
{
    List<Effect> thisManagersEffects;
    public EffectsFactory effectsFactory;

    public Effect objectMaterial;
    public Material materialSkin;


    // .............................................................................
    // [Header("BASIC")]
    [Header("Reaction rates")]
    [HideInInspector] public float reactionRate_fast = 1f;
    [HideInInspector] public float reactionRate_slow = 10f;
    public float currentReactionRate;

    [Header("SPENDING RATE")]
    public float curResourcePerSecond; //assign other values from Effect to it and deplete
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
    public float currentResourceInside; //assign other values from Effect to it and deplete

    public float currentWaterInside;
    public float currentGasInside;
    public float currentFuelInside;
    public float currentElectricityInside;

    //...............................................................................

    void Start()
    {
        thisManagersEffects = new List<Effect>();
        effectsFactory = gameObject.AddComponent<EffectsFactory>();
    }

    //➕🔥
    public virtual void AddEffect(Effect effect)
    {
        if (effect != null && thisManagersEffects.Contains(effect))
        {
            thisManagersEffects.Add(effect);
        }
    }

    //❌🔥
    public virtual void RemoveEffect(Effect effect)
    {
        if (effect == null) return;
        if (thisManagersEffects.Contains(effect))
            thisManagersEffects.Remove(effect);
        else
            Debug.Log($"Effect {effect.name} not found - can't remove");
    }

    //🔄️🔥
    public virtual void UpdateEffects()
    {
        foreach (Effect effect in thisManagersEffects)
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
        thisManagersEffects.Clear();
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

    public void DepleteEffectsResource(float resPerSecond, float currentResInside, Effect effect)
    {
        if (currentResInside >= resPerSecond && resPerSecond > 0) currentResInside -= resPerSecond;
        else if (currentResInside <= 0) currentResInside = 0;
    }


    //Applies this material's effects to all materials in contacted EffectManagerToInfluence
    public virtual void ApplyEffects(EffectManager EffectManagerToInfluence)
    {
        // Debug.Log("ApplyEffects in EffectManager");
        foreach (EffectManager otherEffectManager in ContactedObjects)
        {
            List<Effect> otherManagersEffectList = otherEffectManager.thisManagersEffects;
            foreach (Effect otherEffect in otherManagersEffectList)
            {
                foreach (Effect thisEffect in thisManagersEffects)
                {
                    thisEffect.Interract(otherEffect, objectMaterial.matParams);
                    ////TODO: waterPerSecond can be set by the manager or a gun, but currentWaterInside must be depleted from the material 
                    DepleteResource(curResourcePerSecond, currentWaterInside);//TODO: fix a pronlem - it depletes manager's resources not materal's. Either A) Equate material's resources to manager's or B) deplete material's resources 
                    //todo actually its better to put it into Effects and manage from there, getting the depletionrates from the manager. The effects howewver should update the manager on its stats via MatParams and let Managars methods do what thy need based on them
                }
            }
        }
    }


    public IEnumerator ApplyEffects_Enumerator(EffectManager materialToInfluence, float time)
    {
        while (true)
        {
            time = ChangeReactionRate();
            ApplyEffects(materialToInfluence);
            Debug.Log("ApplyEffects_Enumerator");
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
            Debug.Log("Player contacted with EffectManager");
        }
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy contacted with EffectManager");
        }
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
            else if (reactionCoroutine != null && ContactedObjects.Count <= 0) // 2 ---- the LAST contactedGameObject is extracted so it turns off the coroutine
            {
                if (ContactedObjects.Contains(otherEM)) { ContactedObjects.Remove(otherEM); }
                StopCoroutine(reactionCoroutine);
                reactionCoroutine = null;
            }
        }
    }

}
