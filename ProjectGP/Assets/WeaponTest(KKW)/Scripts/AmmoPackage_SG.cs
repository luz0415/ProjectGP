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

        Transform[] childs = GetComponentsInChildren<Transform>(); //�Ѿ� �������� �ڽ� 5���� ������

        for (int i = 0; i < childs.Length; i++) //�� �Ѿ��� �θ�-�ڽ� ���踦 ������
        {
            childs[i].parent = null;
        }

        Destroy(gameObject); //�Ѿ��� ���� �ִ� �� ������Ʈ�� ����
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
