using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_PlayerHitManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* ���� */
    // ����Ƃ̐ڐG��������
    // Trigger�I���ƃI�t�͐ڐG�o���Ȃ�
    // �v���C���[�̋�̃I�u�W�F�N�g�Ƀq�b�g����p��Trigger�I�u�W�F�N�g�����
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<System_HitBall>())
        {
            Debug.Log("lllllllllllllllllllllllllllllllllllllllll");
            this.gameObject.SetActive(false);
        }
    }
}
