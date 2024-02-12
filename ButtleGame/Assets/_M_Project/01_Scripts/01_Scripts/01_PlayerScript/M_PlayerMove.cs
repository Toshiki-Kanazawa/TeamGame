using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// �J�����̕�������ɓ��삷�� �v���C���[�̈ړ� �̋������������܂�
//

public class M_PlayerMove : MonoBehaviour
{
    public enum eAttackBranch
    {
        None,
        N_Attack_1,
        N_Attack_2,
        N_Attack_3,
        ChargeAttack,
    }

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

    [Header("��ɂ���J����")]
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

    [Header("�A�j���[�V��������ϐ�")]
    public bool isMoving = false;
    public bool isGround = true;
    public bool isJumping = false;
    public bool isSecondJump = false;
    public bool isAttack = false;
    public bool isExtendAttack = false;
    public eAttackBranch currentAttackBranch = eAttackBranch.None;
    public eAttackBranch prevAttackBranch = eAttackBranch.None;
    public bool isLockon = false;
    public eMoveDirection eMoveDir;


    // �ʂ̃v���O�����Ő��䂵����
    [Header("�U������n�ϐ�")]
    public GameObject[] HitBall_Creator;
    private Transform[] HitBall_CreatePos;
    public GameObject HitBall_Prefab;
    private GameObject tmp_HitBall;


    /* ---�� Executes ��------------------------------------------------------------------------------------- */

    void Start()
    {
        // ���C���J�������擾
        mainCamera = Camera.main;

        // ���W�b�h�{�f�B����
        rb = this.GetComponent<Rigidbody>();

        // �q�b�g������o���ʒu�̔z����`
        HitBall_CreatePos = new Transform[HitBall_Creator.Length];
        for (int i = 0; i < HitBall_Creator.Length; i++)
        {
            HitBall_CreatePos[i] = HitBall_Creator[i].transform;
        }

        // ������
        isMoving = false;
        isGround = true;
        isJumping = false;
        isSecondJump = false;
        isAttack = false;
        isLockon = false;
        eMoveDir = eMoveDirection.None;
    }

    void Update()
    {
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");

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
        isGround = true;
        isJumping = false;
        isSecondJump = false;

        Debug.Log("�����ɏՓ˂��܂����F�s�������񕜂��܂���");
    }

    /* ---�� Functions ��------------------------------------------------------------------------------------- */

    private void PlayerMove_Source_CameraDirection()
    {
        // �A�j���[�V�������f�p
        isMoving = false;

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
            isMoving = true;
        }

        Debug.Log("�v���C���[���J������������ɓ����Ă��܂�");
    }

    private void Action_Jump()
    {
        if (isSecondJump == true) return;

        // ������ւ̃x�N�g��
        Vector3 jump_vec = Vector3.up;

        // rigidbody �ɗ^����W�����v�ʂ̍쐬
        jump_y = jump_vec.y * addVelocity;

        // rigidody�ɓK��
        rb.velocity = new Vector3(0.0f, jump_y, 0.0f);

        // �t���O����
        if (isJumping == true)
        {
            isSecondJump = true;
            Debug.Log("�Q�i�W�����v���܂���");
        }
        isJumping = true;
        isGround = false;

        Debug.Log("�W�����v���܂���");
    }

    private void Action_Attack()
    {
        if (isExtendAttack == true) return;

        // �U���A�j���[�V�����̍Đ�
        isAttack = true;

        // �U���̕���
        if(currentAttackBranch == eAttackBranch.None)
        {
            currentAttackBranch = eAttackBranch.N_Attack_1;
        }
        // �U���P���[�V�������ɓ��͂��ꂽ�ꍇ
        else if(currentAttackBranch == eAttackBranch.N_Attack_1)
        {
            //isExtendAttack = true;
            currentAttackBranch = eAttackBranch.N_Attack_2;
        }
        // �U���Q���[�V�������ɓ��͂��ꂽ�ꍇ
        else if(currentAttackBranch == eAttackBranch.N_Attack_2)
        {
            isExtendAttack = true;
            currentAttackBranch = eAttackBranch.N_Attack_3;
        }

        Debug.Log("�U�����܂���");

        /* �U������̍쐬�̓A�j���[�V�����Ŋ֐����Ă�ł��܂� */
        /* �A�j���[�V�����I�����Ƀ��Z�b�g�֐����Ă�ł��܂� */
    }

    private void Action_SpinAttack()
    {
        Debug.Log("�X�s���A�^�b�N���܂���");
    }

    public void GamePad_Jump(InputAction.CallbackContext context)
    {
        // �{�^���̉����l��臒l���z�������𔻒肷��
        if (context.phase == InputActionPhase.Started)
        {
            Action_Jump();
        }
    }

    public void GamePad_Attack(InputAction.CallbackContext context)
    {
        // �{�^���̉����l��臒l���z�������𔻒肷��
        if (context.phase == InputActionPhase.Started)
        {
            Action_Attack();
        }
    }

    //public void GamePad_HoldAttack(InputAction.CallbackContext context)
    //{
    //    if(context.interaction is InputInteractionContext.)
    //    {
    //        Action_SpinAttack();
    //    }
    //}

    public void ResetAttack()
    {
        // �h���U�����̓��Z�b�g���Ȃ�
        if (isExtendAttack) return;

        // �U����Ԃ����Z�b�g����
        isAttack = false;
        currentAttackBranch = eAttackBranch.None;

        Debug.Log("Execute�FResetAttack");
    }

    public void ResetExtendAttack()
    {
        isExtendAttack = false;

        // �U����Ԃ����Z�b�g����
        isAttack = false;
        currentAttackBranch = eAttackBranch.None;

        Debug.Log("Execute�FResetExtendAttack");
    }

    public void CreateHitBall()
    {
        // �q�b�g������쐬
        tmp_HitBall = null;
        tmp_HitBall = Instantiate(HitBall_Prefab);

        // �q�b�g����̏��L�҂�o�^�F���ʗp
        tmp_HitBall.GetComponent<System_HitBall>().SetCreator(this.gameObject);

        // �����蔻��{�[�����q�b�gPos�ɒǏ]������
        tmp_HitBall.transform.position = HitBall_CreatePos[0].transform.position;
        tmp_HitBall.transform.rotation = HitBall_CreatePos[0].transform.rotation;
        tmp_HitBall.transform.SetParent(HitBall_Creator[0].transform);    // �{�[���̐e��ݒ�(����Ǐ]�p)
    }

    public void DeleteHitBall()
    {
        // �q�b�g������폜����
        Destroy(tmp_HitBall);
        tmp_HitBall = null;     // �N���A���Ă���
    }

    private void AddGravity()
    {
        if (isGround == false)
        {
            rb.AddForce(new Vector3(0.0f, -gravityPower, 0.0f));
            Debug.Log("�d�͍~�����Ă��܂�");
        }
    }
}
