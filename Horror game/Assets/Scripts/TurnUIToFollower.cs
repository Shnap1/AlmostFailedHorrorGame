using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnUIToFollower : MonoBehaviour
{
    public Transform cam;
    // public TargetUI targetUI;
    void Awake()
    {
        // SetCam();
    }
    public void SetCam()
    {
        // cam = GameData.instance.cam;

        // if (GameData.instance.cam != null && GameData.instance != null)
        // {
        //     cam = GameData.instance.cam;
        //     // Debug.Log("CAM TEST -TurnUIToFollower got camera transform");
        // }
    }
    public void SetCustomCam(Transform transform)
    {
        if (transform != null)
        {
            cam = transform;
            // Debug.Log("CAM TEST -TurnUIToFollower got camera transform");
        }
    }
    // void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.tag == "Player")
    //     {
    //         // turnUIToFollower.enabled = true;
    //         SetCustomCam(other.transform);
    //     }
    // }
    // Start is called before the first frame update    private void LateUpdate()
    private void LateUpdate()
    {
        if (cam != null)
        {
            transform.LookAt(transform.position + cam.forward);
        }
    }
}
