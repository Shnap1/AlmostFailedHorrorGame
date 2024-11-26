using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraScript : MonoBehaviour
{
    public static event Action<Transform> onCamCreated;
    void Awake()
    {
        onCamCreated?.Invoke(transform);
    }
    // Start is called before the first frame update

}
