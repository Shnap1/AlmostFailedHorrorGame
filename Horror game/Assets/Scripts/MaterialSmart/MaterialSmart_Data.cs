using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMaterialSmartData", menuName = "ScriptableObjects/MaterialSmart_Data", order = 1)]
public class MaterialSmart_Data : ScriptableObject
{
    //Basic propertie
    [Header("BASIC")]
    public float maxHealth;
    public float maxWeight;
    public float maxSize;
    public float shatteringPoint;


    // Temperature properties
    [Header("TEMPERATURE")]
    public float frozenDegree;
    public float liqidDegree;
    public float burningDegree;
    public float vaporizeDegree;
    public float explosionDegree;



    //Chemichal properties
    [Header("CHEMICAL")]
    public float maxWaterInside;
    public float maxGasInside;
    public float maxFuelInside;

    void Start()
    {

    }

    void Update()
    {

    }
}
