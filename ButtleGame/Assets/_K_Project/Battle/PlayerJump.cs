using System;
using UnityEngine;
using UnityEngine.InputSystem; // InputSystem���g���ׂɕK�v

// ���g��RigitBody���Ȃ��Ƃ������Œǉ�����鑮��
[RequireComponent(typeof(Rigidbody))]

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float jumpForce; // �W�����v��

    private Rigidbody rb;

    private Inputs inputs; // InputSystem

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // �@ Action�X�N���v�g�̃C���X�^���X����
        inputs = new Inputs();

        // �A Input Action���@�\�����邽�߂ɗL����������
        inputs.Enable();
    }

    private void OnDestroy()
    {
        // �B ���\�[�X�̉��
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