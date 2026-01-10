using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class GravitySphere : MonoBehaviour
{
    [Header("Параметры силы притяжения")]
    [Tooltip("Базовая «константа» гравитации (G).")]
    public float gravityStrength = 20f;

    [Tooltip("Минимальное расстояние до центра сферы, чтобы блокировать бесконечный рост силы.")]
    public float minDistance = 0.5f;

    [Tooltip("Если true, сила будет ~1/r²; иначе — линейно убывающая от центра до края.")]
    public bool useInverseSquare = true;

    [Header("Фильтрация «притягиваемых» объектов")]
    [Tooltip("Будут притягиваться только объекты с этими тегами.")]
    public string[] allowedTags = { "Player", "Metal", "Wood", "Water" };

    private SphereCollider sphereCollider;

    private void Reset()
    {
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
    }

    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
    }

    private bool IsAllowed(Collider other)
    {
        // Проверяем, есть ли у объекта один из разрешённых тегов.
        for (int i = 0; i < allowedTags.Length; i++)
        {
            if (other.CompareTag(allowedTags[i]))
                return true;
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsAllowed(other)) return;

        // Ищем компонент IGravityBody — возможно, игрок или какой-то другой объект.
        var gravityBody = other.GetComponent<IGravityBody>();
        if (gravityBody != null)
        {
            gravityBody.EnterGravitySphere(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsAllowed(other)) return;

        var gravityBody = other.GetComponent<IGravityBody>();
        if (gravityBody != null)
        {
            gravityBody.ExitGravitySphere(this);
        }
    }

}