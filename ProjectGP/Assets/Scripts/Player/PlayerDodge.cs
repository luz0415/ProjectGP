using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PlayerDodge : MonoBehaviour
{
    public float dashDistance = 5f;     // �뽬 �Ÿ�
    public float dashDuration = 0.5f;   // �뽬 ���� �ð�
    public float dashCooldown = 3f;     // �뽬 ��Ÿ�� �ð�
    public LayerMask obstacleLayer;     // �뽬 �� �浹 Ȯ���� ���̾�

    // �뽬 �ӵ��� �����Ϸ��� dashDistance�� ũ���ϰų� dashDuration�� ª���ϸ� �ʴ�.
    // �뽬 �ӵ��� �ð�/�Ÿ� �� ������ �س��� ����

    private bool canDash = true;        // �뽬 ���� �����ΰ�

    public bool hasStealthModule = false;
    private CapsuleCollider capsuleCollider;

    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canDash)     // �����̽� ������ �뽬 ���� ������ �� ȸ�� ����
        {
            StartCoroutine(PerformDash());
        }
    }

    private IEnumerator PerformDash()
    {
        canDash = false;

        // �뽬 ���� ���� (���� �÷��̾ ���� ����)
        Vector3 dashDirection = transform.forward;

        // �뽬 ���� ����
        Vector3 startPos = transform.position;

        // �뽬 ���� ����
        Vector3 endPos = startPos + dashDirection * dashDistance;

        // �뽬 ���� �ð�
        float startTime = Time.time;

        if (hasStealthModule) GameManager.instance.isEnemyPaused = true;
        GetComponent<PlayerHP>().isDodging = true;

        while ((Time.time - startTime) < dashDuration)
        {
            // �뽬 �߿� ��ֹ��� �浹�ϸ� �뽬�� �ߴ� �ϵ��� �Ѵ�.
            if (CheckHitWall(dashDirection))
            {
                break;
            }

            // �뽬 �߿� �̵��ϱ�
            float dashProgress = (Time.time - startTime) / dashDuration;
            transform.position = Vector3.Lerp(startPos, endPos, dashProgress);

            yield return null;
        }

        if (hasStealthModule) GameManager.instance.isEnemyPaused = false;
        GetComponent<PlayerHP>().isDodging = false;

        // �뽬 ��Ÿ�� ����
        yield return new WaitForSeconds(dashCooldown);
        // �뽬 ���� ���·� ��ȯ
        canDash = true;
    }

    bool CheckHitWall(Vector3 movement)
    {
        // scope�� ray �浹�� Ȯ���� ������ ������ �� �ִ�.
        float scope = 1f;

        // �÷��̾��� �Ӹ�, ����, �� �� 3�������� ray�� ���.
        List<Vector3> rayPositions = new List<Vector3>();
        rayPositions.Add(transform.position + Vector3.up * capsuleCollider.height * 0.5f);
        rayPositions.Add(transform.position + Vector3.up * capsuleCollider.height * 1.0f);
        rayPositions.Add(transform.position + Vector3.up * capsuleCollider.height * 2.0f);

        // ������� ���� ray�� ȭ�鿡 �׸���.
        foreach (Vector3 pos in rayPositions)
        {
            Debug.DrawRay(pos, movement * scope, Color.red);
        }

        // ray�� ���� �浹�� Ȯ���Ѵ�.
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
