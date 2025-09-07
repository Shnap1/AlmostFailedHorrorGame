using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsFactory : MonoBehaviour
{

    public Dictionary<E_Effect, Effect> effects;
    public List<Effect> effectsList;
    public List<GameObject> effectGameObjectsList;

    //
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

    }

    public Effect SearchInList(E_Effect effectType)
    {
        for (int i = 0; i < effectGameObjectsList.Count; i++)
        {
            if (effectGameObjectsList[i].GetComponent<Effect>().GetEffectType() == effectType)
            {
                Effect effectToReturn = effectGameObjectsList[i].GetComponent<Effect>();
                return effectToReturn;
            }
        }
        return null;
    }

    void Start()
    {
        if (effects == null)
        {
            effects = new Dictionary<E_Effect, Effect>();
        }
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
        return SearchInList(effectType);
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



