using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MaterialSmart_Base : MonoBehaviour
{
    public GameObject ObjWithMaterial;
    public MeshRenderer objRenderer;



    public MaterialSmart_Data MSData;
    //Basic properties
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


    public enum MaterialsE
    {
        //Solids
        Metal,
        Wood,
        Stone,
        Glass,
        Plastic,

        //Flexible thin
        Vegetation,
        Fabric,
        Mud,

        //Liquid
        Water,
        Oil,
        Gasoline,



        //Gases
        Fire,
        Gas,
        Electric,
        Steam
    }

    public enum MaterialStatesE
    {
        Dry,
        Burning,
        Melting,
        Boiling,
        Freezing,
        Corrosive,
        Electrifying,
        Wet
    }

    public Dictionary<MaterialStatesE, bool> materialStates = new Dictionary<MaterialStatesE, bool>()
{
    { MaterialStatesE.Dry, false },
    { MaterialStatesE.Burning, false },
    { MaterialStatesE.Melting, false },
    { MaterialStatesE.Boiling, false },
    { MaterialStatesE.Freezing, false },
    { MaterialStatesE.Corrosive, false },
    { MaterialStatesE.Electrifying, false },
    { MaterialStatesE.Wet, false }
};

    public MaterialsE currentMaterial;

    [Header("contacted OBJECTS")]
    public List<MaterialSmart_Base> ContactedObjects = new List<MaterialSmart_Base>();

    // Start is called before the first frame update
    void Start()
    {
        objRenderer = ObjWithMaterial.GetComponent<MeshRenderer>();

        if (MSData == null)
        {
            Debug.LogError("Missing MS Data");
        }
        else
        {
            SetProperties();
        }
    }

    public void SetProperties()
    {
        currentHealth = MSData.maxHealth;
        currentSize = MSData.maxSize;

        currentWaterInside = MSData.maxWaterInside;
        currentGasInside = MSData.maxGasInside;
        currentFuelInside = MSData.maxFuelInside;
    }


    void OnTriggerEnter(Collider other)
    {

        MaterialSmart_Base otherMS = null;
        if (other.gameObject.GetComponent<MaterialSmart_Base>() != null)
        {
            otherMS = other.gameObject.GetComponent<MaterialSmart_Base>();

            currentReactionRate = reactionRate_fast;
            otherMS.currentReactionRate = otherMS.reactionRate_fast;

            if (reactionCoroutine != null) // 2 ----- coroutine is already running so just adding contactedGameObject list 
            {
                if (!ContactedObjects.Contains(otherMS)) { ContactedObjects.Add(otherMS); }
            }
            else if (reactionCoroutine == null && ContactedObjects.Count <= 0) // 1 ---- the 1st Contacted Game object turns on the coroutine
            {
                if (!ContactedObjects.Contains(otherMS)) { ContactedObjects.Add(otherMS); }
                reactionCoroutine = StartCoroutine(ApplyEffects_Enumerator(otherMS, reactionRate_fast));
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



        MaterialSmart_Base otherMS = null;
        if (other.gameObject.GetComponent<MaterialSmart_Base>() != null)
        {
            otherMS = other.gameObject.GetComponent<MaterialSmart_Base>();

            currentReactionRate = reactionRate_fast;
            otherMS.currentReactionRate = otherMS.reactionRate_fast;

            if (reactionCoroutine != null && ContactedObjects.Count >= 1) // 1 ----- coroutine is already running and theres more then 1 contactedGameObject list 
            {
                if (ContactedObjects.Contains(otherMS)) { ContactedObjects.Remove(otherMS); }

            }
            else if (reactionCoroutine != null && ContactedObjects.Count <= 0) // 2 ---- the LAST contactedGameObject isextracted so it turns off the coroutine
            {
                if (ContactedObjects.Contains(otherMS)) { ContactedObjects.Remove(otherMS); }
                StopCoroutine(reactionCoroutine);
                reactionCoroutine = null;
            }
        }




    }


    //Hihly interractive
    public abstract void InterractWithNPCs();
    public abstract void OnFire(float temperature);
    public abstract void OnIce(float temperature);
    public abstract void OnWater(float WaterWeight, float GiveWaterPerSecond, float WaterTemperature);
    public abstract void OnGas();


    public abstract void OnElectricity();
    public void DepleteResource(float resPerSecond, float currentResInside)
    {
        if (currentResInside >= resPerSecond && resPerSecond > 0) currentResInside -= resPerSecond;
        else if (currentResInside <= 0) currentResInside = 0;
    }

    public abstract void OnWind(float windSpeed, float windTemperature);
    public abstract void OnEarth();
    public abstract void OnLight();
    public abstract void OnMetal();

    public void ReplaceMaterialSmart(MaterialSmart_Base newMaterial, GameObject objectWithMaterial)
    {
        // Get the type of the derived class
        // Add the derived class component to the GameObject
        Component derivedComponent = objectWithMaterial.AddComponent(newMaterial.GetType());
        // Disable the existing MaterialSmart_Base component

        this.enabled = false;
    }

    public abstract void InnerReaction();
    public abstract void ApplyEffects(MaterialSmart_Base materialToInfluence);

    public IEnumerator ApplyEffects_Enumerator(MaterialSmart_Base materialToInfluence, float time)
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






}
