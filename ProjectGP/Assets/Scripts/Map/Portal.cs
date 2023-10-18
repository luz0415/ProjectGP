using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject targetMap;
    public Transform targetEntrance;
    public int direction;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.position = targetEntrance.transform.position;
            GameManager.instance.ChangeRoomCamera(targetMap.transform.position);
        }
    }
}