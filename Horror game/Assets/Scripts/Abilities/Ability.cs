using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public bool setupEnded = false;
    public bool isInitialized = false;

    public bool ShotOne = false;
    public bool ShotTwo = false;

    public int shots = 0;

    Vector3 hitPoint;
    public bool canBeAddedTwice = false;


    public virtual void Initialize()
    {
        string className = GetType().Name;
        isInitialized = true;
        Debug.Log($"Ability {className} Initialize");
        AddNecessaryComponents();
        CheckOtherAbilities();
        Setup();
    }
    public void GetHitPoint(Vector3 hitPoint)
    {
        ++shots;
    }

    public virtual void Setup()
    {
        // setupEnded = true;
        ++shots;
        if (shots == 1) ShotOne = true;
        if (shots >= 2)
        {
            ShotTwo = true;
            setupEnded = true;
        }
    }


    public virtual void AddNecessaryComponents() { }
    public virtual void CheckOtherAbilities() { }
    public virtual void UseAbility() { }
    public virtual void StopAbility() { }
}

public enum AbilityType
{
    AbilityBasic,
    GravitySphere,
    Pinata,
    GunProjectile,
}

