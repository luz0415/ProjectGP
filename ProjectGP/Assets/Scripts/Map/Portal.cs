using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject targetMap;
    public Transform targetEntrance;
    public int direction;

    private Vector3 cameraPositionPreset;

    private void Start()
    {
        cameraPositionPreset = Camera.main.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.position = targetEntrance.transform.position;
            Camera.main.transform.position = cameraPositionPreset + targetMap.transform.position;
        }
    }
}
