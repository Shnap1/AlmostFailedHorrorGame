using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_MS : MaterialSmart_Base
{
    void Start()
    {
        currentMaterial = Materials.Water;

        // currentHealth = MSData.maxHealth;
        // currentSize = MSData.maxSize;

        // currentWaterInside = MSData.maxWaterInside;
        // currentGasInside = MSData.maxGasInside;
        // currentFuelInside = MSData.maxFuelInside;
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

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {

    }
}
