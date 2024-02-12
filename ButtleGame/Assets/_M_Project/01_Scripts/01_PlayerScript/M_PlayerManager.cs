using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_PlayerManager : MonoBehaviour
{
    [Header("����N���X")]
    [SerializeField]
    private M_PlayerMove pl_Move;
    [SerializeField]
    private M_PlayerAttack pl_Attack;
    [SerializeField]
    private PlayerAnimationComparator pl_AnimCompo;

    [Header("�\���p�[�c")]
    [SerializeField]
    private GameObject[] pl_Parts;
    [SerializeField]
    private Transform[] pl_Parts_Pos;


    void Start()
    {
        // ����N���X�̃R���|�[�l���g�擾
        pl_Move = this.gameObject.GetComponent<M_PlayerMove>();
        pl_Attack = this.gameObject.GetComponent<M_PlayerAttack>();
        pl_AnimCompo = this.gameObject.GetComponent<PlayerAnimationComparator>();

        // �\���p�[�c�̃Q�[���I�u�W�F�N�g�̎擾
        // ExTag �̃R���|�[�l���g�����q�I�u�W�F�N�g����������
        ExTag_PlayerParts[] tmp = GetComponentsInChildren<ExTag_PlayerParts>();
        int count = tmp.Length;

        // ExTag�̐��Ŕz�񐔂Ń������ɍڂ���
        pl_Parts = new GameObject[count];
        pl_Parts_Pos = new Transform[count];

        for (int i = 0; i < count - 1; i++)
        {
            // ExTag �����I�u�W�F�N�g����
            pl_Parts[i] = tmp[i].gameObject;

            // ���W�����R�s�[
            pl_Parts_Pos[i] = pl_Parts[i].transform;
        }
    }

    void Update()
    {

    }
}
