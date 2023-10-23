using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 근거리이랑 원거리 적 AI는 attackRange 변화만 주면 될 듯

public class Enemy_Move : MonoBehaviour
{
    public float detectionRange = 10f; // 추적 감지 범위  (Inspector 창에서 변경 가능)
    public float attackRange = 2f;     // 공격 감지 범위   
    public float moveSpeed = 5f;       // 적 ai 이동 속도

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
        agent.stoppingDistance = attackRange; // 도착한 목적지 = 공격 범위 시작

        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
        weapon = GetComponentInChildren<Weapon>();
    }


    void Update()
    {
        if (GameManager.instance != null && (GameManager.instance.isGamePaused || GameManager.instance.isEnemyPaused)) return;
        if(GameManager.instance.hasRoboticEye) agent.speed = moveSpeed * 0.9f;
        if (enemy.dead) return;

        // 플레이어가 추적 범위 내에 있는지 확인한다.
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        // 추적범위보다 거리가 짧을 때
        if (distanceToPlayer <= detectionRange && !isAttacking)
        {
            transform.LookAt(target);
            agent.SetDestination(target.position); // 플레이어 추적한다.

            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                // 플레이어 공격 범위에 도착하면 멈추고 공격
                isAttacking = true;
                Attack();
                
            }
            else if(agent.remainingDistance <= agent.stoppingDistance)
            {
                // 목적지 도착 시 이동 정지
                agent.isStopped = true;
            }
            else
            {   // 공격 범위 밖이고 목적지에 도착 안했으면 이동 계속함
                agent.isStopped = false;
            }
        }
        else
        {   // 플레이어가 추적 범위를 벗어나면 이동 중지
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
