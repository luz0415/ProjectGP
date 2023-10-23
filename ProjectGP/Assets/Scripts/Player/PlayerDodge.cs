using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PlayerDodge : MonoBehaviour
{
    public float dashDistance = 5f;     // 대쉬 거리
    public float dashDuration = 0.5f;   // 대쉬 지속 시간
    public float dashCooldown = 3f;     // 대쉬 쿨타임 시간
    public LayerMask obstacleLayer;     // 대쉬 중 충돌 확인할 레이어

    // 대쉬 속도를 조절하려면 dashDistance를 크게하거나 dashDuration을 짧게하면 됨다.
    // 대쉬 속도를 시간/거리 로 설정을 해놨기 때문

    private bool canDash = true;        // 대쉬 가능 상태인가

    public bool hasStealthModule = false;
    private CapsuleCollider capsuleCollider;

    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
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
        canDash = false;

        // 대쉬 방향 설정 (현재 플레이어가 보는 방향)
        Vector3 dashDirection = transform.forward;

        // 대쉬 시작 지점
        Vector3 startPos = transform.position;

        // 대쉬 종료 지점
        Vector3 endPos = startPos + dashDirection * dashDistance;

        // 대쉬 시작 시간
        float startTime = Time.time;

        if (hasStealthModule) GameManager.instance.isEnemyPaused = true;
        GetComponent<PlayerHP>().isDodging = true;

        while ((Time.time - startTime) < dashDuration)
        {
            // 대쉬 중에 장애물과 충돌하면 대쉬를 중단 하도록 한다.
            if (CheckHitWall(dashDirection))
            {
                break;
            }

            // 대쉬 중에 이동하기
            float dashProgress = (Time.time - startTime) / dashDuration;
            transform.position = Vector3.Lerp(startPos, endPos, dashProgress);

            yield return null;
        }

        if (hasStealthModule) GameManager.instance.isEnemyPaused = false;
        GetComponent<PlayerHP>().isDodging = false;

        // 대쉬 쿨타임 시작
        yield return new WaitForSeconds(dashCooldown);
        // 대쉬 가능 상태로 전환
        canDash = true;
    }

    bool CheckHitWall(Vector3 movement)
    {
        // scope로 ray 충돌을 확인할 범위를 지정할 수 있다.
        float scope = 1f;

        // 플레이어의 머리, 가슴, 발 총 3군데에서 ray를 쏜다.
        List<Vector3> rayPositions = new List<Vector3>();
        rayPositions.Add(transform.position + Vector3.up * capsuleCollider.height * 0.5f);
        rayPositions.Add(transform.position + Vector3.up * capsuleCollider.height * 1.0f);
        rayPositions.Add(transform.position + Vector3.up * capsuleCollider.height * 2.0f);

        // 디버깅을 위해 ray를 화면에 그린다.
        foreach (Vector3 pos in rayPositions)
        {
            Debug.DrawRay(pos, movement * scope, Color.red);
        }

        // ray와 벽의 충돌을 확인한다.
        foreach (Vector3 pos in rayPositions)
        {
            if (Physics.Raycast(pos, movement, out RaycastHit hit, scope))
            {
                if (hit.collider.CompareTag("Wall"))
                    return true;
            }
        }
        return false;
    }
}
