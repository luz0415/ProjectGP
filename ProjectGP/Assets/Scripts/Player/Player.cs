using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
        applySpeed = walkSpeed;
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
        if(Input.GetKey(KeyCode.LeftShift))
        {
            Running();
        }
        if(!Input.GetKey(KeyCode.LeftShift))
        {
            RunningCancel();
        }
    }

    // 달리기 시도
    private void Running()
    {
        isRun = true;
        applySpeed = runSpeed;
    }
     // 달리기 해제
    private void RunningCancel()
    {
        isRun = false;
        applySpeed = walkSpeed;
    }

    // 캐릭터 움직임
    private void Move()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);

    }

    //캐릭터 방향 회전
    private void CharacterRotation()
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
    }
}
