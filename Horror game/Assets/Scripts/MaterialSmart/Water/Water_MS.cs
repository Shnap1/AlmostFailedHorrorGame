using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_MS : MaterialSmart_Base
{
    public GameObject ObjWithMaterial;

    void Start()
    {
        currentMaterial = MaterialsE.Water;

        currentHealth = MSData.maxHealth;
        currentSize = MSData.maxSize;

        currentWaterInside = MSData.maxWaterInside;
        currentGasInside = MSData.maxGasInside;
        currentFuelInside = MSData.maxFuelInside;

        // ChangeMaterial(this, ObjWithMaterial); //TODO replace "this" with the other derived class
    }

    public Water_MS()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<MaterialSmart_Base>() != null)
        {
            var temp = other.gameObject.GetComponent<MaterialSmart_Base>();
            ApplyEffects(temp);
        }

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player contacted with Water");
        }

        if (other.gameObject.tag == "Enemy")
        {

        }

        // materialStates[MaterialStatesE.Burning] = true; //TODO set the material state AND/OR if condition

    }

    public override void InterractWithNPCs()
    {
    }

    public override void OnEarth()
    {
    }

    public override void OnElectricity()
    {
        materialStates[MaterialStatesE.Electrifying] = true;
    }

    public override void OnFire(float temperature)
    {
        //evaporate
        Mathf.Clamp(temperature, 1, 100);
        if (temperature > 100)
        {
            currentWaterInside = 0;
        }
    }

    public override void OnGas()
    {
    }

    public override void OnIce(float temperature)
    {
        if (temperature < MSData.frozenDegree)
        {
            materialStates[MaterialStatesE.Freezing] = true;
        }
    }

    public override void OnLight()
    {

    }

    public override void OnMetal()
    {

    }

    public override void OnWater(float WaterWeight, float WaterTemperature)
    {
        if (WaterWeight > 0)
        {
            var tempXmass = (WaterWeight * WaterTemperature + currentWeight * currentDegree);
            currentDegree = tempXmass / (WaterWeight + currentWeight);

            currentWeight += WaterWeight;
        }

    }

    public override void OnWind(float speed, float temperature)
    {

    }

    public override void GetEffects()
    {

    }


    // Update is called once per frame
    void Update()
    {

    }

    public override void ApplyEffects(MaterialSmart_Base materialToInfluence)
    {
        if (materialStates[MaterialStatesE.Freezing])
        {
            materialToInfluence.OnIce(currentDegree);
        }
        if (materialStates[MaterialStatesE.Electrifying])
        {
            materialToInfluence.OnElectricity();
        }
        materialToInfluence.OnWater(currentWeight, currentDegree);

    }
}
