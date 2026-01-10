using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class AbilityAdder : MonoBehaviour
{
    public Ability currnetChosenAbility;
    public void SelectAbilityUI(AbilityType abilityType)
    {
        switch (abilityType)
        {
            case AbilityType.Test:
                currnetChosenAbility = new Ability();
                break;
            case AbilityType.GravitySphere:
                currnetChosenAbility = new GravitySphere();
                break;

        }

    }
    public void ChooseAbility()
    {

    }
    public void ShootAbility(GameObject gameObject)
    {
        Ability ability = new();
        ability.AddSelfTo(gameObject);
    }


}
