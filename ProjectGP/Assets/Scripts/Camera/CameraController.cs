using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private CinemachineBrain brain;
    private CinemachineVirtualCamera[] vCams = new CinemachineVirtualCamera[2];
    private Vector3 cameraPositionPreset;

    public CinemachineVirtualCamera activedCam
    {
        get
        {
            return (CinemachineVirtualCamera)brain.ActiveVirtualCamera;
        }
    }

    private void Awake()
    {
        brain = GetComponentInChildren<CinemachineBrain>();

        vCams = transform.GetComponentsInChildren<CinemachineVirtualCamera>();
        vCams[0].Priority = 0;
        vCams[1].Priority = 1;

        cameraPositionPreset = brain.transform.localPosition;
    }

    public void ChangeActiveCamera()
    {
        if (activedCam == null)
        {
            return;
        }

        if (activedCam == vCams[0])
        {
            vCams[0].Priority--;
            vCams[1].Priority++;
        }
        else
        {
            vCams[0].Priority++;
            vCams[1].Priority--;
        }
    }

    public void MoveDeactivedCamera(Vector3 movePos)
    {
        if (activedCam == vCams[0])
        {
            vCams[1].transform.position = cameraPositionPreset + movePos;
        }
        else
        {
            vCams[0].transform.position = cameraPositionPreset + movePos;
        }
    }
}
