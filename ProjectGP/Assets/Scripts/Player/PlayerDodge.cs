using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodge : MonoBehaviour
{
    public float dashDistance = 5f;     // �뽬 �Ÿ�
    public float dashDuration = 0.5f;   // �뽬 ���� �ð�
    public float dashCooldown = 3f;     // �뽬 ��Ÿ�� �ð�
    public LayerMask obstacleLayer;     // �뽬 �� �浹 Ȯ���� ���̾�

    // �뽬 �ӵ��� �����Ϸ��� dashDistance�� ũ���ϰų� dashDuration�� ª���ϸ� �ʴ�.
    // �뽬 �ӵ��� �ð�/�Ÿ� �� ������ �س��� ����

    private bool canDash = true;        // �뽬 ���� �����ΰ�
    private bool isDodging = false;     // ȸ�� ���� �����ΰ�
   
    void Start()
    {
        
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
        isDodging = true;
        canDash = false;

        // �뽬 ���� ���� (���� �÷��̾ ���� ����)
        Vector3 dashDirection = transform.forward;

        // �뽬 ���� ����
        Vector3 startPos = transform.position;

        // �뽬 ���� ����
        Vector3 endPos = startPos + dashDirection * dashDistance;

        // �뽬 ���� �ð�
        float startTime = Time.time;

        while ((Time.time - startTime) < dashDuration)
        {


            // �뽬 �߿� ��ֹ��� �浹�ϸ� �뽬�� �ߴ� �ϵ��� �Ѵ�.
            if (Physics.Raycast(transform.position, dashDirection, dashDistance, obstacleLayer))
            {
                break;
            }
            // �뽬 �߿� �̵��ϱ�
            float dashProgress = (Time.time - startTime) / dashDuration;
            transform.position = Vector3.Lerp(startPos, endPos, dashProgress);

            yield return null;
        }

        // �뽬 ���� ó��
        isDodging = false;

        // �뽬 ��Ÿ�� ����
        yield return new WaitForSeconds(dashCooldown);
        // �뽬 ���� ���·� ��ȯ
        canDash = true;



    }
}
