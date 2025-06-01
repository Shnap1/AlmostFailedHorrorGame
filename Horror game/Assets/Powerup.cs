using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour, IPowerUp
{
    LootType currentLootType;
    Material currentMaterial;


    public bool TypeSet = false;
    public Material HealthMat;
    public Material SpeedMat;
    public Material JumpMat;
    public Material StaminaMat;
    public Material DefenseMat;

    MeshRenderer MR;



    public enum LootType
    {
        healthPU,
        speedPU,
        jumpHeightPU,
        // staminaPU,
        defensePU,
        // target
    }
    void Start()
    {
        MR = GetComponent<MeshRenderer>();

        if (TypeSet == false)
        {
            SetLootType(LootType.healthPU);
        }
        // SetLootType(LootType.healthPU);

    }

    void OnTriggerEnter(Collider other)
    {
        DoAction(0, other.gameObject);
    }

    public void SetMat(Material mat)
    {
        MR.material = HealthMat;
    }
    public void SetLootType(LootType lootType)
    {
        currentLootType = lootType;
        switch (currentLootType)
        {
            case LootType.healthPU:
                SetMat(HealthMat);
                break;
            case LootType.speedPU:
                SetMat(SpeedMat);
                break;
            case LootType.jumpHeightPU:
                SetMat(JumpMat);
                break;
            // case LootType.staminaPU:
            //     break;
            case LootType.defensePU:
                SetMat(DefenseMat);
                break;
            // case LootType.target:
            //     break;
            default:
                break;
        }
        TypeSet = true;
    }
    public void DoAction(int amount, GameObject gameObject)
    {
        switch (currentLootType)
        {
            case LootType.healthPU:
                gameObject.GetComponent<HealthCounter>().AddHealth(amount);
                break;
            case LootType.speedPU:

                break;
            case LootType.jumpHeightPU:
                break;
            // case LootType.staminaPU:
            //     break;
            case LootType.defensePU:
                break;
            // case LootType.target:
            //     break;
            default:
                break;
        }
    }

}
