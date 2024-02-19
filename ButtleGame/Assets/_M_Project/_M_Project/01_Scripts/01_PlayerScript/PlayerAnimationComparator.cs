using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationComparator : MonoBehaviour
{
    [Header("�e�I�u�W�F�N�g�̃}�l�[�W���[")]
    [SerializeField]
    private M_PlayerManager pl_MGR;

    [Header("�Q�Ƃ���I�u�W�F�N�g�F�v���C���[")]
    [SerializeField]
    private M_CharactorStatus pl_Status;
    [SerializeField]
    private Animator pl_Anim;

    void Start()
    {
        // �}�l�[�W���[�̎擾
        pl_MGR = GetComponent<M_PlayerManager>();

        // �R���|�[�l���g���擾
        pl_Status = GetComponent<M_CharactorStatus>();
        pl_Anim = GetComponent<Animator>();
    }

    void Update()
    {
        pl_Anim.SetBool("isMoving", pl_Status.GetIsMoving());
        pl_Anim.SetBool("isAttack", pl_Status.GetIsAttack());
        //pl_Anim.SetInteger("eAttackBranch", (int)pl_Status.currentAttackBranch);
        //pl_Anim.SetBool("isExtendAttack", pl_Status.isExtendAttack);
        pl_Anim.SetBool("isJumping", pl_Status.GetIsJumping());
        pl_Anim.SetBool("isSecondJump", pl_Status.GetIsSecondJump());
        //player_anim.SetInteger("moveDir", (int)moveDir);
    }
}
