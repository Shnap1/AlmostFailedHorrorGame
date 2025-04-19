using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MaterialSmart_Base : MonoBehaviour
{
    public MaterialSmart_Data MSData;
    //Basic properties
    [Header("BASIC")]


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


    public enum Materials
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

    public Materials currentMaterial;




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
    public abstract void OnIce();
    public abstract void OnWater();
    public abstract void OnGas();
    public abstract void OnElectricity();


    public abstract void OnWind();
    public abstract void OnEarth();
    public abstract void OnLight();
    public abstract void OnMetal();




}
