using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// A class attached to a material to handle all the effects and their interraction with that material
/// </summary>
public class EffectManager : MonoBehaviour
{
    List<Effect> thisManagerEffects;
    public EffectsFactory effectsFactory;

    public bool statsChanging;
    public bool calcReactionsBetweenMatsOnStart = false;


    public Effect objectMaterial;
    public Material materialSkin;



    // .............................................................................
    // [Header("BASIC")]
    [Header("Reaction rates")]
    // [HideInInspector]
    public float reactionRate_fast = 1f;

    // [HideInInspector]
    public float reactionRate_slow = 10f;

    public float currentReactionRate;

    [Header("SPENDING RATE")]
    public float curResourcePerSecond; //assign other values from Effect to it and deplete
    public float waterPerSecond;
    public float gasPerSecond;
    public float fuelPerSecond;
    public float electricityPerSecond;

    public List<EffectManager> ContactedObjects = new List<EffectManager>();


    [HideInInspector] public Coroutine reactionCoroutine;
    [HideInInspector] public Coroutine reactionsBetweenMatsCoroutine;
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
        thisManagerEffects = new List<Effect>();
        effectsFactory = gameObject.AddComponent<EffectsFactory>();

        if (calcReactionsBetweenMatsOnStart)
        {

            if (reactionsBetweenMatsCoroutine == null)
            {
                reactionsBetweenMatsCoroutine = StartCoroutine(CalcInMaterialReactions_Enumerator(ChangeReactionRate()));
            }
            else
            {
                StopCoroutine(reactionsBetweenMatsCoroutine);

                reactionsBetweenMatsCoroutine = StartCoroutine(CalcInMaterialReactions_Enumerator(ChangeReactionRate()));
            }
        }
    }

    //➕🔥
    public virtual void AddEffect(Effect effect)
    {
        if (effect != null && thisManagerEffects.Contains(effect))
        {
            thisManagerEffects.Add(effect);
        }
    }

    //❌🔥
    public virtual void RemoveEffect(Effect effect)
    {
        if (effect == null) return;
        if (thisManagerEffects.Contains(effect))
            thisManagerEffects.Remove(effect);
        else
            Debug.Log($"Effect {effect.name} not found - can't remove");
    }

    //🔄️🔥
    public virtual void UpdateEffects()
    {
        foreach (Effect effect in thisManagerEffects)
        {
            effect.UpdateEffect();//usually calculates inner reactions
            CalcReactionsBetweenMats();

            //
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
        thisManagerEffects.Clear();
    }

    public virtual void ChangeMaterial(E_Effect effectEnum)
    {
        if (objectMaterial.thistype == effectEnum) return;
        objectMaterial = effectsFactory.GetEffectFromDictionary(effectEnum);

    }



    // dddddddddddddddddddddddddddddddddddddddddddddd

    // public void DepleteResource(float resPerSecond, float currentResInside)
    // {
    //     if (currentResInside >= resPerSecond && resPerSecond > 0) currentResInside -= resPerSecond;
    //     else if (currentResInside <= 0) currentResInside = 0;
    // }


    //Applies this material's effects to all materials in contacted objects
    public virtual void ApplyEffects()
    {
        // Debug.Log("ApplyEffects in EffectManager");

        // 1. Take an EffectManager from a contacted object. (And loop through all of them)
        foreach (EffectManager otherEffectManager in ContactedObjects)
        {
            List<Effect> otherManagerEffectList = otherEffectManager.thisManagerEffects;

            // 2.From this contacted object's EffectManager take each material/effect
            foreach (Effect otherEffect in otherManagerEffectList)
            {
                //3. Apply to another material each this material's effects. (And loop through all of other object's materials)
                foreach (Effect thisEffect in thisManagerEffects)
                {
                    thisEffect.Interract(otherEffect, thisEffect.matParams);
                }
            }
        }

    }

    public void CalcReactionsBetweenMats()
    {
        //4. Take one effect from this effect list
        foreach (Effect thisEffect in thisManagerEffects)
        {
            // 5. Apply to that material all this material's effects
            foreach (Effect otherthisEffect in thisManagerEffects)
                if (thisEffect.name != thisEffect.name)
                {
                    thisEffect.Interract(otherthisEffect, thisEffect.matParams);
                }
        }

    }


    public IEnumerator ApplyEffects_Enumerator(float time)
    {
        while (true)
        {
            time = ChangeReactionRate();
            ApplyEffects();
            Debug.Log("ApplyEffects_Enumerator");
            // foreach (Effect thisEffect in thisManagerEffects)
            // {
            //     thisEffect.UpdateEffect();
            // }
            yield return new WaitForSeconds(time);
        }
    }

    public IEnumerator CalcInMaterialReactions_Enumerator(float time)
    {
        while (true)
        {
            time = ChangeReactionRate();
            CalcReactionsBetweenMats();

            foreach (Effect thisEffect in thisManagerEffects)
            {
                thisEffect.UpdateEffect();
            }
            yield return new WaitForSeconds(time);
        }
    }

    public float ChangeReactionRate()
    {
        if (lastHealth == currentHealth && lastWeight == currentWeight && lastSize == currentSize)
        {
            lastHealth = currentHealth;
            lastWeight = currentWeight;
            lastSize = currentSize;

            statsChanging = false;
            return reactionRate_slow;
        }
        else
        {
            lastHealth = currentHealth;
            lastWeight = currentWeight;
            lastSize = currentSize;

            statsChanging = true;
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
                reactionCoroutine = StartCoroutine(ApplyEffects_Enumerator(reactionRate_fast));
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

            if (reactionCoroutine != null && ContactedObjects.Count >= 1) // 1 ----- coroutine is running and theres more then 1 contactedGameObject list 
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

    void OnDisable()
    {
        if (reactionCoroutine != null)
        {
            StopCoroutine(reactionCoroutine);
            reactionCoroutine = null;
        }
        if (reactionsBetweenMatsCoroutine != null)
        {
            StopCoroutine(reactionsBetweenMatsCoroutine);
            reactionsBetweenMatsCoroutine = null;
        }

    }

}
