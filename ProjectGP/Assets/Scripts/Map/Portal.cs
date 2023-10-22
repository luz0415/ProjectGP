using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject targetMap;
    public Transform targetEntrance;
    public int direction;

    private MapSpawner mapSpawner;
    private Map map;
    private void Start()
    {
        map = GetComponentInParent<Map>();
        mapSpawner = FindObjectOfType<MapSpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameManager.instance.isGamePaused = true;
            other.transform.position = targetEntrance.transform.position;
            GameManager.instance.ChangeRoomCamera(targetMap.transform.position);
            mapSpawner.RoomUIChange(map.roomCoord[0], map.roomCoord[1], direction);
        }
    }
}
