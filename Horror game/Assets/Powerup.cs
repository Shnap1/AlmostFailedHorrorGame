using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Powerup : MonoBehaviour, IPowerUp
{
    public LootType currentLootType;


    Material currentMaterial;

    [HideInInspector] public bool TypeSet = false;
    public bool setTypeFromCode = false;
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

    public float amount;
    public float time = 1f;

    public float AddHealth;
    public float AddSpeed;
    public float AddJumpHeight;
    public float AddStamina;
    public float AddDefense;
    public float AddATK;


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

        if (setTypeFromCode == false)
        {
            SetLootType(currentLootType);
        }
        if (setTypeFromCode == true)
        {
            SetLootType(LootType.healthPU);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            DoAction(amount, time, other.gameObject);
            currentPUParticle.Play();

            Debug.Log("PowerUp Triggered");
        }
    }


    public void SetMat(Material mat)
    {
        MR.material = mat;
    }

    public void SetLootType(LootType lootType)
    {
        currentLootType = lootType;
        switch (currentLootType)
        {
            case LootType.healthPU:
                SetMat(HealthMat);
                currentPUParticle = healthPUParticle;
                amount = AddHealth;
                break;
            case LootType.speedPU:
                SetMat(SpeedMat);
                currentPUParticle = speedPUParticle;
                amount = AddSpeed;
                break;
            case LootType.jumpHeightPU:
                SetMat(JumpMat);
                currentPUParticle = jumpPUParticle;
                amount = AddJumpHeight;
                break;
            // case LootType.staminaPU:
            //     break;
            case LootType.defensePU:
                SetMat(DefenseMat);
                currentPUParticle = defensePUParticle;
                amount = AddDefense;
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
        gameObject.SetActive(false);
    }


}
