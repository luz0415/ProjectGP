using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo_GrenadeLauncher : MonoBehaviour
{

    public float power = 2;             // 발사 속도에 관여
    public Vector3 Target;              // 날아갈 위치

    public float firingAngle = 45.0f;   // 발사각
    public float gravity = 9.8f;        // 중력값

    public Vector3 mousePosition;       // 마우스 포인터

    public Transform Projectile;        // 유탄
    private Transform myTransform;
    public GameObject _VFX;


    // 속도 구하기 위한 변수
    private Vector3 oldPosition;
    private Vector3 currentPosition;
    public double velocity;


    void Awake()
    {
        myTransform = transform;
    }

    void Start()
    {
        transform.RotateAround(transform.position, transform.right, -45);

        oldPosition = transform.position;

        SetTarget();

        StartCoroutine(pomulsun());
    }

    private void OnTriggerEnter(Collider other)
    {                
            Destroy(gameObject);
            Instantiate(_VFX, transform.position, Quaternion.identity);
    }

    IEnumerator pomulsun()
    {
        Vector3 start = transform.position; //현재 위치
        Vector3 end = Target; //끝나는 위치

        float jumpTime = Mathf.Max(0.3f, Vector3.Distance(start, end) / power); //거리분의 속력을 해서 시간을 얻어냄 (거,속,시)


        float currentTime = 0f;
        float percent = 0f;
        //y방향의 초기속도
        float v0 = (end - start).y + power;

        while (true)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / jumpTime; //percent는 currentTime == jumpTime이 되어야 1이 된다

            Vector3 position = Vector3.Lerp(start, end, percent); //자연스럽게 이동하기 위해 Lerp사용, percent만큼 이동

            //포물선 운동 : 시작위치 + 초기속도*시간 + 중력*시간제곱
            if(jumpTime <= 0.3f)
                position.y = start.y - (gravity * percent * percent);
            else
                position.y = start.y + (v0 * percent) - (gravity * percent * percent);

            transform.position = position;

            transform.RotateAround(transform.position, transform.right, Time.deltaTime * 90/jumpTime);

            yield return null;
        }

    }

    void SetTarget()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 화면 좌표에서 출발해 카메라를 통해 월드좌표로 발사할 Ray
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) // Ray가 발사해 맞았다면
        {
            mousePosition = hit.point; // mousePosition에 맞은 월드좌표 대입
        }

        Target = mousePosition;
    }

    void SetVelocity_Y()
    {
        currentPosition = transform.position;
        var dis = (currentPosition - oldPosition);
        var distance = dis.y;
        velocity = distance / Time.deltaTime;
        oldPosition = currentPosition;
    }
}