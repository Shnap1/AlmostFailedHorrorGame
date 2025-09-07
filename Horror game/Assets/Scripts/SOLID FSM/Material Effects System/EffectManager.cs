using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// A class attached to a material to handle all the effects and their interraction with that material
/// </summary>
public class EffectManager : MonoBehaviour
{
    public List<Effect> thisManagerEffects = new List<Effect>();
    public EffectsFactory effectsFactory;

    public bool statsChanging;
    public bool calcReactionsBetweenMatsOnStart = false;


    public Effect MainMaterial; //TODO figure out if this is needed, or call MainMaterial that determines the mesh of an object
    public E_Effect mainMaterialType;
    public Material MainMaterialSkin;

    MeshRenderer meshRenderer;



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

    public void Start()
    {

        meshRenderer = GetComponent<MeshRenderer>();


        // effectsFactory = gameObject.AddComponent<EffectsFactory>();

        ChangeMainMaterial(mainMaterialType);

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
        // if (MainMaterial != null) ChangeMainMaterial(MainMaterial.thistype);
        // effectsFactory = EffectsFactory.instance;
    }

    //➕🔥
    // public virtual void AddEffect(Effect effect)
    // {
    //     if (effect != null && thisManagerEffects.Contains(effect))
    //     {
    //         thisManagerEffects.Add(EffectsFactory.instance.GetEffectFromDictionary(effect));
    //     }
    // }
    //2nd version:
    public virtual Effect AddEffect(E_Effect e_Effect)
    {
        //Addingeffect factory if its not there and getting the effect from there from an enum
        // if (effectsFactory == null) effectsFactory = gameObject.AddComponent<EffectsFactory>();

        // effectsFactory = EffectsFactory.instance;

        // Effect effect = EffectsFactory.instance.GetEffectFromDictionary(e_Effect);

        thisManagerEffects.Add(EffectsFactory.instance.GetEffectFromList(e_Effect));

        // if (effect != null && !thisManagerEffects.Contains(effect))
        // {
        // }

        return EffectsFactory.instance.GetEffectFromList(e_Effect);
    }
    //3d version with custom Material Parameters:
    public virtual void AddEffect(E_Effect e_Effect, MatParams newMatParams)
    {
        //Addingeffect factory if its not there and getting the effect from there from an enum
        // if (effectsFactory == null) effectsFactory = gameObject.AddComponent<EffectsFactory>();

        //todo commented out double-code  from AddEffect(E_Effect e_Effect)
        // Effect effect = effectsFactory.GetEffectFromDictionary(e_Effect);
        // if (effect != null && thisManagerEffects.Contains(effect))
        // {

        //     thisManagerEffects.Add(effect);
        //     SetCustomEffectStats(newMatParams, effect);
        // }

        SetCustomEffectStats(newMatParams, AddEffect(e_Effect));

    }

    //❌🔥
    public virtual void RemoveEffect(Effect effect)
    {
        if (effect == null) return;
        if (thisManagerEffects.Contains(effect))
            thisManagerEffects.Remove(effect);

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

    public virtual void ChangeMainMaterial(E_Effect effectEnum)
    { //TODO get rid of all this if statements-shit. replace with proper get methods in Effect
      // if (MainMaterial == null) return;

        // if (MainMaterial.thistype == effectEnum) return;
        // if (MainMaterial.thistype == 0) return;


        // if (effectsFactory.GetEffectFromDictionary(effectEnum) != null)
        MainMaterial = EffectsFactory.instance.GetEffectFromList(effectEnum);
        // MainMaterial
        // gameObject.AddComponent<MainMaterial>();
        // else Debug.Log($"MainMaterial in EffectManager on {gameObject.name} is null");

        // if (MainMaterial.matParams.materialVisual != null) MainMaterialSkin = MainMaterial.matParams.materialVisual;

        //todo also take physical properties from MainMaterial and apply to this object
        // if (meshRenderer != null && MainMaterialSkin != null) meshRenderer.material = MainMaterialSkin;
        // else Debug.Log($"MeshRenderer or MainMaterialSkin in EffectManager on {gameObject.name} is null");

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
                    thisEffect.Interract(otherEffect);
                }
            }
        }

    }

    public void CalcReactionsInAndBetweenMats()
    {

        //4. Take one effect from this effect list
        foreach (Effect thisEffect in thisManagerEffects)
        {
            // 5. Apply to that material all this material's effects
            foreach (Effect otherthisEffect in thisManagerEffects)
            {

                if (thisEffect.GetEffectType() != otherthisEffect.GetEffectType())
                {
                    thisEffect.Interract(otherthisEffect);
                }
            }
            //🔁 updating inner reaction calculation of every Effect/Material after interraction
            thisEffect.UpdateEffect();
        }

    }


    public IEnumerator ApplyEffects_Enumerator(float time)
    {
        while (true)
        {
            time = ChangeReactionRate();
            ApplyEffects();
            // Debug.Log($"ApplyEffects_Enumerator called on {gameObject.name}");
            //todo comment it out because it updates material's inner state only while ApplyEffects() not always
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
            CalcReactionsInAndBetweenMats();

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

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            Debug.Log($"Update called on {gameObject.name}");
            thisManagerEffects.Add(new Wood_Effect());
        }

    }
}
