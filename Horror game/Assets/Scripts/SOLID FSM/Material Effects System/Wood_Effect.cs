using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class Wood_Effect : Effect
{
    //todo need to add a water material since wood hassome water inside

    // public override void StartEffect()
    // {
    // AddMatParams();

    // effectManager.AddEffect(new Water_Effect());
    //     //todo Cant add cuz theres no EffectManager yet in constructor
    // }

    public override void SecondAction()
    {
        // effectManager.AddEffect(new Water_Effect());
        // Debug.Log("SecondAction in Wood_Effect");
    }
    public override void AddMatParams()
    {
        // Load the MatParams asset from the Resources folder
        matParams = Resources.Load<MatParams>("WoodMatParams");
        Debug.Log($"Resources.Load<MatParams>(WoodMatParams);");

        // If the asset is not found, you can create a new one
        if (matParams == null)
        {
            matParams = ScriptableObject.CreateInstance<MatParams>();
            matParams.name = "WoodMatParams";
            AssetDatabase.CreateAsset(matParams, "Assets/Resources/WoodMatParams.asset");

        }
    }
    public override void Interract(Effect effectToInterractWith)
    {
        effectToInterractWith.OnWood(matParams);

        //Wood doesnt is not liquid so no DepleteResource
        // InterractOtherConditions(effectToInterractWith, matParams);
        // DepleteResource(matParams.fuelPerSecond, matParams.currentAcidInside);
    }

    public override void OnFire(MatParams OtherEffectMatParams)
    {
        MatParams OEMP = OtherEffectMatParams;
        if (OEMP.currentTemp >= matParams.burningPoint_C && matParams.currentSize < (OEMP.currentSize * 10)) //if fire is hot enough
        {
            if (matParams.currentWaterInside >= 0)
            {
                float waterToEvap = OEMP.currentSize * OEMP.currentTemp;//todo WRONG
                DepleteResource(waterToEvap, matParams.currentWaterInside);
                matParams.currentWaterInside -= 1;
            }
        }


        matParams.currentHealth -= OtherEffectMatParams.curResourceTemperature;
    }
    public override void CalculateInnerState()
    {
        if (matParams.currentHealth <= 0) Debug.Log("Dead");
    }
}
