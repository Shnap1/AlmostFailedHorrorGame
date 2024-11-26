using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnUIToFollower : MonoBehaviour
{
    public Transform cam;
    void Start()
    {
        cam = GameData.instance.cam;
    }
    // Start is called before the first frame update    private void LateUpdate()
    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
