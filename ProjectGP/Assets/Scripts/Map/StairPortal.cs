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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (nowSceneIndex == lastFloorSceneIndex-1) // 조건문 추가 마지막방 끝 들어갔을 때 엔딩 나오게 설정
            {
                DialogueTrigger.instance.CutSceneStart();
            }
            else if (nowSceneIndex != lastFloorSceneIndex)
            {
                GameManager.instance.ChangeScene();
                SceneManager.LoadScene(nowSceneIndex + 1);
            }
        }
    }
}
