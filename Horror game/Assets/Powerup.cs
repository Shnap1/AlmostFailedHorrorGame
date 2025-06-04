using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Powerup : MonoBehaviour, IPowerUp
{
    LootType currentLootType;
    Material currentMaterial;

    [HideInInspector] public bool TypeSet = false;
    [Header("Materials")]
    public Material HealthMat;
    public Material SpeedMat;
    public Material JumpMat;
    // public Material StaminaMat;


    public Material DefenseMat;

    [Header("Particles")]
    public ParticleSystem currentPUParticle;

    public ParticleSystem healthPUParticle;
    public ParticleSystem speedPUParticle;
    public ParticleSystem jumpPUParticle;
    // public ParticleSystem staminaPUParticle;
    public ParticleSystem defensePUParticle;



    MeshRenderer MR;

    float AddHealth;
    float AddSpeed;
    float AddJumpHeight;
    float AddStamina;
    float AddDefense;
    float AddATK;


    float prevHealth;
    float prevSpeed;
    float prevJumpHeight;
    float prevStamina;
    float prevDefense;
    float prevATK;
    public enum LootType
    {
        healthPU,
        speedPU,
        jumpHeightPU,
        // staminaPU,
        defensePU,
        //ATK_PU,
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
        DoAction(0, 0, other.gameObject);
        currentPUParticle.Play();

        Debug.Log("PowerUp Triggered");
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
                currentPUParticle = healthPUParticle;
                break;
            case LootType.speedPU:
                SetMat(SpeedMat);
                currentPUParticle = speedPUParticle;
                break;
            case LootType.jumpHeightPU:
                SetMat(JumpMat);
                currentPUParticle = jumpPUParticle;
                break;
            // case LootType.staminaPU:
            //     break;
            case LootType.defensePU:
                SetMat(DefenseMat);
                currentPUParticle = defensePUParticle;
                break;
            // case LootType.target:
            //     break;
            default:
                break;
        }
        if (TypeSet == false)
        {
            TypeSet = true;
        }
    }
    public void DoAction(float amount, float time, GameObject gameObject)
    {
        if (currentPUParticle != null)
        {
            currentPUParticle.GetComponent<Transform>().position = GameData.instance.puPlace.transform.position;
            currentPUParticle.GetComponent<Transform>().rotation = GameData.instance.puPlace.transform.rotation;
        }

        switch (currentLootType)
        {
            case LootType.healthPU:
                gameObject.GetComponent<StatsCounter>().AddHealth(Mathf.RoundToInt(amount));
                break;
            case LootType.speedPU:
                gameObject.GetComponent<StatsCounter>().AddSpeed(amount);
                break;
            case LootType.jumpHeightPU:
                prevJumpHeight = gameObject.GetComponent<PlayerStateMachine>()._currentJumpHeight;
                gameObject.GetComponent<StatsCounter>().AddJumpHight(amount, prevJumpHeight);
                break;
            // case LootType.staminaPU:
            //     break;
            case LootType.defensePU:
                gameObject.GetComponent<StatsCounter>().AddDefence(Mathf.RoundToInt(amount));
                break;
            // case LootType.target:
            //     break;
            default:
                break;
        }
        currentPUParticle.Play();
    }


}
