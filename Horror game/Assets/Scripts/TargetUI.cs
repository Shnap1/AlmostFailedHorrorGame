using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetUI : MonoBehaviour
{
    public TMP_Text targetText;
    public string targetName;
    public GameObject TargetCanvas;
    public TurnUIToFollower turnUIToFollower;

    bool triggered = false;

    void Start()
    {
        TargetCanvas.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        //TargetCanvas.SetActive(true);
        if (other.gameObject.tag == "Player")
        {
            // turnUIToFollower.enabled = true;
            turnUIToFollower.SetCam();

            targetText.text = $"COLLECT {targetName}";
            TargetCanvas.SetActive(true);
            //Debug.Log($"TargetCanvas.SetActive{TargetCanvas.activeSelf} ");
        }


    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") TargetCanvas.SetActive(false);
    }
}
