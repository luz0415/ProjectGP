using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// �ٰŸ��̶� ���Ÿ� �� AI�� attackRange ��ȭ�� �ָ� �� ��

public class Enemy_Move : MonoBehaviour
{
    public float detectionRange = 10f; // ���� ���� ����  (Inspector â���� ���� ����)
    public float attackRange = 2f;     // ���� ���� ����   
    public float moveSpeed = 5f;       // �� ai �̵� �ӵ�

    private Rigidbody Rigid;
    private NavMeshAgent agent;

    private Transform target;
    private bool isAttacking = false;

    private Enemy enemy;
    private Animator animator;
    private Weapon weapon;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Rigid = GetComponent<Rigidbody>();
        target = GameManager.instance.player.transform;
        agent.speed = moveSpeed;
        agent.stoppingDistance = attackRange; // ������ ������ = ���� ���� ����

        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
        weapon = GetComponentInChildren<Weapon>();
    }


    void Update()
    {
        if (GameManager.instance != null && (GameManager.instance.isGamePaused || GameManager.instance.isEnemyPaused)) return;
        if(GameManager.instance.hasRoboticEye) agent.speed = moveSpeed * 0.9f;
        if (enemy.dead) return;

        // �÷��̾ ���� ���� ���� �ִ��� Ȯ���Ѵ�.
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        // ������������ �Ÿ��� ª�� ��
        if (distanceToPlayer <= detectionRange && !isAttacking)
        {
            transform.LookAt(target);
            agent.SetDestination(target.position); // �÷��̾� �����Ѵ�.

            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                // �÷��̾� ���� ������ �����ϸ� ���߰� ����
                isAttacking = true;
                Attack();
                
            }
            else if(agent.remainingDistance <= agent.stoppingDistance)
            {
                // ������ ���� �� �̵� ����
                agent.isStopped = true;
            }
            else
            {   // ���� ���� ���̰� �������� ���� �������� �̵� �����
                agent.isStopped = false;
            }
        }
        else
        {   // �÷��̾ ���� ������ ����� �̵� ����
            agent.isStopped = true;
            isAttacking = false;
        }
     
        if(agent.velocity.magnitude == 0)
        {
            animator.SetBool("isWalk", false);
        }
        else
        {
            animator.SetBool("isWalk", true);
        }
       
    }

    void Attack()
    {
        weapon.BulletFire();
    }

}
