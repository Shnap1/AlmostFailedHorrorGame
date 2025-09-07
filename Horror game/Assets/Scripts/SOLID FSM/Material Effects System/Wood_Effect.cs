using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class Wood_Effect : Effect
{
    //todo need to add a water material since wood hassome water inside
    void Start()
    {
        // effectManager.AddEffect(E_Effect.Water);
        //todo Apparently effectManager cant add Effect classes to a list
        //todo Should stop inhereting Effect from MonoBehaviour and add a a wrapper to put into as a reference in a Monobeh so it could be easily attachable to objects.

        // thistype = E_Effect.Wood;

        //Not specifying water stats for now 
        // 1st the waterMatParams needs to be created 
        // 2nd it has to be done like this: 

        // CheckEffectManager();
        // effectManager.AddEffect(E_Effect.Water);

        // AddMatParams();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            CheckEffectManager();
            effectManager.AddEffect(E_Effect.Water);

        }
    }
    public void AddMatParams()
    {
        // Load the MatParams asset from the Resources folder
        // matParams = Resources.Load<MatParams>("WoodMatParams");
        // Debug.Log($"Resources.Load<MatParams>(WoodMatParams);");

        // If the asset is not found, you can create a new one
        if (matParams == null)
        {
            // matParams = ScriptableObject.CreateInstance<MatParams>();
            // matParams.name = "WoodMatParams";
            // AssetDatabase.CreateAsset(matParams, "Assets/Resources/WoodMatParams.asset");

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
