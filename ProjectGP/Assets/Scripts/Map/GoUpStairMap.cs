using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoUpStairMap : Map
{
    [SerializeField] protected GameObject[] stairPortals;
    [SerializeField] protected GameObject[] mysteriousReinforcements;

    public int firstFloorSceneIndex = 0;
    private int nowSceneIndex;

    public bool canGoUpStair = false;
    protected override void ActiveDoor()
    {
        base.ActiveDoor();
        bool isStairPortalOpened = false;

        for (int direction = 0; direction < 4; direction++)
        {
            if (!isStairPortalOpened && neighborMap[direction] == null)
            {
                doors[direction].SetActive(true);
                stairPortals[direction].SetActive(true);
                wallCenters[direction].SetActive(false);

                isStairPortalOpened = true;
            }
            else
            {
                stairPortals[direction].SetActive(false);
            }
        }
    }

    protected override void OpenDoor()
    {
        for (int direction = 0; direction < 4; direction++)
        {
            if (doors[direction].activeSelf && !stairPortals[direction].activeSelf)
            {
                Transform upperPart = doors[direction].transform.GetChild(0);
                Transform lowerPart = doors[direction].transform.GetChild(1);

                if (upperPart.position.y < doorUpperPartPosY)
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

    protected void OpenStairDoor()
    {
        for (int direction = 0; direction < 4; direction++)
        {
            if (stairPortals[direction].activeSelf)
            {
                Transform upperPart = doors[direction].transform.GetChild(0);
                Transform lowerPart = doors[direction].transform.GetChild(1);

                if (upperPart.position.y < doorUpperPartPosY)
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

    protected override void Start()
    {
        base.Start();
        nowSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PickMysteriousReinforcement();
    }

    private void PickMysteriousReinforcement()
    {
        if(nowSceneIndex - firstFloorSceneIndex == 0)
        {
            mysteriousReinforcements[0].SetActive(true);
        }
        else
        {
            mysteriousReinforcements[1].SetActive(true);
        }
    }

    private void Update()
    {
        if (isRoomEnd)
        {
            EndRoom();
        }
        if (canGoUpStair)
        {
            OpenStairDoor();
        }
    }

    protected override void EnterRoom()
    {
        base.EnterRoom();
        isRoomEnd = true;
        if(GameManager.instance.transform.childCount == 0)
        {
            canGoUpStair = true;
        }
    }
    protected override void RevisitRoom()
    {
        base.RevisitRoom();
        if (GameManager.instance.transform.childCount == 0)
        {
            canGoUpStair = true;
        }
    }
}
