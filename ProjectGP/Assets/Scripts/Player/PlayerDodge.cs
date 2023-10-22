using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodge : MonoBehaviour
{
    public float dashDistance = 5f;     // 대쉬 거리
    public float dashDuration = 0.5f;   // 대쉬 지속 시간
    public float dashCooldown = 3f;     // 대쉬 쿨타임 시간
    public LayerMask obstacleLayer;     // 대쉬 중 충돌 확인할 레이어

    // 대쉬 속도를 조절하려면 dashDistance를 크게하거나 dashDuration을 짧게하면 됨다.
    // 대쉬 속도를 시간/거리 로 설정을 해놨기 때문

    private bool canDash = true;        // 대쉬 가능 상태인가
    private bool isDodging = false;     // 회피 중인 상태인가
   
    void Start()
    {
        
    }

 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canDash)     // 스페이스 누르고 대쉬 가능 상태일 때 회피 실행
        {
            StartCoroutine(PerformDash());
        }
    }

    private IEnumerator PerformDash()
    {
        isDodging = true;
        canDash = false;

        // 대쉬 방향 설정 (현재 플레이어가 보는 방향)
        Vector3 dashDirection = transform.forward;

        // 대쉬 시작 지점
        Vector3 startPos = transform.position;

        // 대쉬 종료 지점
        Vector3 endPos = startPos + dashDirection * dashDistance;

        // 대쉬 시작 시간
        float startTime = Time.time;

        while ((Time.time - startTime) < dashDuration)
        {


            // 대쉬 중에 장애물과 충돌하면 대쉬를 중단 하도록 한다.
            if (Physics.Raycast(transform.position, dashDirection, dashDistance, obstacleLayer))
            {
                break;
            }
            // 대쉬 중에 이동하기
            float dashProgress = (Time.time - startTime) / dashDuration;
            transform.position = Vector3.Lerp(startPos, endPos, dashProgress);

            yield return null;
        }

        // 대쉬 종료 처리
        isDodging = false;

        // 대쉬 쿨타임 시작
        yield return new WaitForSeconds(dashCooldown);
        // 대쉬 가능 상태로 전환
        canDash = true;



    }
}
