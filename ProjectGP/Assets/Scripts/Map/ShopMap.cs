using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMap : Map
{
    [SerializeField] GameObject[] stands;

    protected override void Start()
    {
        base.Start();
        DisplayItemsOnStands();
    }

    private void DisplayItemsOnStands()
    {
        foreach (var stand in stands)
        {
            ChooseRandomItem(stand);
        }
    }

    private void ChooseRandomItem(GameObject stand)
    {
        int itemCount = stand.transform.childCount;
        if (itemCount > 0)
        {
            int chosenItemIdx = Random.Range(0, itemCount);

            stand.transform.GetChild(chosenItemIdx).gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if(isRoomEnd)
        {
            EndRoom();
        }
    }

    protected override void EnterRoom()
    {
        base.EnterRoom();
        isRoomEnd = true;
    }
}
