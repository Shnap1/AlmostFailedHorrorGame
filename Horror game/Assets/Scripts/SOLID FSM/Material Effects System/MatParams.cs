using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// a universal class for holding all possible material and effect parameters. Should be passed as a patameter for methods instead of specific values so other classes will take specific values for themselves
/// </summary>
[CreateAssetMenu(fileName = "MatParams", menuName = "MatParams")]
public class MatParams : ScriptableObject
{
    [Header("REACTION PROPERTIES")]
    public float boilingPoint;
    public float meltingPoint;
    public float freezingPoint;
    public float shatteringPoint;
    public bool isShattered;

    [Header("STABLE PROPERTIES")]
    public float rigidity;
    public float viscosity;
    public float hardness;
    public float density;
    public float surfaceRoughness;

    public float electricConductivity;
    public float heatConductivity;
    public float heatCapacity;
    public float specificHeatCapacity;

    [Header("Absorbtion Rates")]
    public float liquidAbsorbtionRate;
    public float gasAbsorbtionRate;
    public float fuelAbsorbtionRate;
    public float electricityAbsorbtionRate;


    [Header("SPENDING RATE")] //todo SPENDING RATEs could be removed and only assign in effectmanager and a gun
    public float curResourcePerSecond;

    public float waterPerSecond;
    public float gasPerSecond;
    public float fuelPerSecond;
    public float electricityPerSecond;
    public float windPerSecond;
    public float acidPerSecond;

    [Header("CHANGING PROPERTIES")]
    public float currentResourceInside; //assign other values from Effect to it and deplete
    [HideInInspector] public float lastResourceInside;

    public float curResourceTemperature;
    [HideInInspector] public float lastResourceTemperature;

    //

    public float currentHealth;
    [HideInInspector] public float lastHealth;

    public float currentWeight;
    [HideInInspector] public float lastWeight;

    public float currentSize;
    [HideInInspector] public float lastSize;

    public float currentTemp;
    [HideInInspector] public float lastTemp;

    public float currentPressure;
    [HideInInspector] public float lastPressure;


    //Other Materias

    public float currentFireInside;
    [HideInInspector] public float lastFireInside;

    public float currentWaterInside;
    [HideInInspector] public float lastWaterInside;

    public float currentIceInside;
    [HideInInspector] public float lastIceInside;

    public float currentMetalInside;
    [HideInInspector] public float lastMetalInside;

    public float currentLightInside;
    [HideInInspector] public float lastLightInside;

    public float currentMudInside;
    [HideInInspector] public float lastMudInside;

    public float currentRotInside;
    [HideInInspector] public float lastRotInside;

    public float currentGasInside;
    [HideInInspector] public float lastGasInside;

    public float currentOxygenInside;
    [HideInInspector] public float lastOxygenInside;

    public float currentFuelInside;
    [HideInInspector] public float lastFuelInside;


    public float currentElectricityInside;
    [HideInInspector] public float lastElectricityInside;


    public float currentWindInside;
    [HideInInspector] public float lastWindInside;

    public float currentAcidInside;
    [HideInInspector] public float lastAcidInside;

    public Material material;
    public Material[] materialsVariations;


}
