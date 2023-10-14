using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    // ����� �� ���
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

    // �� ó��
}
