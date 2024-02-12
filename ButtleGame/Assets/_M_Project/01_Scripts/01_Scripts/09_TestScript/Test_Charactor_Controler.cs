using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Test_Charactor_Controler : MonoBehaviour
{
    [Header("表示させるカメラ")]
    public Camera mainCamera;

    // 移動速度
    public float speed = 3.0f;
    public float moveX;
    public float moveZ;
    public float rotSpeed = 3.0f;

    private Rigidbody rb;
    public float gravityPower = 9.8f;
    public float velo = 5.0f;
    public float jump_y = 0.0f;

    //public bool footHit = false; // 足元用のコライダーの判定

    // アニメーション制御
    public enum eMoveDir
    {
        None,
        Forward,
        Back,
        Left,
        Right
    }
    public eMoveDir moveDir;
    public bool isMoving;
    public bool isLockOn = false;  // フリーカメラかロックオンカメラか
    public GameObject LockOnObject; // ロックオンするオブジェクト
    public bool isAttack;

    public bool isJumping;
    public bool isSecondJump;

    // Hit Ball
    [Header("攻撃判定を生成する位置")]
    public GameObject[] attackCollider;
    [Header("自動設定：攻撃判定の座標")]
    public Transform[] attackCollider_Pos;

    [Header("生成するヒット判定プレハブ")]
    public GameObject damageBall;

    // 攻撃判定を一時的に生成する変数
    private GameObject tmp_DmgBall = null;

    // テスト用
    [Header("テスト表示")]
    public Vector3 n_CameraForward = new Vector3(0.0f, 0.0f, 0.0f);

    void Start()
    {
        // リジッドボディの取得
        rb = this.gameObject.GetComponent<Rigidbody>();

        // 攻撃判定のオブジェクトの配列の定義
        attackCollider_Pos = new Transform[attackCollider.Length];

        // 攻撃判定のオブジェクト定義
        for(int i = 0; i < attackCollider.Length; i++)
        {
            attackCollider_Pos[i] = attackCollider[i].transform;
        }
    }
    void Update()
    {
        // ロックモードの設定
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if (isLockOn == true) isLockOn = false;
            else isLockOn = true;
        }
        this.gameObject.GetComponent<Animator>().SetBool("isLockOn", isLockOn);

        // プレイヤーの移動、移動アニメージュの実行
        PlayerMove();

        Gravity();

        this.gameObject.GetComponent<Animator>().SetBool("isJumping", isJumping);
        this.gameObject.GetComponent<Animator>().SetBool("isSecondJump", isSecondJump);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("FootingObject"))
        {
            isJumping = false;
            isSecondJump = false;
        }
    }

    /* Move Function */
    private void PlayerMove()
    {
        // 攻撃中のみスピードを下げる
        speed = isAttack ? 1.0f : 3.0f;

        // プレイヤーの移動：正規化済み
        //moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        //moveZ = Input.GetAxis("Vertical") * speed * Time.deltaTime;




        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        // カメラの方向から X-Z平面の単位ベクトルを取得
        //Vector3 n_CameraForward = Vector3.Scale(mainCamera.transform.forward, new Vector3(1.0f, 0.0f, 1.0f)).normalized;
        n_CameraForward = Vector3.Scale(mainCamera.transform.forward, new Vector3(1.0f, 0.0f, 1.0f)).normalized;

        // 方向入力とカメラの向きから移動方向を決定
        Vector3 moveForward = n_CameraForward * moveZ + mainCamera.transform.right * moveX;

        // 移動方向にスピードを適応する
        rb.velocity = (moveForward * speed + new Vector3(0, jump_y, 0) * Time.deltaTime);

        // キャラクターの向きを進行方向に向ける
        if(moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
            isMoving = true;
        }




        //// 移動方向の作成
        //Vector3 n_moveDir = new Vector3(moveX, 0, moveZ);

        //// アニメーション用フラグの管理
        //isMoving = n_moveDir == Vector3.zero ? false : true;

        // 移動量を座標に適応
        //this.transform.position += n_moveDir;

        // アニメーションの設定
        this.gameObject.GetComponent<Animator>().SetBool("isMoving", isMoving);
        this.gameObject.GetComponent<Animator>().SetBool("isAttack", isAttack);

        if (isLockOn == true)
        {
            moveDir = eMoveDir.None;
            if (moveX > 0) moveDir = eMoveDir.Right;
            if (moveX < 0) moveDir = eMoveDir.Left;
            if (moveZ > 0) moveDir = eMoveDir.Forward;
            if (moveZ < 0) moveDir = eMoveDir.Back;
            this.gameObject.GetComponent<Animator>().SetInteger("moveDir", (int)moveDir);
            // 正面方向がロックオンオブジェクトになるように回転させる
            Transform lockDir = LockOnObject.transform;
            this.transform.LookAt(lockDir);
        }

        // フリーカメラ版の移動
        if (isLockOn == false)
        {
            // 正面方向が進行方向になるように回転させる
            //transform.forward = Vector3.Slerp(transform.forward, n_moveDir, Time.deltaTime * rotSpeed);
        }
    }
    /* Action Function */
    public void GamePad_Jump(InputAction.CallbackContext context)
    {
        // ボタンの押す値が閾値を越えた時を判定する
        if (isSecondJump == true) return;

        if(isJumping == false || isSecondJump == false)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                Vector3 jump_vec = Vector3.up;
                //Vector3 jump_velo = jump_vec * velo;

                 jump_y = jump_vec.y * velo;

                //rb.velocity = jump_velo;
                if (isJumping == true) isSecondJump = true;
                isJumping = true;
                Debug.Log("ジャンプしました");
            }
        }
    }

    private void Gravity()
    {
        if(isJumping == true)
        {
            rb.AddForce(new Vector3(0.0f, -gravityPower, 0.0f));
        }
    }

    /* Attack Function */
    public void GamePad_Attack(InputAction.CallbackContext context)
    {
        // ボタンの押す値が閾値を越えた時を判定する
        if(context.phase == InputActionPhase.Started)
        {
            // 攻撃アニメーション設定：攻撃中
            isAttack = true;
            Debug.Log(context.phase);
            Debug.Log("攻撃しました");
        }
    }
    public void ResetAttack()
    {   // 攻撃状態をリセットする
        isAttack = false;
    }

    /* HitBall Function */
    public void CreateDamageBall()
    {
        // ヒットボールを生成
        tmp_DmgBall = Instantiate(damageBall);

        // ヒットボールに所有者を登録：識別用
        tmp_DmgBall.GetComponent<Test_DamageBall_Controler>().SetCreator(this.gameObject);

        // 当たり判定ボールをヒットPosに追従させる
        tmp_DmgBall.transform.position = attackCollider_Pos[0].transform.position;
        tmp_DmgBall.transform.rotation = attackCollider_Pos[0].transform.rotation;
        tmp_DmgBall.transform.SetParent(attackCollider[0].transform);    // ボールの親を設定(判定追従用)

    }

    public void DeleteDamageBall()
    {   // ヒットボールを削除する
        Destroy(tmp_DmgBall);
        tmp_DmgBall = null;
    }

    /* LegacyFunction */
    //private void LegacyPlayerMove()
    //{
    //    // 動いてない場合の状態を作成
    //    Vector3 n_transform = new Vector3(0.0f, 0.0f, 0.0f);
    //    moveDir = eMoveDir.None;
    //    this.gameObject.GetComponent<Animator>().SetBool("isMoving", false);
    //    this.gameObject.GetComponent<Animator>().SetInteger("moveDir", 0);

    //    // 移動
    //    if (Input.GetKey(KeyCode.W)) // MoveDir : 1
    //    {
    //        n_transform.z = transform.forward.z;
    //        moveDir = eMoveDir.Forward;
    //    }
    //    if (Input.GetKey(KeyCode.S)) // MoveDir : 2
    //    {
    //        n_transform.z = -transform.forward.z;
    //        moveDir = eMoveDir.Back;
    //    }
    //    if (Input.GetKey(KeyCode.A)) // MoveDir : 3
    //    {
    //        n_transform.x = -transform.right.x;
    //        moveDir = eMoveDir.Left;
    //    }
    //    if (Input.GetKey(KeyCode.D)) // MoveDir : 4
    //    {
    //        n_transform.x = transform.right.x;
    //        moveDir = eMoveDir.Right;
    //    }

    //    // 移動量の最終計算
    //    transform.position += n_transform.normalized * speed * Time.fixedDeltaTime;
    //    // モーションへの値代入
    //    isMoving = moveDir == eMoveDir.None ? false : true;
    //    this.gameObject.GetComponent<Animator>().SetBool("isMoving", isMoving);
    //    this.gameObject.GetComponent<Animator>().SetInteger("moveDir", (int)moveDir);

    //}

    //private void InputMoveKey(KeyCode keyCode)
    //{
    //    // 動いてない場合の状態を作成
    //    Vector3 n_transform = new Vector3(0.0f, 0.0f, 0.0f);
    //    moveDir = eMoveDir.None;
    //    this.gameObject.GetComponent<Animator>().SetBool("isMoving", false);
    //    this.gameObject.GetComponent<Animator>().SetInteger("moveDir", 0);

    //    // 旧式
    //    if (keyCode == KeyCode.W) // MoveDir : 1
    //    {
    //        // 移動量を正規化する
    //        n_transform.z = transform.forward.z;

    //        // 移動中のモーションを再生する
    //        this.gameObject.GetComponent<Animator>().SetBool("isMoving", true);
    //        this.gameObject.GetComponent<Animator>().SetInteger("moveDir", 1);
    //    }
    //    if (keyCode == KeyCode.S) // MoveDir : 2
    //    {
    //        n_transform.z = -transform.forward.z;

    //        this.gameObject.GetComponent<Animator>().SetBool("isMoving", true);
    //        this.gameObject.GetComponent<Animator>().SetInteger("moveDir", 2);
    //    }
    //    if (keyCode == KeyCode.A) // MoveDir : 3
    //    {
    //        n_transform.x = -transform.right.x;

    //        this.gameObject.GetComponent<Animator>().SetBool("isMoving", true);
    //        this.gameObject.GetComponent<Animator>().SetInteger("moveDir", 3);
    //    }
    //    if (keyCode == KeyCode.D) // MoveDir : 4
    //    {
    //        n_transform.x = transform.right.x;

    //        this.gameObject.GetComponent<Animator>().SetBool("isMoving", true);
    //        this.gameObject.GetComponent<Animator>().SetInteger("moveDir", 4);
    //    }

    //    // 移動量の最終計算
    //    transform.position += n_transform.normalized * speed * Time.deltaTime;
    //}
}
