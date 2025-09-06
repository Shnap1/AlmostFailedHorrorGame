using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Fire_Effect : Effect
{
    void Start()
    {
        thistype = E_Effect.Fire;
        AddMatParams();
    }


    public void AddMatParams()
    {
        // Load the MatParams asset from the Resources folder
        matParams = Resources.Load<MatParams>("FireMatParams");
        Debug.Log($"Resources.Load<MatParams>(FireEffectMatParams);");

        // If the asset is not found, you can create a new one
        if (matParams == null)
        {
            matParams = ScriptableObject.CreateInstance<MatParams>();
            matParams.name = "FireEffectMatParams";
            AssetDatabase.CreateAsset(matParams, "Assets/Resources/FireMatParams.asset");

        }
    }



    public override void Interract(Effect effectToInterractWith)
    {
        //example with OnAcid()🧪 if THIS effect is an acid
        effectToInterractWith.OnFire(matParams);
        InterractOtherConditions(effectToInterractWith, matParams);
        DepleteResource(matParams.fuelPerSecond, matParams.currentAcidInside);
    }
}
