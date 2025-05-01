using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainCameraScript : MonoBehaviour
{
    public static event Action<Transform> onCamCreated;
    void Awake()
    {
        // if (this.isActiveAndEnabled)
        // {
        // }
        onCamCreated?.Invoke(this.transform);
        // Debug.Log("CAM TEST - MainCameraScript onCamCreated on " + this.name);
    }
    void Start()
    {
        onCamCreated?.Invoke(this.transform);
        // Debug.Log("CAM TEST - MainCameraScript onCamCreated on " + this.name);
    }

    // Start is called before the first frame update

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            onCamCreated?.Invoke(this.transform);
            Debug.Log("CAM TEST - MainCameraScript onCamCreated on " + this.name);
        }
    }

}
