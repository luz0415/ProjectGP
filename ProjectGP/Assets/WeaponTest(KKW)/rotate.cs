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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 화면 좌표에서 출발해 카메라를 통해 월드좌표로 발사할 Ray
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) // Ray가 발사해 맞았다면
        {
            mousePosition = hit.point; // mousePosition에 맞은 월드좌표 대입
        }
        Vector3 rotatePosition = new Vector3(mousePosition.x, 0f, mousePosition.z) - myRigid.position;
        myRigid.rotation = Quaternion.Lerp(myRigid.rotation, Quaternion.LookRotation(rotatePosition), 3 * Time.deltaTime);


    }
}
