using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_MS : MaterialSmart_Base
{

    public Material BurningMat;
    public Material ExtinguishedMat;

    public Material currentMat;


    void Start()
    {

        // currentMat = ObjWithMaterial.GetComponent<MeshRenderer>().material;


        currentMaterial = MaterialsE.Water;
        materialStates[MaterialStatesE.Burning] = true;
        objRenderer.material = BurningMat;

    }

    // void OnTriggerEnter(Collider other)
    // {

    //     if (other.gameObject.GetComponent<MaterialSmart_Base>() != null)
    //     {
    //         var temp = other.gameObject.GetComponent<MaterialSmart_Base>();
    //         ApplyEffects(temp);
    //     }
    //     if (other.gameObject.tag == "Player")
    //     {
    //         Debug.Log("Player contacted with Water");
    //     }

    //     if (other.gameObject.tag == "Enemy")
    //     {

    //     }
    // }

    public override void ApplyEffects(MaterialSmart_Base materialToInfluence)
    {
        Debug.Log("ApplyEffects in Fire MS");

        if (materialStates[MaterialStatesE.Burning])
        {
            materialToInfluence.OnFire(currentTemp);
            DepleteResource(fuelPerSecond, currentFuelInside);
        }
        else
        {
            // currentMat = ExtinguishedMat;
            objRenderer.material = ExtinguishedMat;
        }
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
        if (GiveWaterPerSecond > currentSize)
        {
            currentTemp = 0;

            objRenderer.material = ExtinguishedMat;
        }
        // ObjWithMaterial.GetComponent<MeshRenderer>().material = currentMat;
    }

    public override void OnWind(float windSpeed, float windTemperature)
    {
        if (windSpeed > MSData.maxStableWindSpeed)
        {
            currentTemp = windTemperature;
        }
        else
        {
            float newDegree = (currentTemp + windTemperature) / 2;
            currentTemp = newDegree;
        }
    }


}
