using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideBody : MonoBehaviour
{
    public List<GameObject> bodyObjects = new List<GameObject>();

    // Wrapper method to call HideBodyObjects with a parameter
    void OnDisable()
    {
        CallShowBodyObjectsWithDelay(true, 0f);
    }
    public void CallShowBodyObjectsWithDelay(bool value, float delay)
    {
        Invoke(nameof(ShowBodyObjectsWrapper), delay);
        // Store the value in a class-level variable to use it in the wrapper method
        hideValue = value;
    }

    private bool hideValue;

    private void ShowBodyObjectsWrapper()
    {
        ShowBodyObjects(hideValue);
    }

    public void ShowBodyObjects(bool value)
    {
        foreach (GameObject obj in bodyObjects)
        {
            obj.GetComponent<SkinnedMeshRenderer>().enabled = value;
        }
    }
}
