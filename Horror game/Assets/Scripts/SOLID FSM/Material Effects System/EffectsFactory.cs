using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsFactory : MonoBehaviour
{

    // public Dictionary<E_Effect, Effect> effects;
    public List<Effect> effectsList = new List<Effect>();
    // public List<GameObject> effectGameObjectsList;
    [HideInInspector] public static EffectsFactory instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        InitializeEffectsList();
        AssignSO();
    }
    public void InitializeEffectsList()
    {
        Debug.Log("InitializeEffectsList");
        effectsList.Add(new Water_Effect());
        effectsList.Add(new Fire_Effect());
        effectsList.Add(new Wood_Effect());
    }
    public void AssignSO()
    {
        foreach (Effect effect in effectsList)
        {
            effect.AddMatParams();
        }
    }
    public Effect SearchInList(E_Effect effectType)
    {
        Debug.Log("SearchInList");
        Effect effectToReturn = null;// = effectsList[0];//todo delete
        for (int i = 0; i < effectsList.Count; i++)
        {
            Debug.Log($"Effect {i} {effectsList[i].matParams.thistype}");
            if (effectsList[i].matParams == null)
            {
                Debug.Log($"Effect {i} has no matParams");
            }
            if (effectsList[i].matParams.thistype == effectType)
            {
                effectToReturn = effectsList[i];
                Debug.Log($"Effect {effectType} found");
                // return effectToReturn;
            }
        }
        return effectToReturn;

    }

    public Effect PrimitiveShitSearch(E_Effect effectType)
    {
        Effect effectToReturn = null;//todo delete
        switch (effectType)
        {
            case E_Effect.Water:
                effectToReturn = new Water_Effect();
                break;
            case E_Effect.Fire:
                effectToReturn = new Fire_Effect();
                break;
            case E_Effect.Wood:
                effectToReturn = new Wood_Effect();
                break;
        }
        return effectToReturn;
    }

    void Start()
    {
        // if (effects == null)
        // {
        //     effects = new Dictionary<E_Effect, Effect>();
        // }

        // InitializeEffectsDictionary();
        // InitializeEffectsList();
    }

    public Effect GetEffectFromList(E_Effect effectType)
    {
        // if (effects[effectType] != null)
        //     return effects[effectType];
        // else if (effects[effectType] == null)
        //     Debug.Log($"Effect {effectType} not found");
        // return null;
        // return SearchInList(effectType);
        // return PrimitiveShitSearch(effectType);
        return PrimitiveShitSearch(effectType);
    }

}

public enum E_Effect
{
    Ice,
    Water,
    Gas,

    Fire,
    Electricity,
    Light,
    Radiation,

    Rust,
    Rot,

    Vibration,
    Sound,
    Temperature,
    Gravity,
    Pressure,
    Humidity,

    Wind,
    Snow,

    Earth,
    Lava,
    Mud,
    Sand,
    Stone,
    Glass,
    Dust,
    Crystal,
    Oil,
    Gasoline,
    Plastic,
    Rubber,

    Coal,
    Wood,
    Foliage,
    Plants,
    Paper,
    Textile,
    Metal,
    OnMoltenMetal,

    Acid,
    ToxicGas,

    Plasma,

    Flesh
}



