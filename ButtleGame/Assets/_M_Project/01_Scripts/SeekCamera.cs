using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SeekCamera : MonoBehaviour
{
    public enum eCameraMode
    {
        GameMode,
        LockDown,
    }

    [Header("�Ǐ]����I�u�W�F�N�g")]
    public GameObject target;
    private Transform target_transform;
    public Vector3 prevTargetPos = new Vector3(0.0f, 0.0f, 0.0f);

    [Header("�J�����̈ʒu��Ǐ]�̎d��")]
    public eCameraMode camMode = eCameraMode.GameMode;

    [Header("�e���[�h�ɂ�����I�t�Z�b�g�l")]
    public Vector3 offset_LockDown = new Vector3(0.0f, 7.5f, -1.5f);
    public Vector3 offset_BackView = new Vector3(0.0f, 1.0f, -10.0f);
    public float seekSpeed = 2.0f;  // �Ǐ]���x
    public float cameraSensity = 100.0f; // ���x

    [Header("R�X�e�B�b�N�ŃJ�����𓮂����ׂ̕ϐ�")]
    public bool input_Rstick;
    public float inputHorizontal;
    public float inputVertical;

    [Header("�J�����̏c�����̊p�x���E")]
    public float maxAngle = 70.0f;
    public float minAngle = -10.0f;
    private Vector3 totalAngle = Vector3.zero;

void Start()
    {
        // �����ʒu�̐ݒ�
        this.transform.position = new Vector3(0.0f, 1.5f, -5.0f);

        // �Ǐ]�Ώۂ̍��W�����擾����
        target_transform = target.transform;
        // �O�̃t���[���ł̍��W�ƂȂ�悤�ɍ��W��ۑ����Ă���
        prevTargetPos = target.transform.position;

        // �E�X�e�B�b�N�̓��͏�����
        input_Rstick = false;
    }

    void Update()
    {
        // �v���C���[�̎��͂���]�ł���悤�ȕW���I�ȃJ����
        if (camMode == eCameraMode.GameMode)
        {
            this.transform.position += target.transform.position - prevTargetPos;
            prevTargetPos = target.transform.position;

            // �J�����̉�]�p�x���������A�E�X�e�B�b�N�̓��͂𔽉f����
            var newAngle = Vector3.zero;
            newAngle.x = inputHorizontal * Time.deltaTime * cameraSensity;
            newAngle.y = (inputVertical * -1) * Time.deltaTime * cameraSensity;

            // ��]�l��K�����鎞�ɁA�����p�x���z���Ă����ꍇ�ɋ�������
            if (maxAngle < totalAngle.y + newAngle.y)
            {
                newAngle.y = maxAngle - totalAngle.y;
            }
            if (totalAngle.y + newAngle.y < minAngle)
            {
                newAngle.y = minAngle - totalAngle.y;
            }

            // ���݂̃J�����p�x�ɁA���̃t���[���ňړ�������]�l�𑫂�����
            totalAngle.y += newAngle.y;

            // �^�[�Q�b�g�̈ʒu��Y���W�𒆐S�ɉ�]����
            transform.RotateAround(prevTargetPos, Vector3.up, newAngle.x);
            transform.RotateAround(prevTargetPos, transform.right, newAngle.y);
        }
        // DS�[���_�݂����ȏォ�猩�����_�̃J����
        if (camMode == eCameraMode.LockDown) // �����낵
        {
            // �v���C���[�̍��W�ɂ��Ă���
            this.gameObject.transform.position = target_transform.position + offset_LockDown;
            // �v���C���[�̕���������
            this.transform.LookAt(target_transform);
        }
    }

    public void GamePad_RightStick_CameraControl(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<Vector2>();
        inputHorizontal = value.x;
        inputVertical = value.y;
    }
}
