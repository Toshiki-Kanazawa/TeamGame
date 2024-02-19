using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* �v���C���[�̏�Ԃ�Q�Ƃ����ׂ̐��l���i�[����N���X */
/* �G�L�������ł����p�ł���悤�݌v */

public class M_CharactorStatus : MonoBehaviour
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

    [Header("�L�����N�^�[�̃p�����[�^")]
    [SerializeField]
    private int hitPoint = 100;     // �̗�
    [SerializeField]
    private float stamina = 100.0f; // �s����
    [SerializeField]
    private int atk = 10;           // �U����
    [SerializeField]
    private float spd = 3.0f;       // �ړ����x

    [Header("��ԕω�")]
    [SerializeField]
    private bool invincible = false;       // ���G���
    [SerializeField]
    private float stunTime_Attack = 0.0f;  // �U���d��
    [SerializeField]
    private float stunTime_Damage = 0.0f;  // ��_�����̍d��

    [Header("�A�j���[�V��������ϐ�")]
    [SerializeField]
    private bool isMoving = false;   // �ړ���
    [SerializeField]
    private bool isGround = false;    // �n�ʂƐڐG���Ă���
    [SerializeField]
    private bool isJumping = false;  // �W�����v��
    [SerializeField]
    private bool isSecondJump = false; // �Q�i�W�����v��
    [SerializeField]
    private bool isAttack = false;   // �U����
    [SerializeField]
    private bool isLockon = false;   // ���b�N�I�����Ă���
    [SerializeField]
    private eMoveDirection eMoveDir; // �ړ�����


    void Start()
    {
        pl_MGR = GetComponent<M_PlayerManager>();
        // ���̃X�N���v�g�̑S�Ă̒l��������
        ResetIsAllValues();
    }

    void Update()
    {
        
    }

/* Setter */
    public void SetIsMoving(bool flag)
    {
        isMoving = flag;
    }
    public void SetIsGround(bool flag)
    {
        isGround = flag;
    }
    public void SetIsJumping(bool flag)
    {
        isJumping = flag;
    }
    public void SetIsSecondJump(bool flag)
    {
        isSecondJump = flag;
    }
    public void SetIsAttack(bool flag)
    {
        isAttack = flag;
    }
    public void SetIsLockOn(bool flag)
    {
        isLockon = flag;
    }
    public void SetMoveDirection(eMoveDirection m_dire)
    {
        eMoveDir = m_dire;
    }

    /* Getter */
    public bool GetIsMoving()
    {
        return isMoving;
    }
    public bool GetIsGround()
    {
        return isGround;
    }
    public bool GetIsJumping()
    {
        return isJumping;
    }
    public bool GetIsSecondJump()
    {
        return isSecondJump;
    }
    public bool GetIsAttack()
    {
        return isAttack;
    }
    public bool GetIsLockOn()
    {
        return isLockon;
    }
    public eMoveDirection GetMoveDirection()
    {
        return eMoveDir;
    }

    /* ReSet */
    public void ResetIsAllValues()
    {
        // �p�����[�^
        hitPoint = 100;
        stamina = 100.0f;
        atk = 10;
        spd = 3.0f;

        // ��ԕω�
        invincible = false;
        stunTime_Attack = 0.0f;
        stunTime_Damage = 0.0f;

        // �A�j���[�V��������ϐ�
        isMoving = false;
        isGround = false;
        isJumping = false;
        isSecondJump = false;
        isAttack = false;
        isLockon = false;
        eMoveDir = eMoveDirection.None;
    }
}
