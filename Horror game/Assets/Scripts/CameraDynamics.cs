using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraDynamics : MonoBehaviour
{
    [SerializeField]
    public Transform focusObjectTransform;

    public CinemachineFreeLook cinemachineVirtualCamera;

    private void Awake()
    {
        Camera.main.gameObject.TryGetComponent<CinemachineBrain>(out var brain);
        if (brain == null)
        {
            brain = Camera.main.gameObject.AddComponent<CinemachineBrain>();
        }

        brain.m_DefaultBlend.m_Time = 1;
        brain.m_ShowDebugText = true;


    }
    void Start()
    {
        // cinemachineVirtualCamera.Follow = focusObjectTransform;
        // cinemachineVirtualCamera.LookAt = focusObjectTransform;
    }
    public void SetLooktObject(Transform focusObjectTransform)
    {
        this.focusObjectTransform = focusObjectTransform;
        cinemachineVirtualCamera.Follow = focusObjectTransform;
        cinemachineVirtualCamera.LookAt = focusObjectTransform;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
