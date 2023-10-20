using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealMap : Map
{
    protected override void Start()
    {
        base.Start();
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
