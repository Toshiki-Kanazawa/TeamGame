using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class M_PlayerAttack : MonoBehaviour
{
    [Header("親オブジェクトのマネージャー")]
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

    // 自身のコンポーネント
    private M_CharactorStatus status;

    [Header("制御変数")]
    public bool isExtendAttack = false;
    public eAttackBranch currentAttackBranch = eAttackBranch.None;
    public eAttackBranch prevAttackBranch = eAttackBranch.None;



    void Start()
    {
        // マネージャーの取得
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

        // 攻撃アニメーションの再生
        status.SetIsAttack(true);

        /* 調整中：３段攻撃 */
        //// 攻撃の分岐
        //if (currentAttackBranch == eAttackBranch.None)
        //{
        //    currentAttackBranch = eAttackBranch.N_Attack_1;
        //}
        //// 攻撃１モーション中に入力された場合
        //else if (currentAttackBranch == eAttackBranch.N_Attack_1)
        //{
        //    //isExtendAttack = true;
        //    currentAttackBranch = eAttackBranch.N_Attack_2;
        //}
        //// 攻撃２モーション中に入力された場合
        //else if (currentAttackBranch == eAttackBranch.N_Attack_2)
        //{
        //    isExtendAttack = true;
        //    currentAttackBranch = eAttackBranch.N_Attack_3;
        //}

        Debug.Log("攻撃しました");

        /* 攻撃判定の作成はアニメーションで関数を呼んでいます */
        /* アニメーション終了時にリセット関数を呼んでいます */
    }

    public void GamePad_Attack(InputAction.CallbackContext context)
    {
        // ボタンの押す値が閾値を越えた時を判定する
        if (context.phase == InputActionPhase.Started)
        {
            Action_Attack();
        }
    }

    public void ResetAttack()
    {
        // 派生攻撃中はリセットしない
        if (isExtendAttack == true) return;

        // 攻撃状態をリセットする
        status.SetIsAttack(false);
        currentAttackBranch = eAttackBranch.None;

        Debug.Log("Execute：ResetAttack");
    }

    public void ResetExtendAttack()
    {
        isExtendAttack = false;

        // 攻撃状態をリセットする
        status.SetIsAttack(false);
        currentAttackBranch = eAttackBranch.None;

        Debug.Log("Execute：ResetExtendAttack");
    }

}
