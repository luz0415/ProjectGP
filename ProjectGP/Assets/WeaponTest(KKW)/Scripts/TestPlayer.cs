using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    // 스피드 조정 변수
    public float walkSpeed;
    public float runSpeed = 100f;
    public float applySpeed;

    // 상태 변수
    private bool isRun = false;

    // 민감도
    public float lookSensitivity;

    // 카메라 한계
    private float cameraRotationLimit;

    // 필요한 컴포넌트
    public Camera theCamera;
    private Rigidbody myRigid;
    private CapsuleCollider capsuleCollider;
    private Vector3 mousePosition;


    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        
        myRigid = GetComponent<Rigidbody>();
        applySpeed = walkSpeed;
        capsuleCollider = GetComponent<CapsuleCollider>();
        mousePosition = transform.rotation.eulerAngles;

        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        TryRun();
        Move();
        CharacterRotation();
    }

    private void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Running();
        }
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            RunningCancel();
        }
    }

    // 달리기 시도
    private void Running()
    {
        isRun = true;
        applySpeed = runSpeed;

        animator.SetBool("isRun", true);
    }
    // 달리기 해제
    private void RunningCancel()
    {
        isRun = false;
        applySpeed = walkSpeed;

        animator.SetBool("isRun", false);
    }

    // 캐릭터 움직임
    private void Move()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = Vector3.right * _moveDirX;
        Vector3 _moveVertical = Vector3.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;
        if (CheckHitWall(_velocity))
            _velocity = Vector3.zero;

        myRigid.MovePosition(myRigid.position + _velocity * Time.deltaTime);

        // walk 애니메이션을 위한 함수
        if (_moveDirX != 0 || _moveDirZ != 0)
            animator.SetBool("isWalk", true);
        else
        {
            animator.SetBool("isWalk", false);
            animator.SetTrigger("setIdle");
        }
    }

    //캐릭터 방향 회전
    private void CharacterRotation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 화면 좌표에서 출발해 카메라를 통해 월드좌표로 발사할 Ray
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) // Ray가 발사해 맞았다면
        {
            mousePosition = hit.point; // mousePosition에 맞은 월드좌표 대입
        }

        Vector3 rotatePosition = new Vector3(mousePosition.x, 0f, mousePosition.z) - myRigid.position;
        myRigid.rotation = Quaternion.Lerp(myRigid.rotation, Quaternion.LookRotation(rotatePosition), lookSensitivity * Time.deltaTime);
       //    print(myRigid.rotation);
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
           // Debug.DrawRay(pos, movement * scope, Color.red);
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

