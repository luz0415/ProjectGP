using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoUpStairMap : Map
{
    [SerializeField] protected GameObject[] stairPortals;

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

}
