using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_MS : MaterialSmart_Base
{
    void Start()
    {
        currentMaterial = MaterialsE.Water;

        currentHealth = MSData.maxHealth;
        currentSize = MSData.maxSize;

        currentWaterInside = MSData.maxWaterInside;
        currentGasInside = MSData.maxGasInside;
        currentFuelInside = MSData.maxFuelInside;
        reactionCoroutine = null;

        // ChangeMaterial(this, ObjWithMaterial); //TODO replace "this" with the other derived class
    }
    public Water_MS() { }
    // void OnTriggerEnter(Collider other)
    // {
    //     MaterialSmart_Base otherMS = null;
    //     if (other.gameObject.GetComponent<MaterialSmart_Base>() != null)
    //     {
    //         otherMS = other.gameObject.GetComponent<MaterialSmart_Base>();


    //         if (reactionCoroutine != null) //coroutine is already running so just adding contactedGameObject list ------------ && otherMS.GetType() == typeof(MaterialSmart_Base)
    //         {
    //             if (!ContactedObjects.Contains(otherMS)) { ContactedObjects.Add(otherMS); }
    //         }
    //         else if (reactionCoroutine == null && ContactedObjects.Count <= 0) //the first Contacted Game object turns on the coroutine -------- && otherMS.GetType() == typeof(MaterialSmart_Base)
    //         {
    //             if (!ContactedObjects.Contains(otherMS)) { ContactedObjects.Add(otherMS); }
    //             reactionCoroutine = StartCoroutine(ApplyEffects_Enumerator(otherMS, reactionRate_fast));
    //         }
    //     }



    //     //TODO: rewrite. Described more specifically in ApplyEffects()
    //     if (other.gameObject.tag == "Player")
    //     {
    //         Debug.Log("Player contacted with Water");
    //     }

    //     if (other.gameObject.tag == "Enemy")
    //     {
    //         Debug.Log("Enemy contacted with Water");
    //     }

    //     // materialStates[MaterialStatesE.Burning] = true; //TODO set the material state AND/OR if condition

    // }

    public override void InterractWithNPCs() { }
    public override void OnEarth() { }
    public override void OnElectricity()
    {
        materialStates[MaterialStatesE.Electrifying] = true;
    }
    public override void OnFire(float temperature)
    {
        //evaporate
        // Mathf.Clamp(temperature, 1, 100);
        if (temperature > 100)
        {
            currentWaterInside = 0;
        }
        else if (temperature < 0 && temperature > 100)
        {
            currentTemp = (temperature + currentTemp) / 2;
        }
    }
    public override void OnGas() { }

    public override void OnIce(float newTemperature)
    {
        if (newTemperature < MSData.frozenDegree)
        {
            currentTemp = (currentTemp + newTemperature) / 2;
            if (currentTemp < MSData.frozenDegree)
            {
                materialStates[MaterialStatesE.Freezing] = true;
            }
        }
    }
    public override void OnLight() { }

    public override void OnMetal() { }

    public override void OnWater(float WaterWeight, float transferredWaterPerSecond, float WaterTemperature)
    {
        if (transferredWaterPerSecond > 0)
        {
            var tempTimesMass = transferredWaterPerSecond * WaterTemperature + currentWeight * currentTemp;
            currentTemp = tempTimesMass / (transferredWaterPerSecond + currentWeight);

            currentWeight += transferredWaterPerSecond;
        }
    }
    public override void OnWind(float speed, float temperature) { }
    public override void InnerReaction() { }
    // IEnumerator ApplyEffects_Enumerator(MaterialSmart_Base materialToInfluence, float time)
    // {
    //     yield return new WaitForSeconds(time);
    //     ApplyEffects(materialToInfluence);
    // }
    public override void ApplyEffects(MaterialSmart_Base materialToInfluence)
    {
        //TODO: only works for materials, NOT  NPCs or PLAYERs. Needs to be fixed. Either by 1) rewriting PLAYER/NPC reaction logic or 2) creating a new material for them, or 3) separate method here for them
        // Debug.Log("ApplyEffects in Water MS");

        foreach (MaterialSmart_Base material in ContactedObjects)
        {
            materialToInfluence.OnWater(currentWeight, waterPerSecond, currentTemp);
            DepleteResource(waterPerSecond, currentWaterInside);

            if (materialStates[MaterialStatesE.Freezing])
            {
                materialToInfluence.OnIce(currentTemp);
            }
            if (materialStates[MaterialStatesE.Electrifying])
            {
                materialToInfluence.OnElectricity();
                DepleteResource(electricityPerSecond, currentElectricityInside);
            }
        }
    }
}
