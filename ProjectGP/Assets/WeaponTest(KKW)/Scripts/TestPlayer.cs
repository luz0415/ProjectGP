using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TestPlayer : MonoBehaviour
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

    // �޸��� �õ�
    private void Running()
    {
        isRun = true;
        applySpeed = runSpeed;

        animator.SetBool("isRun", true);
    }
    // �޸��� ����
    private void RunningCancel()
    {
        isRun = false;
        applySpeed = walkSpeed;

        animator.SetBool("isRun", false);
    }

    // ĳ���� ������
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

        // walk �ִϸ��̼��� ���� �Լ�
        if (_moveDirX != 0 || _moveDirZ != 0)
            animator.SetBool("isWalk", true);
        else
        {
            animator.SetBool("isWalk", false);
            animator.SetTrigger("setIdle");
        }
    }

    //ĳ���� ���� ȸ��
    private void CharacterRotation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // ȭ�� ��ǥ���� ����� ī�޶� ���� ������ǥ�� �߻��� Ray
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) // Ray�� �߻��� �¾Ҵٸ�
        {
            mousePosition = hit.point; // mousePosition�� ���� ������ǥ ����
        }

        Vector3 rotatePosition = new Vector3(mousePosition.x, 0f, mousePosition.z) - myRigid.position;
        myRigid.rotation = Quaternion.Lerp(myRigid.rotation, Quaternion.LookRotation(rotatePosition), lookSensitivity * Time.deltaTime);
       //    print(myRigid.rotation);
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
           // Debug.DrawRay(pos, movement * scope, Color.red);
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

