using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityLimiter : MonoBehaviour
{
    void OnEnable()
    {

    }
    void OnDisable()
    {

    }

    [Header("Abilities AFFECTED")]
    public bool canSpawnOnHit = true;
    public bool canInvisibility = true;
    public bool canChangeScale = true;
    public bool canChangeMass = true;
    public bool canJumpPad = true;
    public bool canGravitySphere = true;
    public bool canCollisionLimiter = true;
    public bool canDeathZone = true;

}
