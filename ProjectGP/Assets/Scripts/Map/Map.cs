using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Map[] neighborMap = { null, null, null, null };

    public int neighborCount
    {
        get
        {
            int count = 0;
            foreach (var neighbor in neighborMap)
            {
                if (neighbor != null) count++;
            }
            return count;
        }
    }

    [SerializeField] private GameObject[] doors;
    [SerializeField] private GameObject[] wallCenters;
    [SerializeField] private GameObject[] portals;

    public bool isRoomEnd;

    public float doorUpperPartPosY = 7.5f;
    public float doorLowerPartPosY = -5f;
    public float doorOpenSpeed = 0.1f;

    private bool isNotVisited;
    private float waitForFirstVisit = 1.2f;
    private float waitForRevisit = 0.7f;

    // �� ó��
    private void Start()
    {
        ActiveDoor();
        isNotVisited = true;
    }

    private void Update()
    {
        if (isRoomEnd)
        {
            OpenDoor();
        }
    }

    private void ActiveDoor()
    {
        for(int direction = 0; direction < 4; direction++)
        {
            if (neighborMap[direction] != null)
            {
                int reverseDirection;
                if (direction % 2 == 0)
                {
                    reverseDirection = direction + 1;
                }
                else
                {
                    reverseDirection = direction - 1;
                }

                doors[direction].SetActive(true);
                portals[direction].SetActive(true);
                wallCenters[direction].SetActive(false);

                const int ENTRANCE_CHILD_POSITION = 2;

                Portal portal = portals[direction].GetComponent<Portal>();
                portal.targetMap = neighborMap[direction].gameObject;
                portal.targetEntrance = neighborMap[direction].portals[reverseDirection].transform.GetChild(ENTRANCE_CHILD_POSITION);
                portal.direction = direction;
            }
            else
            {
                doors[direction].SetActive(false);
                portals[direction].SetActive(false);
                wallCenters[direction].SetActive(true);
            }
        }
    }

    private void OpenDoor()
    {
        foreach(var door in doors)
        {
            if (door.activeSelf)
            {
                Transform upperPart = door.transform.GetChild(0);
                Transform lowerPart = door.transform.GetChild(1);

                if(upperPart.position.y < doorUpperPartPosY)
                {
                    Vector3 doorUpperPartPos = new Vector3(upperPart.position.x, doorUpperPartPosY, upperPart.position.z);

                    Vector3 velocity = Vector3.zero;
                    upperPart.position = Vector3.Lerp(upperPart.position, doorUpperPartPos, doorOpenSpeed);
                }

                if (lowerPart.position.y > doorLowerPartPosY)
                {
                    Vector3 doorLowerPartPos = new Vector3(lowerPart.position.x, doorLowerPartPosY, lowerPart.position.z);

                    Vector3 velocity = Vector3.zero;
                    lowerPart.position = Vector3.Lerp(lowerPart.position, doorLowerPartPos, doorOpenSpeed);
                }
            }


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (isNotVisited)
            {
                StartRoom();
                isNotVisited = false;
            }
            else
            {
                RevisitRoom();
            }

            EndRoom();
        }
    }

    private void StartRoom()
    {
        StartCoroutine(StartRoomCoroutine());
    }

    private IEnumerator StartRoomCoroutine()
    {
        GameManager.instance.SetPostProcessDOFFocalLength(GameManager.instance.maxGrainIntensity);
        GameManager.instance.SetPostProcessGrainIntensity(GameManager.instance.maxDOFFocalLength);

        Time.timeScale = 0f;

        for (float remainTime = waitForFirstVisit; remainTime >= 0; remainTime -= 0.01f)
        {
            float percent = remainTime / waitForFirstVisit;
            GameManager.instance.SetPostProcessDOFFocalLength(GameManager.instance.maxDOFFocalLength * percent);
            GameManager.instance.SetPostProcessGrainIntensity(GameManager.instance.maxGrainIntensity * percent);
            yield return new WaitForSecondsRealtime(0.01f);
        }

        GameManager.instance.SetPostProcessDOFFocalLength(0f);
        GameManager.instance.SetPostProcessGrainIntensity(0f);
        Time.timeScale = 1f;
    }
    private void RevisitRoom()
    {
        StartCoroutine(RevisitRoomCoroutine());
    }

    private IEnumerator RevisitRoomCoroutine()
    {

        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(waitForRevisit);
        Time.timeScale = 1f;
    }


    private void EndRoom()
    {
        isRoomEnd = true;
    }
}
