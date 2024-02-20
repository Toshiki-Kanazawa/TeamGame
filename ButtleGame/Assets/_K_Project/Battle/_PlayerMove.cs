using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // InputSystem���g�����߂ɕK�v

/// <summary>
/// �v���C���[�̈ړ��A�W�����v
/// </summary>

// ���g��RigitBody���Ȃ��Ƃ������Œǉ�����鑮��
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

        // �@ Action�X�N���v�g�̃C���X�^���X����
        inputs = new Inputs();

        // �A Input Action���@�\�����邽�߂ɗL����������
        inputs.Enable();
    }

    void OnDestroy()
    {
        // �B ���\�[�X�̉��
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
        //��]
        if (horizontal > 0)
            transform.Rotate(0, rotSpeed * Time.deltaTime, 0);
        if (horizontal < 0)
            transform.Rotate(0, (-1) * rotSpeed * Time.deltaTime, 0);

        //�ړ��x�N�g��
        Vector3 moveVec = transform.forward * vertical * moveSpeed * Time.deltaTime;
        //�ړ�
        rb.velocity = moveVec;
    }


    private void Jump()
    {
        if (!inputs.Player.Jump.triggered) return; 

            Debug.Log("Jump");
            rb.velocity += new Vector3(0, jumpForce, 0);
    }
}
