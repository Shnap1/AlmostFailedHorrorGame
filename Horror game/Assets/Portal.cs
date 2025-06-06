using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject thisPortal;
    public GameObject otherPortal;

    public GameObject _objectToTeleport;

    public enum PortalType { Standart, Dash }
    public PortalType currPortalType;

    public bool JustTeleported = false;


    void Start()
    {
        ReadynessChecker();
    }
    void ReadynessChecker()
    {
        if (thisPortal == null || otherPortal == null)
        {
            Debug.Log("portalEnter or portalExit is null");
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!JustTeleported && other.gameObject != null)
        {
            SendObject(otherPortal, other.gameObject);
        }
    }
    void OnTriggerExit(Collider other)
    {
        JustTeleported = false;
    }

    void SendObject(GameObject otherPortal, GameObject ObjectToTeleport)
    {
        switch (currPortalType)
        {
            case PortalType.Standart:

                otherPortal.GetComponent<Portal>().ReceiveObject(ObjectToTeleport);
                break;
            case PortalType.Dash:
                Teleport_Dash();
                break;
        }
    }

    void Teleport_Dash()
    {
        Debug.Log("Teleport_Dash() is not added yet");
    }

    public void ReceiveObject(GameObject ObjectToTeleport)
    {
        if (ObjectToTeleport.tag != "Player")
        {
            JustTeleported = true;
            ObjectToTeleport.transform.position = transform.position;
        }
        else if (ObjectToTeleport.tag == "Player")
        {
            ObjectToTeleport.GetComponent<PlayerStateMachine>().CharacterController.enabled = false;

            ObjectToTeleport.transform.position = transform.position;

            ObjectToTeleport.GetComponent<PlayerStateMachine>().CharacterController.enabled = true;

            JustTeleported = true;
        }

    }
}
