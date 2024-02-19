using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// �J�����̕�������ɓ��삷�� �v���C���[�̈ړ� �̋������������܂�
//

public class M_PlayerMove : MonoBehaviour
{
    [Header("�e�I�u�W�F�N�g�̃}�l�[�W���[")]
    [SerializeField]
    private M_PlayerManager pl_MGR;

    public enum eMoveDirection
    {
        None,
        Forward,
        Back,
        Left,
        Right
    }

    // ���g�̃R���|�[�l���g
    private Rigidbody rb;
    private M_CharactorStatus status;

    [Header("��ɂ���J����")]
    private GameObject cam_obj;
    public Camera mainCamera;
    public Vector3 n_CameraForward = new Vector3(0.0f, 0.0f, 0.0f);

    [Header("�ړ��n�ϐ�")]
    public float moveSpeed = 3.0f;  // �ړ����x �� �p�����[�^�p�̃v���O�����Ɉړ�
    public float inputHorizontal;   // �������̓���
    public float inputVertical;     // �c�����̓���
    public Vector3 moveForward = new Vector3(0.0f, 0.0f, 0.0f);

    [Header("�W�����v�����ϐ�")]
    public float gravityPower = 3.0f;
    public float addVelocity = 9.0f;
    public float jump_y = 0.0f;

    /* ---�� Executes ��------------------------------------------------------------------------------------- */

    void Start()
    {
        // �}�l�[�W���[�̎擾
        pl_MGR = this.gameObject.GetComponent<M_PlayerManager>();

        /* �v���C���[�J�����̎擾�̓}�l�[�W���[�ŏ��� */

        // ���W�b�h�{�f�B����
        rb = GetComponent<Rigidbody>();
        status = GetComponent<M_CharactorStatus>();
    }

    void Update()
    {
        // �n�ʂɂ��Ȃ��Ƃ� �d�͂����Z����
        AddGravity();
    }

    private void FixedUpdate()
    {
        // �J�����̌�������� �v���C���[���ړ�������
        PlayerMove_Source_CameraDirection();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �s�������񕜂���
        status.SetIsGround(true);
        status.SetIsJumping(false);
        status.SetIsSecondJump(false);

        Debug.Log("�����ɏՓ˂��܂����F�s�������񕜂��܂���");
    }

    /* ---�� Functions ��------------------------------------------------------------------------------------- */

    public void GamePad_LeftStick_CameraControl(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<Vector2>();
        inputHorizontal = value.x;
        inputVertical = value.y;
    }

    private void PlayerMove_Source_CameraDirection()
    {
        // �A�j���[�V�������f�p
        status.SetIsMoving(false);

        // �J�����̕������� X-Z���ʂ̒P�ʃx�N�g�����擾
        n_CameraForward = Vector3.Scale(mainCamera.transform.forward, new Vector3(1.0f, 0.0f, 1.0f)).normalized;

        // �������͂̓��͒l�ƃJ�����̌�������ړ�����������
        moveForward = n_CameraForward * inputVertical + mainCamera.transform.right * inputHorizontal;

        // �ړ������ɃX�s�[�h��K������
        // �W�����v�◎��������ꍇ�A�ʓr Y�������̃x�N�g�� �𑫂�
        rb.velocity = moveForward * moveSpeed + new Vector3(0.0f, rb.velocity.y, 0.0f);

        // �L�����N�^�[�̌�����i�s�����Ɍ�����
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
            status.SetIsMoving(true);
        }

        Debug.Log("�v���C���[���J������������ɓ����Ă��܂�");
    }

    private void Action_Jump()
    {
        if (status.GetIsSecondJump() == true) return;

        // ������ւ̃x�N�g��
        Vector3 jump_vec = Vector3.up;

        // rigidbody �ɗ^����W�����v�ʂ̍쐬
        jump_y = jump_vec.y * addVelocity;

        // rigidody�ɓK��
        rb.velocity = new Vector3(0.0f, jump_y, 0.0f);

        // �t���O����
        if (status.GetIsJumping() == true)
        {
            status.SetIsSecondJump(true);
            Debug.Log("�Q�i�W�����v���܂���");
        }
        status.SetIsJumping(true);
        status.SetIsGround(true);

        Debug.Log("�W�����v���܂���");
    }

    public void GamePad_Jump(InputAction.CallbackContext context)
    {
        // �{�^���̉����l��臒l���z�������𔻒肷��
        if (context.phase == InputActionPhase.Started)
        {
            Action_Jump();
        }
    }

    private void AddGravity()
    {
        if (status.GetIsGround() == false)
        {
            rb.AddForce(new Vector3(0.0f, -gravityPower, 0.0f));
            Debug.Log("�d�͍~�����Ă��܂�");
        }
    }

    public void SetPlayerCamera(Camera cam)
    {
        mainCamera = cam;
    }
}
