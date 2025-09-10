using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class Wood_Effect : Effect
{
    //todo need to add a water material since wood hassome water inside
    public float currentHealth = 10f;
    public float maxHealth = 10f;

    public override void StartEffect()
    {
        AddMatParams();
        Debug.Log($"currentHealth={matParams.currentHealth} maxHealth={matParams.maxHealth}");
        matParams.currentHealth = matParams.maxHealth;
        // AddMatParams();

        // effectManager.AddEffect(new Water_Effect());
        //todo Cant add cuz theres no EffectManager yet in constructor
    }

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
            // matParams = ScriptableObject.CreateInstance<MatParams>();
            // matParams.name = "WoodMatParams";
            // AssetDatabase.CreateAsset(matParams, "Assets/Resources/WoodMatParams.asset");

        }
        // SwitchMat(matParams.materialVisual);
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
        //todo Uncomment these conditions when test works fine
        // if (OEMP.currentTemp_C >= matParams.burningPoint_C && matParams.currentSize >= (OEMP.currentSize * 10)) //if fire is hot enough
        // {
        //     if (matParams.currentWaterInside >= 0)
        //     {
        //         float waterToEvap = OEMP.currentSize * OEMP.currentTemp_C;//todo WRONG
        //         DepleteResource(waterToEvap, matParams.currentWaterInside);
        //         matParams.currentWaterInside -= 1;
        //     }
        // }


        // matParams.currentHealth -= 1;

        // DepleteResource(1, matParams.currentHealth);
        BurnVisuals();
        Debug.Log("OnFire Wood");
    }

    public override void SwitchMat(Material newMat)
    {
        currentVisualMaterial = newMat;
        effectManager.meshRenderer.material = newMat;
    }

    public void BurnVisuals()
    {
        Debug.Log($"currentHealth={matParams.currentHealth} maxHealth={matParams.maxHealth}");

        if (matParams.currentHealth >= (matParams.maxHealth * 0.8f))
        {
            SwitchMat(matParams.materialVisual);
            Debug.Log("Intact");
        }
        else if (matParams.currentHealth >= (matParams.maxHealth * 0.6f))
        {
            SwitchMat(matParams.materialBurntLittle);
            Debug.Log($"Burnt Min");
        }
        else if (matParams.currentHealth >= (matParams.maxHealth * 0.3f))
        {
            SwitchMat(matParams.materialBurntMedium);
            Debug.Log($"Burnt Half");
        }
        else if (matParams.currentHealth >= 0f)
        {
            SwitchMat(matParams.materialBurntMax);
            Debug.Log($"Burnt Max");
        }

    }


    public override void CalculateInnerState()
    {
        // if (currentHealth <= 0) Debug.Log("Dead");
        // Debug.Log($"currentHealth={currentHealth} maxHealth={maxHealth}");

        if (matParams.currentHealth > 0)
        {
            matParams.currentHealth -= 1;
        }

    }
}
