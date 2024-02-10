using System;
using UnityEngine;
using UnityEngine.InputSystem; // InputSystemを使う為に必要

// 自身にRigitBodyがないとき自動で追加される属性
[RequireComponent(typeof(Rigidbody))]

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float jumpForce; // ジャンプ力

    private Rigidbody rb;

    private Inputs inputs; // InputSystem

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // ① Actionスクリプトのインスタンス生成
        inputs = new Inputs();

        // ② Input Actionを機能させるために有効化させる
        inputs.Enable();
    }

    private void OnDestroy()
    {
        // ③ リソースの解放
        inputs?.Dispose();
    }

    private void Update()
    {
        if(inputs.Player.Jump.triggered)
        {
            Debug.Log("Jump");
            rb.velocity += new Vector3(0, jumpForce,0);
        }
    }
}