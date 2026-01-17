using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChangeMass : Ability
{
    private Rigidbody cachedRigidbody;

    public override void AddNecessaryComponents()
    {
        cachedRigidbody = GetComponent<Rigidbody>();

        if (cachedRigidbody == null) cachedRigidbody = gameObject.AddComponent<Rigidbody>();
    }

    public void SetMass(float newMass)
    {
        if (cachedRigidbody != null)
        {
            cachedRigidbody.mass = newMass;
            Debug.Log($"Changed mass to: {newMass} kg");
        }
    }
    public void CalculateNewMass(float scaleMultiplier)
    {
        //base * scaleMultiplier and POWER(3)
        float newMass = (float)Math.Pow(cachedRigidbody.mass * scaleMultiplier, 3); // 2 в степени 3

        SetMass(newMass);
    }

}
