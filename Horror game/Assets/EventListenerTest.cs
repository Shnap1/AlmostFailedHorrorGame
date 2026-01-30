using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventListenerTest : MonoBehaviour
{

    public void DebugOnPlayer(GameObject player)
    {
        if (player.tag == "Player")
        {
            Debug.Log("Player Found");
        }
    }
}
