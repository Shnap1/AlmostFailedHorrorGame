using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MaterialSmart_Base : MonoBehaviour
{
    public MaterialSmart_Data MSData;
    //Basic properties
    [Header("BASIC")]

    [Header("Reaction rates")]
    public float reactionRate_fast;
    public float reactionRate_slow;

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
    public float currentDegree;

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

    // Update is called once per frame
    void Update()
    {

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

    public void ChangeMaterial(MaterialSmart_Base newMaterial, GameObject objectWithMaterial)
    {
        // Get the type of the derived class
        // Add the derived class component to the GameObject
        Component derivedComponent = objectWithMaterial.AddComponent(newMaterial.GetType());
        // Disable the existing MaterialSmart_Base component

        this.enabled = false;
    }

    public abstract void InnerReaction();
    public abstract void ApplyEffects(MaterialSmart_Base materialToInfluence);




}
