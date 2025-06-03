using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    Light playerFlashlight;
    public bool FlashlightOn = true;
    void Start()
    {
        playerFlashlight = gameObject.GetComponent<Light>();
    }

    public void ToggleFlashlight()
    {
        if (FlashlightOn)
        {
            playerFlashlight.enabled = false;
            FlashlightOn = false;
        }
        else
        {
            playerFlashlight.enabled = true;
            FlashlightOn = true;
        }
    }

    public void FlashlightSwitch(bool FlashlightOn)
    {
        if (FlashlightOn == true)
        {
            playerFlashlight.enabled = true;
        }
        else if (FlashlightOn == false)
        {
            playerFlashlight.enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) ToggleFlashlight();
    }

}
