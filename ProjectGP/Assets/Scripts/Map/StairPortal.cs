using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StairPortal : MonoBehaviour
{
    private int lastFloorSceneIndex;
    private int nowSceneIndex;

    private void Start()
    {
        lastFloorSceneIndex = SceneManager.sceneCountInBuildSettings;
        nowSceneIndex = SceneManager.GetActiveScene().buildIndex;
        print(lastFloorSceneIndex);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if(nowSceneIndex != lastFloorSceneIndex)
            {
                SceneManager.LoadScene(nowSceneIndex + 1);
                GameManager.instance.ChangeRoomCamera(Vector3.zero);
            }
        }
    }
}
