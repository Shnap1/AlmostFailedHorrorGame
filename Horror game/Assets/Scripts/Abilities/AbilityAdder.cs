using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class AbilityAdder : MonoBehaviour
{
    public Ability chosenAbility;
    public Ability currentAbility;


    public Action onShot;

    public List<Ability> abilities = new();

    void Start()
    {
        // SelectAbilityUI(AbilityType.AbilityBasic);

        // abilities.Add(new Ability());
        // abilities.Add(new DeathZone());
        // abilities.Add(new JumpPad());

        currentAbility = new SpawnOnHit();
        gameObject.AddComponent(currentAbility.GetType());
        currentAbility.Initialize();
        currentAbility.Setup();

        // currentAbilty = chosenAbility;

    }


    public void SelectAbilityUI(AbilityType abilityType)
    {
        switch (abilityType)
        {
            case AbilityType.AbilityBasic:
                chosenAbility = new Ability();
                break;
            case AbilityType.GravitySphere:
                chosenAbility = new GravitySphere();
                break;
        }
    }

    public void UseAbility(RaycastHit hit)
    {
        // if (currentAbility == null) return;
        // if (hit.transform == null) return;
        Vector3 hitPoint = hit.point;

        // GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        // cube.transform.position = hit.point;
        // UnityEngine.Debug.Log("SpawnOnHit");

        // if (currentAbility.isInitialized && !currentAbility.setupEnded)
        // {
        //     currentAbility.GetHitPoint(hitPoint);
        //     currentAbility.Setup();
        // }
        // if (currentAbility.isInitialized && currentAbility.setupEnded) currentAbility.UseAbility(hit);

        currentAbility.UseAbility(hit);
    }


    public void ThrowAbilityAt(RaycastHit hit)
    {
        GameObject newGameObject = hit.transform.gameObject;
        //todo if Portals are chosen you gotta spawn 2 of them with 2 shots. So need a way to check how many times are shot and then 

        //DONT add ability if it is already there and can't be added twice
        // if (gameObject.GetComponent(currentAbilty.GetType()) == null && !currentAbilty.canBeAddedTwice) return;

        //ADD ability if it IS there and CAN be added twice 
        // if (currentAbilty.canBeAddedTwice)
        // {
        //     gameObject.AddComponent(currentAbilty.GetType());
        // }



        //ADD ability if it is NOT there and CANT be added twice
        if (currentAbility != null && !newGameObject.GetComponent(currentAbility.GetType()) && !currentAbility.canBeAddedTwice)
        {

            newGameObject.AddComponent(currentAbility.GetType());
            currentAbility.Initialize();
        }

        if (currentAbility != null && newGameObject.GetComponent(currentAbility.GetType()) != null)
        {
            var abilityComponent = newGameObject.GetComponent(currentAbility.GetType()) as Ability;
            if (abilityComponent != null)
            {
                abilityComponent.Setup();
            }
        }


        // gameObject.AddComponent(currentAbilty.GetType());
        // currentAbility = null;

    }




}
