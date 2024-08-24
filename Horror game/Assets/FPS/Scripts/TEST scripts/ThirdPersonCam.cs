using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    // public Rigidbody rb;

    public float rotationSpeed;

    public Transform combatLookAt;

    public GameObject thirdPersonCam;
    public GameObject firstPersonCam;
    public GameObject combatCam;
    // public GameObject topDownCam;

    public CameraStyle currentStyle;
    public HideBody HB;
    public enum CameraStyle
    {
        Basic,
        Combat,
        POV,
        // Topdown
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // switch styles

        if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchCameraStyle(CameraStyle.Basic);

        if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchCameraStyle(CameraStyle.Combat);

        if (Input.GetKeyDown(KeyCode.Alpha3)) SwitchCameraStyle(CameraStyle.POV);


        // rotate orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        // roate player object
        if (currentStyle == CameraStyle.Basic)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (inputDir != Vector3.zero)
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }

        else if (currentStyle == CameraStyle.Combat)
        {
            Vector3 dirToCombatLookAt = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
            orientation.forward = dirToCombatLookAt.normalized;

            playerObj.forward = dirToCombatLookAt.normalized;


        }

        else if (currentStyle == CameraStyle.POV)
        {

        }

        // else if (currentStyle == CameraStyle.FirstPerson)
        // {
        //     Vector3 dirToCombatLookAt = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
        //     orientation.forward = dirToCombatLookAt.normalized;

        //     playerObj.forward = dirToCombatLookAt.normalized;

        // }
    }

    private void SwitchCameraStyle(CameraStyle newStyle)
    {
        combatCam.SetActive(false);
        thirdPersonCam.SetActive(false);
        firstPersonCam.SetActive(false);
        // topDownCam.SetActive(false);

        if (newStyle == CameraStyle.Basic)
        { thirdPersonCam.SetActive(true); HB.CallShowBodyObjectsWithDelay(true, .5f); }

        if (newStyle == CameraStyle.Combat)
        { combatCam.SetActive(true); HB.CallShowBodyObjectsWithDelay(true, .5f); }
        if (newStyle == CameraStyle.POV) { firstPersonCam.SetActive(true); HB.CallShowBodyObjectsWithDelay(false, 1f); }
        // if (newStyle == CameraStyle.Topdown) topDownCam.SetActive(true);

        currentStyle = newStyle;
    }
}
