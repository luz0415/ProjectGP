using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRotator : MonoBehaviour
{
    private void Awake()
    {
        RotateMap();
    }
    
    private void RotateMap()
    {
        int rotateNumber = Random.Range(0, 4);
        transform.rotation = Quaternion.Euler(0f, (float)(rotateNumber) * 90f, 0f);
    }
}
