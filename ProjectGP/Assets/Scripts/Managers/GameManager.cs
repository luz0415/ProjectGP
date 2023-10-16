using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GameManager : MonoBehaviour
{
    public static GameManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }
            return m_instance;
        }
    }

    private static GameManager m_instance;

    public bool isGameOver;
    private CameraController cameraController;
    private PostProcessVolume roomChangePostProcess;

    public float maxGrainIntensity = 1.0f;
    public float maxDOFFocalLength = 300.0f;

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        isGameOver = false;

        cameraController = FindObjectOfType<CameraController>();
        roomChangePostProcess = FindObjectOfType<PostProcessVolume>();

        SetPostProcessDOFFocalLength(0f);
        SetPostProcessGrainIntensity(0f);

    }

    public void EndGame()
    {
        isGameOver = true;
    }

    public void ChangeRoomCamera(Vector3 moveRoomPos)
    {
        cameraController.ChangeActiveCamera();
        cameraController.MoveDeactivedCamera(moveRoomPos);
    }

    public void SetPostProcessGrainIntensity(float intensity)
    {
        Grain grain;
        roomChangePostProcess.profile.TryGetSettings(out grain);
        grain.intensity.value = intensity;
    }

    public void SetPostProcessDOFFocalLength(float focalLength)
    {
        DepthOfField dof;
        roomChangePostProcess.profile.TryGetSettings(out dof);
        dof.focalLength.value = focalLength;
    }
}
