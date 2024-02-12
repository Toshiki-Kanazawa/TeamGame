using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// カメラの方向を基準に動作する プレイヤーの移動 の挙動を実装します
//

public class M_PlayerMove : MonoBehaviour
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

    // 自身のコンポーネント
    private Rigidbody rb;

    [Header("基準にするカメラ")]
    public Camera mainCamera;
    public Vector3 n_CameraForward = new Vector3(0.0f, 0.0f, 0.0f);

    [Header("移動系変数")]
    public float moveSpeed = 3.0f;  // 移動速度 → パラメータ用のプログラムに移動
    public float inputHorizontal;   // 横方向の入力
    public float inputVertical;     // 縦方向の入力
    public Vector3 moveForward = new Vector3(0.0f, 0.0f, 0.0f);

    [Header("ジャンプ挙動変数")]
    public float gravityPower = 3.0f;
    public float addVelocity = 9.0f;
    public float jump_y = 0.0f;

    [Header("アニメーション制御変数")]
    public bool isMoving = false;
    public bool isGround = true;
    public bool isJumping = false;
    public bool isSecondJump = false;
    public bool isLockon = false;
    public eMoveDirection eMoveDir;

    /* ---↓ Executes ↓------------------------------------------------------------------------------------- */

    void Start()
    {
        // マネージャーの取得
        pl_MGR = this.gameObject.GetComponent<M_PlayerManager>();

        // メインカメラを取得
        mainCamera = Camera.main;

        // リジッドボディを代入
        rb = this.GetComponent<Rigidbody>();

        // 初期化
        isMoving = false;
        isGround = true;
        isJumping = false;
        isSecondJump = false;
        //isAttack = false;
        isLockon = false;
        eMoveDir = eMoveDirection.None;
    }

    void Update()
    {
        // 地面にいないとき 重力を加算する
        AddGravity();
    }

    private void FixedUpdate()
    {
        // カメラの向きを基準に プレイヤーを移動させる
        PlayerMove_Source_CameraDirection();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 行動権を回復する
        isGround = true;
        isJumping = false;
        isSecondJump = false;

        Debug.Log("何かに衝突しました：行動権を回復しました");
    }

    /* ---↓ Functions ↓------------------------------------------------------------------------------------- */

    public void GamePad_LeftStick_CameraControl(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<Vector2>();
        inputHorizontal = value.x;
        inputVertical = value.y;
    }

    private void PlayerMove_Source_CameraDirection()
    {
        // アニメーション反映用
        isMoving = false;

        // カメラの方向から X-Z平面の単位ベクトルを取得
        n_CameraForward = Vector3.Scale(mainCamera.transform.forward, new Vector3(1.0f, 0.0f, 1.0f)).normalized;

        // 方向入力の入力値とカメラの向きから移動方向を決定
        moveForward = n_CameraForward * inputVertical + mainCamera.transform.right * inputHorizontal;

        // 移動方向にスピードを適応する
        // ジャンプや落下がある場合、別途 Y軸方向のベクトル を足す
        rb.velocity = moveForward * moveSpeed + new Vector3(0.0f, rb.velocity.y, 0.0f);

        // キャラクターの向きを進行方向に向ける
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
            isMoving = true;
        }

        Debug.Log("プレイヤーがカメラ方向を基準に動いています");
    }

    private void Action_Jump()
    {
        if (isSecondJump == true) return;

        // 上方向へのベクトル
        Vector3 jump_vec = Vector3.up;

        // rigidbody に与えるジャンプ量の作成
        jump_y = jump_vec.y * addVelocity;

        // rigidodyに適応
        rb.velocity = new Vector3(0.0f, jump_y, 0.0f);

        // フラグ処理
        if (isJumping == true)
        {
            isSecondJump = true;
            Debug.Log("２段ジャンプしました");
        }
        isJumping = true;
        isGround = false;

        Debug.Log("ジャンプしました");
    }

    public void GamePad_Jump(InputAction.CallbackContext context)
    {
        // ボタンの押す値が閾値を越えた時を判定する
        if (context.phase == InputActionPhase.Started)
        {
            Action_Jump();
        }
    }

    private void AddGravity()
    {
        if (isGround == false)
        {
            rb.AddForce(new Vector3(0.0f, -gravityPower, 0.0f));
            Debug.Log("重力降下しています");
        }
    }
}
