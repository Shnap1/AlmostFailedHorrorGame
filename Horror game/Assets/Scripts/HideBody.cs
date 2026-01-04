using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideBody : MonoBehaviour
{
    public List<GameObject> bodyObjects = new List<GameObject>();
    private bool hideValue;

    // Wrapper method to call HideBodyObjects with a parameter
    void OnDisable()
    {
        // CallShowBodyObjectsWithDelay(true, 0.7f);
    }
    public void CallShowBodyObjectsWithDelay(bool value, float delay)
    {
        // Debug.Log($" CallShowBodyObjectsWithDelay(value :{value}, delay: {delay})");
        hideValue = value;
        Invoke(nameof(ShowBodyObjectsWrapper), delay);
        // Store the value in a class-level variable to use it in the wrapper method
    }


    private void ShowBodyObjectsWrapper()
    {
        ShowBodyObjects(hideValue);
    }

    public void ShowBodyObjects(bool value)
    {
        // Debug.Log($" ShowBodyObjects(value :{value})");
        foreach (GameObject obj in bodyObjects)
        {
            obj.GetComponent<SkinnedMeshRenderer>().enabled = value;
        }
    }
}
