using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationComparator : MonoBehaviour
{
    [Header("親オブジェクトのマネージャー")]
    [SerializeField]
    private M_PlayerManager pl_MGR;

    [Header("参照するオブジェクト：プレイヤー")]
    [SerializeField]
    private M_CharactorStatus pl_Status;
    [SerializeField]
    private Animator pl_Anim;

    void Start()
    {
        // マネージャーの取得
        pl_MGR = GetComponent<M_PlayerManager>();

        // コンポーネントを取得
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
