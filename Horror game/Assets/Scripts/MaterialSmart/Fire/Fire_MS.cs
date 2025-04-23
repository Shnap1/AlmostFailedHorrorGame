using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_MS : MaterialSmart_Base
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
    }

    public override void ApplyEffects(MaterialSmart_Base materialToInfluence)
    {
        materialToInfluence.OnFire(currentDegree);
    }

    public override void InnerReaction()
    {

    }

    public override void InterractWithNPCs()
    {
        throw new System.NotImplementedException();
    }

    public override void OnEarth()
    {
        throw new System.NotImplementedException();
    }

    public override void OnElectricity()
    {
        throw new System.NotImplementedException();
    }

    public override void OnFire(float temperature)
    {
        throw new System.NotImplementedException();
    }

    public override void OnGas()
    {
        throw new System.NotImplementedException();
    }

    public override void OnIce(float temperature)
    {
        throw new System.NotImplementedException();
    }

    public override void OnLight()
    {
        throw new System.NotImplementedException();
    }

    public override void OnMetal()
    {
        throw new System.NotImplementedException();
    }

    public override void OnWater(float WaterWeight, float GiveWaterPerSecond, float WaterTemperature)
    {
        if (WaterWeight > currentSize)
        {
            currentDegree = 0;

        }
    }

    public override void OnWind(float windSpeed, float windTemperature)
    {
        if (windSpeed > MSData.maxStableWindSpeed)
        {
            currentDegree = windTemperature;
        }
        else
        {
            float newDegree = (currentDegree + windTemperature) / 2;
            currentDegree = newDegree;
        }
    }

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {

    }
}
