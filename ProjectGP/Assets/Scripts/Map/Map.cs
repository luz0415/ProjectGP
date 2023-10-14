using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    // 연결된 방 기록
    public Map[] neighborMap = { null, null, null, null };
    public int neighborCount
    {
        get
        {
            int count = 0;
            foreach(var neighbor in neighborMap)
            {
                if (neighbor != null) count++;
            }
            return count;
        }
    }

    // 문 처리
}
