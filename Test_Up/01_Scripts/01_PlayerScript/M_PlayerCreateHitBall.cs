using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_PlayerCreateHitBall : MonoBehaviour
{
    [Header("�e�I�u�W�F�N�g�̃}�l�[�W���[")]
    [SerializeField]
    private M_PlayerManager pl_MGR;

    [Header("�����ݒ�F�o��������ꏊ�FExTag_HitBallCreator")]
    [SerializeField]
    private GameObject[] HitBall_Creators;
    [SerializeField]
    private Transform[] HitBall_CreatePos;

    [Header("�U������̃v���n�u")]
    public GameObject HitBall_Prefab;
    private GameObject tmp_HitBall;


    void Start()
    {
        // �}�l�[�W���[�̎擾
        pl_MGR = this.gameObject.GetComponent<M_PlayerManager>();

        // ExTag �̃R���|�[�l���g�����q�I�u�W�F�N�g����������
        ExTag_HitBallCreator[] tmp = GetComponentsInChildren<ExTag_HitBallCreator>();
        int count = tmp.Length;

        // ExTag�̐��Ŕz�񐔂Ń������ɍڂ���
        HitBall_Creators = new GameObject[count];
        HitBall_CreatePos = new Transform[count];

        for (int i = 0; i < count; i++)
        {
            // ExTag �����I�u�W�F�N�g����
            HitBall_Creators[i] = tmp[i].gameObject;

            // ���W�����R�s�[
            HitBall_CreatePos[i] = HitBall_Creators[i].transform;
        }
    }

    void Update()
    {
        
    }

    public void CreateHitBall()
    {
        // �q�b�g������쐬
        tmp_HitBall = null;
        tmp_HitBall = Instantiate(HitBall_Prefab);

        // �q�b�g����̏��L�҂�o�^�F���ʗp
        tmp_HitBall.GetComponent<System_HitBall>().SetCreator(this.gameObject);

        // �����蔻��{�[�����q�b�gPos�ɒǏ]������
        tmp_HitBall.transform.position = HitBall_CreatePos[0].transform.position;
        tmp_HitBall.transform.rotation = HitBall_CreatePos[0].transform.rotation;
        tmp_HitBall.transform.SetParent(HitBall_Creators[0].transform);    // �{�[���̐e��ݒ�(����Ǐ]�p)
    }

    public void DeleteHitBall()
    {
        // �q�b�g������폜����
        Destroy(tmp_HitBall);
        tmp_HitBall = null;     // �N���A���Ă���
    }

}
