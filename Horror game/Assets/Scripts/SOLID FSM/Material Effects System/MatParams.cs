using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// a universal class for holding all possible material and effect parameters. Should be passed as a patameter for methods instead of specific values so other classes will take specific values for themselves
/// </summary>
[CreateAssetMenu(fileName = "MatParams", menuName = "MatParams")]
public class MatParams : ScriptableObject
{
    [Header("SPENDING RATE")] //todo SPENDING RATEs could be removed and only assign in effectmanager and a gun
    public float curResourcePerSecond;
    public float waterPerSecond;
    public float gasPerSecond;
    public float fuelPerSecond;
    public float electricityPerSecond;

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


    public float temperatureIn;
    public float temperatureOut;

    public float waterIn;
    public float waterOut;

    public float pressureIn;
    public float pressureOut;

    public float oxygenIn;
    public float oxygenOut;


}
