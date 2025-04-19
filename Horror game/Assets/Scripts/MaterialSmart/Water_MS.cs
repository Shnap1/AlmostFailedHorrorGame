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

        // currentHealth = MSData.maxHealth;
        // currentSize = MSData.maxSize;

        // currentWaterInside = MSData.maxWaterInside;
        // currentGasInside = MSData.maxGasInside;
        // currentFuelInside = MSData.maxFuelInside;

        // ChangeMaterial(this, ObjWithMaterial); //TODO replace "this" with the other derived class
    }

    public Water_MS()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<MaterialSmart_Base>() != null)
        {
            other.gameObject.GetComponent<MaterialSmart_Base>().OnWater();
        }

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player contacted with Water");
        }

        if (other.gameObject.tag == "Enemy")
        {

        }

        // materialStates[MaterialStatesE.Burning] = true; //TODO set the material state AND/OR if condition
        if (materialStates[MaterialStatesE.Burning])
        {
            // Do something if the material is burning
        }
    }

    public override void InterractWithNPCs()
    {

    }

    public override void OnEarth()
    {

    }

    public override void OnElectricity()
    {

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

    public override void OnIce()
    {

    }

    public override void OnLight()
    {

    }

    public override void OnMetal()
    {

    }

    public override void OnWater()
    {

    }

    public override void OnWind()
    {

    }

    public override void GetEffects()
    {

    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {

    }

}
