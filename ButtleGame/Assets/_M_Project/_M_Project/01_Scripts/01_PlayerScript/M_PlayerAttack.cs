using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class M_PlayerAttack : MonoBehaviour
{
    [Header("�e�I�u�W�F�N�g�̃}�l�[�W���[")]
    [SerializeField]
    private M_PlayerManager pl_MGR;

    public enum eAttackBranch
    {
        None,
        N_Attack_1,
        N_Attack_2,
        N_Attack_3,
        ChargeAttack,
    }

    // ���g�̃R���|�[�l���g
    private M_CharactorStatus status;

    [Header("����ϐ�")]
    public bool isExtendAttack = false;
    public eAttackBranch currentAttackBranch = eAttackBranch.None;
    public eAttackBranch prevAttackBranch = eAttackBranch.None;



    void Start()
    {
        // �}�l�[�W���[�̎擾
        pl_MGR = GetComponent<M_PlayerManager>();

        status = GetComponent<M_CharactorStatus>();

        currentAttackBranch = eAttackBranch.None;
        prevAttackBranch = eAttackBranch.None;
    }

    void Update()
    {
        
    }

    private void Action_Attack()
    {
        if (isExtendAttack == true) return;

        // �U���A�j���[�V�����̍Đ�
        status.SetIsAttack(true);

        /* �������F�R�i�U�� */
        //// �U���̕���
        //if (currentAttackBranch == eAttackBranch.None)
        //{
        //    currentAttackBranch = eAttackBranch.N_Attack_1;
        //}
        //// �U���P���[�V�������ɓ��͂��ꂽ�ꍇ
        //else if (currentAttackBranch == eAttackBranch.N_Attack_1)
        //{
        //    //isExtendAttack = true;
        //    currentAttackBranch = eAttackBranch.N_Attack_2;
        //}
        //// �U���Q���[�V�������ɓ��͂��ꂽ�ꍇ
        //else if (currentAttackBranch == eAttackBranch.N_Attack_2)
        //{
        //    isExtendAttack = true;
        //    currentAttackBranch = eAttackBranch.N_Attack_3;
        //}

        Debug.Log("�U�����܂���");

        /* �U������̍쐬�̓A�j���[�V�����Ŋ֐����Ă�ł��܂� */
        /* �A�j���[�V�����I�����Ƀ��Z�b�g�֐����Ă�ł��܂� */
    }

    public void GamePad_Attack(InputAction.CallbackContext context)
    {
        // �{�^���̉����l��臒l���z�������𔻒肷��
        if (context.phase == InputActionPhase.Started)
        {
            Action_Attack();
        }
    }

    public void ResetAttack()
    {
        // �h���U�����̓��Z�b�g���Ȃ�
        if (isExtendAttack == true) return;

        // �U����Ԃ����Z�b�g����
        status.SetIsAttack(false);
        currentAttackBranch = eAttackBranch.None;

        Debug.Log("Execute�FResetAttack");
    }

    public void ResetExtendAttack()
    {
        isExtendAttack = false;

        // �U����Ԃ����Z�b�g����
        status.SetIsAttack(false);
        currentAttackBranch = eAttackBranch.None;

        Debug.Log("Execute�FResetExtendAttack");
    }

}
