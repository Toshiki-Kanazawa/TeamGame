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
    private M_PlayerMove pl_Move;
    [SerializeField]
    private M_PlayerAttack pl_Attack;
    [SerializeField]
    private Animator pl_Anim;

    void Start()
    {
        // マネージャーの取得
        pl_MGR = this.gameObject.GetComponent<M_PlayerManager>();

        // コンポーネントを取得
        pl_Move = pl_MGR.GetComponent<M_PlayerMove>();
        pl_Attack = pl_MGR.GetComponent<M_PlayerAttack>();
        pl_Anim = pl_MGR.GetComponent<Animator>();
    }

    void Update()
    {
        pl_Anim.SetBool("isMoving", pl_Move.isMoving);
        pl_Anim.SetBool("isAttack", pl_Attack.isAttack);
        pl_Anim.SetInteger("eAttackBranch", (int)pl_Attack.currentAttackBranch);
        pl_Anim.SetBool("isExtendAttack", pl_Attack.isExtendAttack);
        pl_Anim.SetBool("isJumping", pl_Move.isJumping);
        pl_Anim.SetBool("isSecondJump", pl_Move.isSecondJump);

        //player_anim.SetInteger("moveDir", (int)moveDir);
    }
}
