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
    private double velocity;

    void Awake()
    {
        myTransform = transform;
    }

    void Start()
    {
        oldPosition = transform.position;

        StartCoroutine(SimulateProjectile());
    }

    private void Update()
    {
        SetTarget();
        SetVelocity_Y();

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
        var distance = Math.Sqrt(Math.Pow(dis.x, 2) + Math.Pow(dis.y, 2) + Math.Pow(dis.z, 2));
        velocity = distance / Time.deltaTime;
        oldPosition = currentPosition;
    }

    // 유탄 발사 로직
    IEnumerator SimulateProjectile()
    {
        yield return new WaitForSeconds(0.1f);

        // Move projectile to the position of throwing object + add some offset if needed.
        Projectile.position = myTransform.position + new Vector3(0, 0.0f, 0);

        // Calculate distance to target
        float target_Distance = Vector3.Distance(Projectile.position, Target);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = power * (target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity));

        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;

        // Rotate projectile to face the target.
        Projectile.rotation = Quaternion.LookRotation(Target - Projectile.position);

        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            Projectile.Translate(0, (Vy - power * (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            Projectile.rotation = Quaternion.LookRotation(Target - Projectile.position);
         

            elapse_time += Time.deltaTime;

            yield return null;
        }

        Instantiate(_VFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
