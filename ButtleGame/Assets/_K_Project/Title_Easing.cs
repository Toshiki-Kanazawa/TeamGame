using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title_Easing : MonoBehaviour
{
    void Start()
    {
        // ���������_�Ƃ���X�N���[�����W���擾
        float screenX = 0.0f; // X���W�𒆐S�ɂ��邽��0
        float screenY = 0.0f; // Y���W�𒆐S�ɂ��邽��0

        // 3D���W�ɕϊ�
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenX, screenY, Camera.main.nearClipPlane));
    }

    void Update()
    {
        
    }
}
