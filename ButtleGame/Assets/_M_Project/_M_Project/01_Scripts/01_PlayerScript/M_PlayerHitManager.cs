using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_PlayerHitManager : MonoBehaviour
{
    private M_CharactorStatus cs;

    void Start()
    {

        cs = GetComponentInParent<M_CharactorStatus>();
    }

    void Update()
    {
        
    }

    /* ���� */
    // ����Ƃ̐ڐG��������
    // Trigger�I���ƃI�t�͐ڐG�o���Ȃ�
    // �v���C���[�̋�̃I�u�W�F�N�g�Ƀq�b�g����p��Trigger�I�u�W�F�N�g�����
    private void OnTriggerEnter(Collider other)
    {
        System_HitBall sh = other.gameObject.GetComponent<System_HitBall>();

        if (sh)
        {
            cs.TakeDamage(sh.atk);
            Debug.Log(cs.GetHitPoint());
        }
    }
}
