using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPackage_SG : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Transform[] childs = GetComponentsInChildren<Transform>(); //ÃÑ¾Ë ¹­À½¿¡¼­ ÀÚ½Ä 5°³¸¦ °¡Á®¿È

        for (int i = 0; i < childs.Length; i++) //°¢ ÃÑ¾ËÀÇ ºÎ¸ð-ÀÚ½Ä °ü°è¸¦ ²÷¾îÁÜ
        {
            childs[i].parent = null;
        }

        Destroy(gameObject); //ÃÑ¾ËÀ» ¹­°í ÀÖ´ø ºó ¿ÀºêÁ§Æ®¸¦ »èÁ¦
    }
}
