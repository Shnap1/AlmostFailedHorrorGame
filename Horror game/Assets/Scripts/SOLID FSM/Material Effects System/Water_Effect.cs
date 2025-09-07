using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Water_Effect : Effect
{
    void Start()
    {
        // thistype = E_Effect.Water;
        AddMatParams();
    }
    public void AddMatParams()
    {
        // Load the MatParams asset from the Resources folder
        // matParams = Resources.Load<MatParams>("WaterMatParams");
        // Debug.Log($"Resources.Load<MatParams>(WaterMatParams);");

        // If the asset is not found, you can create a new one
        if (matParams == null)
        {
            // matParams = ScriptableObject.CreateInstance<MatParams>();
            // matParams.name = "WoodMaWaterMatParamstParams";
            // AssetDatabase.CreateAsset(matParams, "Assets/Resources/WaterMatParams.asset");

        }

    }


    public override void Interract(Effect effectToInterractWith)
    {
    }

}




