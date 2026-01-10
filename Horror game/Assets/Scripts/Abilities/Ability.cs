using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [SerializeField] public bool dontAddTwice = true;

    public virtual void AddSelfTo(GameObject obj)
    {
        if (dontAddTwice && obj.GetComponent<Ability>()) return;
        obj.AddComponent<Ability>();
        Initialize();
    }
    public virtual void Initialize()
    {
        string className = GetType().Name;
        Debug.Log($"Ability {className} Initialize");
    }
    public virtual void AddNecessaryComponents() { }
    public virtual void CheckOtherAbilities() { }
    public virtual void UseAbility() { }
    public virtual void StopAbility() { }
}

public enum AbilityType
{
    Test,
    GravitySphere,
    Pinata,
    GunProjectile,
}

