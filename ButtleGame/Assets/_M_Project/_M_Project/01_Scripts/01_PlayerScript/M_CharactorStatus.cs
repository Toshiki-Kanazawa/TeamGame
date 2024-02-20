using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* プレイヤーの状態や参照される為の数値を格納するクラス */
/* 敵キャラ等でも流用できるよう設計 */

public class M_CharactorStatus : MonoBehaviour
{
    [Header("親オブジェクトのマネージャー")]
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

    [Header("キャラクターのパラメータ")]
    [SerializeField]
    private const int hitPoint_Max = 100;   // 最大体力
    [SerializeField]
    private int hitPoint = hitPoint_Max;     // 体力
    [SerializeField]
    private const float stamina_Max = 100.0f;   // 最大行動力
    [SerializeField]
    private float stamina = stamina_Max; // 行動力
    [SerializeField]
    private int atk = 10;           // 攻撃力
    [SerializeField]
    private float spd = 3.0f;       // 移動速度

    [Header("状態変化")]
    [SerializeField]
    private bool invincible = false;       // 無敵状態
    [SerializeField]
    private float stunTime_Attack = 0.0f;  // 攻撃硬直
    [SerializeField]
    private float stunTime_Damage = 0.0f;  // 被ダメ時の硬直

    [Header("アニメーション制御変数")]
    [SerializeField]
    private bool isMoving = false;   // 移動中
    [SerializeField]
    private bool isGround = false;    // 地面と接触している
    [SerializeField]
    private bool isJumping = false;  // ジャンプ中
    [SerializeField]
    private bool isSecondJump = false; // ２段ジャンプ中
    [SerializeField]
    private bool isAttack = false;   // 攻撃中
    [SerializeField]
    private bool isLockon = false;   // ロックオンしている
    [SerializeField]
    private eMoveDirection eMoveDir; // 移動方向


    void Start()
    {
        hitPoint = hitPoint_Max;
        stamina = stamina_Max;

        pl_MGR = GetComponent<M_PlayerManager>();
        // このスクリプトの全ての値を初期化
        ResetIsAllValues();
    }

    void Update()
    {
        
    }

    public void TakeDamage(int dmg)
    {
        hitPoint -= dmg;

        if (hitPoint <= 0) Debug.Log("なぜ生きているのだ…？？");
    }

    public int GetHitPoint()
    {
        return hitPoint;
    }

    public int GetHitPointMax()
    {
        return hitPoint_Max;
    }

    public float GetStamina()
    {
        return stamina;
    }

    public float GetStaminaMax()
    {
        return stamina_Max;
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
        // パラメータ
        hitPoint = 100;
        stamina = 100.0f;
        atk = 10;
        spd = 3.0f;

        // 状態変化
        invincible = false;
        stunTime_Attack = 0.0f;
        stunTime_Damage = 0.0f;

        // アニメーション制御変数
        isMoving = false;
        isGround = false;
        isJumping = false;
        isSecondJump = false;
        isAttack = false;
        isLockon = false;
        eMoveDir = eMoveDirection.None;
    }
}
