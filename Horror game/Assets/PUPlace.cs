using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUPlace : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        GameData.instance.puPlace = this;
    }
    void Start()
    {
        GameData.instance.puPlace = this;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
