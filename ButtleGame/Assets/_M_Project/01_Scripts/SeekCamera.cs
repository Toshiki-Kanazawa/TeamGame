using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekCamera : MonoBehaviour
{
    [Header("�Ǐ]����I�u�W�F�N�g")]
    public GameObject target;
    private Transform target_transform;
    public Vector3 prevTargetPos = new Vector3(0.0f, 0.0f, 0.0f);

    public enum eCameraMode
    {
        GameMode,
        LockDown,
        BackView,
    }
    public eCameraMode camMode = eCameraMode.GameMode;

    public Vector3 offset_LockDown = new Vector3(0.0f, 7.5f, -1.5f);
    public Vector3 offset_BackView = new Vector3(0.0f, 1.0f, -10.0f);
    public float seekSpeed = 2.0f;  // �Ǐ]���x

    //public Vector2 cameraMax = new Vector2(5, 5);
    //public Vector2 cameraMin = new Vector2(-5, -5);

void Start()
    {
        target_transform = target.transform;
        prevTargetPos = target.transform.position;
    }

    void Update()
    {

        if(camMode == eCameraMode.GameMode) // ��ʓI�ȃ��c
        {
            this.transform.position += target.transform.position - prevTargetPos;
            prevTargetPos = target.transform.position;

            if(Input.GetMouseButton(1))
            {
                // �}�E�X�̈ړ���
                float mouseInputX = Input.GetAxis("Mouse X");
                float mouseInputY = Input.GetAxis("Mouse Y");

                // �^�[�Q�b�g�̈ʒu��Y���W�𒆐S�ɉ�]����
                transform.RotateAround(prevTargetPos, Vector3.up, mouseInputX * Time.deltaTime * 200.0f);
            }
        }
        if(camMode == eCameraMode.LockDown) // �����낵
        {
            this.gameObject.transform.position = target_transform.position + offset_LockDown;
            // �v���C���[�̕���������
            this.transform.LookAt(target_transform);

        }
        if (camMode == eCameraMode.BackView) // �w�ォ��
        {
            this.transform.position = Vector3.Lerp(this.transform.position,
                                                    target_transform.position + offset_BackView,
                                                    2.0f * Time.deltaTime
                                                    );
            // �v���C���[�̕���������
            this.transform.LookAt(target_transform);
        }

        // �J�����Ǐ]�𐧌�����
        //this.transform.position = new Vector3(Mathf.Clamp(target_transform.position.x, cameraMin.x, cameraMax.x), Mathf.Clamp(target_transform.position., cameraMin.x, cameraMax.x))


        // �Ǐ]����ʒu�ɃJ���������𑫂�����
        //offset_player = new Vector3(0.0f, 0.0f, (target.GetComponent<Test_Charactor_Controler>().moveZ * 100.0f));
        //this.gameObject.transform.position = (target_transform.position + offset_LockDown) + offset_player;

        //this.transform.Rotate(new Vector3(65.0f, 0.0f, 0.0f));
        //this.transform.rotation = new Quaternion(65.0f, 0.0f, 0.0f, 1.0f);

        // �v���C���[�̕���������
        //this.transform.LookAt(target_transform);
    }
}