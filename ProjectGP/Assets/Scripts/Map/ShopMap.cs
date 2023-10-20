using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopMap : Map
{
    [SerializeField] GameObject[] stands;
    private int reinforcementItemCount;

    protected override void Start()
    {
        base.Start();
        DisplayItemsOnStands();
    }

    private void DisplayItemsOnStands()
    {
        for(int i = 0; i < stands.Length - 1; i++)
        {
            if(i == stands.Length - 2)
            {
                ChooseRandomReinforcementItems(stands[i], stands[i + 1]);
            }
            else
            {
                ChooseRandomItem(stands[i]);
            }

        }
    }

    private void ChooseRandomItem(GameObject stand)
    {
        int itemCount = stand.transform.childCount;
        int chosenItemIdx;
        if (itemCount > 0)
        {
            chosenItemIdx = Random.Range(0, itemCount);

            stand.transform.GetChild(chosenItemIdx).gameObject.SetActive(true);
        }
    }

    private void ChooseRandomReinforcementItems(GameObject stand1, GameObject stand2)
    {
        int itemCount = stand1.transform.childCount;
        List<int> numbers = Enumerable.Range(0, itemCount).ToList();

        int chosenItemIdx;
        if (itemCount > 0)
        {
            chosenItemIdx = Random.Range(0, itemCount);
            stand1.transform.GetChild(chosenItemIdx).gameObject.SetActive(true);

            numbers.Remove(chosenItemIdx);
            chosenItemIdx = numbers[Random.Range(0, itemCount-1)];
            stand2.transform.GetChild(chosenItemIdx).gameObject.SetActive(true);
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
