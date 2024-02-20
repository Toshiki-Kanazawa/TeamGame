using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // InputSystemを使うために必要

/// <summary>
/// プレイヤーの移動、ジャンプ
/// </summary>

// 自身にRigitBodyがないとき自動で追加される属性
[RequireComponent(typeof(Rigidbody))]
public class _PlayerMove : MonoBehaviour
{
    public float moveSpeed;
    public float rotSpeed;
    public float jumpForce;

    private Rigidbody rb;
    private Inputs inputs;

    void Start()
    {
        rb = this.transform.GetComponent<Rigidbody>();

        // ① Actionスクリプトのインスタンス生成
        inputs = new Inputs();

        // ② Input Actionを機能させるために有効化させる
        inputs.Enable();
    }

    void OnDestroy()
    {
        // ③ リソースの解放
        inputs?.Dispose();
    }

    void Update()
    {
        Move();

        Jump();
    }

    private void Move()
    {
        float vertical = inputs.Player.Move.ReadValue<Vector2>().y;
        float horizontal = inputs.Player.Move.ReadValue<Vector2>().x;
        //回転
        if (horizontal > 0)
            transform.Rotate(0, rotSpeed * Time.deltaTime, 0);
        if (horizontal < 0)
            transform.Rotate(0, (-1) * rotSpeed * Time.deltaTime, 0);

        //移動ベクトル
        Vector3 moveVec = transform.forward * vertical * moveSpeed * Time.deltaTime;
        //移動
        rb.velocity = moveVec;
    }


    private void Jump()
    {
        if (!inputs.Player.Jump.triggered) return; 

            Debug.Log("Jump");
            rb.velocity += new Vector3(0, jumpForce, 0);
    }
}
