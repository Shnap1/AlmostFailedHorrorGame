using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class camSensControl : MonoBehaviour
{
    // Start is called before the first frame update
    public Cinemachine.CinemachineVirtualCamera POV_VirtCam;
    public Cinemachine.CinemachineFreeLook TP_Cam;
    public Cinemachine.CinemachineFreeLook CombatCam;

    private CinemachinePOV povComponent;



    [SerializeField] public float customSpeed;

    //POV original speed
    float POV_rawHorizSpeed;
    float POV_rawVertSpeed;

    //Third Person original speed
    float TP_X_RAW_Speed;
    float TP_Y_RAW_Speed;

    //Combat original speed
    float Combat_X_RAW_Speed;
    float Combat_Y_RAW_Speed;



    void Start()
    {

        povComponent = POV_VirtCam.GetCinemachineComponent<CinemachinePOV>();
        if (povComponent == null)
        {
            Debug.LogError("POV component not found!");
        }

        SaveRawSpeeds();
        if (customSpeed <= 0f) customSpeed = 1f;

        UpdateSensitivity(customSpeed);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SaveRawSpeeds()
    {
        POV_rawHorizSpeed = povComponent.m_HorizontalAxis.m_MaxSpeed;
        POV_rawVertSpeed = povComponent.m_VerticalAxis.m_MaxSpeed;

        TP_X_RAW_Speed = TP_Cam.m_XAxis.m_MaxSpeed;
        TP_Y_RAW_Speed = TP_Cam.m_YAxis.m_MaxSpeed;

        Combat_X_RAW_Speed = CombatCam.m_XAxis.m_MaxSpeed;
        Combat_Y_RAW_Speed = CombatCam.m_YAxis.m_MaxSpeed;
    }

    public void UpdateSensitivity(float newSensitivity)
    {
        // Debug.Log("Sensitivity - UpdateSensitivity in camSensControl.cs --- " + newSensitivity);

        customSpeed = newSensitivity;

        if (povComponent != null)
        {
            // Modify axis speeds
            povComponent.m_HorizontalAxis.m_MaxSpeed = POV_rawHorizSpeed * customSpeed;
            // Base speed * multiplier
            povComponent.m_VerticalAxis.m_MaxSpeed = POV_rawHorizSpeed * customSpeed;
        }

        if (TP_Cam != null)
        {
            // Switch to InputValueGain mode
            TP_Cam.m_XAxis.m_MaxSpeed = TP_X_RAW_Speed * customSpeed;
            TP_Cam.m_YAxis.m_MaxSpeed = TP_Y_RAW_Speed * customSpeed;
        }

        if (CombatCam != null)
        {
            // Switch to InputValueGain mode
            CombatCam.m_XAxis.m_MaxSpeed = Combat_X_RAW_Speed * customSpeed;
            CombatCam.m_YAxis.m_MaxSpeed = Combat_Y_RAW_Speed * customSpeed;
        }

    }


}
