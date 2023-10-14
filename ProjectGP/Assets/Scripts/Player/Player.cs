using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // ���ǵ� ���� ����
    public float walkSpeed;
    public float runSpeed = 100f;
    public float applySpeed;

    // ���� ����
    private bool isRun = false;

    // �ΰ���
    public float lookSensitivity;

    // ī�޶� �Ѱ�
    private float cameraRotationLimit;

    // �ʿ��� ������Ʈ
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

    // �޸��� �õ�
    private void Running()
    {
        isRun = true;
        applySpeed = runSpeed;
    }
     // �޸��� ����
    private void RunningCancel()
    {
        isRun = false;
        applySpeed = walkSpeed;
    }

    // ĳ���� ������
    private void Move()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);

    }

    //ĳ���� ���� ȸ��
    private void CharacterRotation()
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
    }
}
