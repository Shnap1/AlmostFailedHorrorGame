using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetUI : MonoBehaviour
{
    public TMP_Text targetText;
    public string targetName;
    public GameObject TargetCanvas;
    void Start()
    {
        TargetCanvas.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        //TargetCanvas.SetActive(true);
        if (other.gameObject.tag == "Player")
        {
            targetText.text = $"COLLECT {targetName}";
            TargetCanvas.SetActive(true);
            Debug.Log($"TargetCanvas.SetActive{TargetCanvas.activeSelf} ");
        }


    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") TargetCanvas.SetActive(false);
    }
}
