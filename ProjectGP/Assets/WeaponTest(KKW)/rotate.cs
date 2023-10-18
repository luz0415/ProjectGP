using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public Vector3 mousePosition;

    public Rigidbody myRigid;
    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // ȭ�� ��ǥ���� ����� ī�޶� ���� ������ǥ�� �߻��� Ray
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) // Ray�� �߻��� �¾Ҵٸ�
        {
            mousePosition = hit.point; // mousePosition�� ���� ������ǥ ����
        }
        Vector3 rotatePosition = new Vector3(mousePosition.x, 0f, mousePosition.z) - myRigid.position;
        myRigid.rotation = Quaternion.Lerp(myRigid.rotation, Quaternion.LookRotation(rotatePosition), 3 * Time.deltaTime);


    }
}
