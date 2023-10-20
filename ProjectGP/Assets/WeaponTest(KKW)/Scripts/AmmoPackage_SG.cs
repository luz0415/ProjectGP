using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPackage_SG : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 fwb = transform.TransformDirection(Vector3.up);

        Transform[] childs = GetComponentsInChildren<Transform>(); //ÃÑ¾Ë ¹­À½¿¡¼­ ÀÚ½Ä 5°³¸¦ °¡Á®¿È

        for (int i = 0; i < childs.Length; i++) //°¢ ÃÑ¾ËÀÇ ºÎ¸ð-ÀÚ½Ä °ü°è¸¦ ²÷¾îÁÜ
        {
            childs[i].parent = null;
        }

        Destroy(gameObject); //ÃÑ¾ËÀ» ¹­°í ÀÖ´ø ºó ¿ÀºêÁ§Æ®¸¦ »èÁ¦
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
