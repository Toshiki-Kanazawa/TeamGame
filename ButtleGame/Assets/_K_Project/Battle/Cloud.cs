using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float amplitude = 1.0f; // �U��
    public float frequency = 1.0f; // ���g��

    private float initY; // ����Y���W

    void Start()
    {
        initY = transform.position.y; // ����Y���W���L�^
    }

    void Update()
    {
        // �I�u�W�F�N�g���㉺�ɓ�����
        float posY = initY + amplitude * Mathf.Sin(Time.time * frequency);
        transform.position = new Vector3(transform.position.x, posY, transform.position.z);
    }
}
