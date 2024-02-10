using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove_CameraDir : MonoBehaviour
{
    public Camera mainCamera;

    public float inputHorizontal;
    public float inputVertical;

    public Vector3 n_CameraForward = new Vector3(0.0f, 0.0f, 0.0f);

    public float speed = 3.0f;

    private Rigidbody rb;

    private Inputs input;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();

        input = new Inputs();

        input.Enable();
    }

    private void OnDestroy()
    {
        input?.Dispose();
    }

    // Update is called once per frame
    void Update()
    {
        inputHorizontal = input.Player.Look.ReadValue<Vector2>().x;
        inputVertical = input.Player.Look.ReadValue<Vector2>().y;

        // �J�����̕������� X-Z���ʂ̒P�ʃx�N�g�����擾
        //Vector3 n_CameraForward = Vector3.Scale(mainCamera.transform.forward, new Vector3(1.0f, 0.0f, 1.0f)).normalized;
        n_CameraForward = Vector3.Scale(mainCamera.transform.forward, new Vector3(1.0f, 0.0f, 1.0f)).normalized;

        // �������͂ƃJ�����̌�������ړ�����������
        Vector3 moveForward = n_CameraForward * inputVertical + mainCamera.transform.right * inputHorizontal;

        // �ړ������ɃX�s�[�h��K������
        rb.velocity = (moveForward * speed + new Vector3(inputHorizontal, rb.velocity.y, inputVertical) * Time.deltaTime);

        // �L�����N�^�[�̌�����i�s�����Ɍ�����
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }

    }
}
