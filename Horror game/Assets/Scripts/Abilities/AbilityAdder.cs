using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityAdder : MonoBehaviour
{
    public Ability currnetChosenAbility;
    public void SelectABilityUI()
    {

    }
    public void ChooseAbility()
    {

    }
    public void AddAbility(GameObject gameObject)
    {
        Ability ability = new();
        ability.AddSelfTo(gameObject);
    }


}
