using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeRodColor_Action : MonoBehaviour, IAction
{
    public void Act<T>(T objectToRecolor)
    {
        if (objectToRecolor is GameObject gameObject) ChangeColor(objectToRecolor as GameObject);
        else Debug.LogError($"Unsupported context type: {typeof(T).Name}");
    }

    private void ChangeColor(GameObject context)
    {
        if (context.gameObject.TryGetComponent<MeshRenderer>(out MeshRenderer renderer))
            context.GetComponent<MeshRenderer>().material.color = Color.red;

    }
}
