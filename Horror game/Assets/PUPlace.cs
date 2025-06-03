using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUPlace : MonoBehaviour
{

    void Start()
    {
        if (GameData.instance.puPlace == null)
        {
            GameData.instance.puPlace = this;
        }
    }

}
