using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnUIToFollower : MonoBehaviour
{
    public Transform cam;
    // public TargetUI targetUI;
    void Awake()
    {
        SetCam();
    }
    public void SetCam()
    {
        if (GameData.instance.cam != null)
        {
            cam = GameData.instance.cam;
            Debug.Log("CAM TEST -TurnUIToFollower got camera transform");
        }
    }
    // Start is called before the first frame update    private void LateUpdate()
    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
