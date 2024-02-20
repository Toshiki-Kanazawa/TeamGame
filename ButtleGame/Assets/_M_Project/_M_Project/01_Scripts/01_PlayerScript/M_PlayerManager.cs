using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_PlayerManager : MonoBehaviour
{
    [Header("���L����J�����F�����ݒ�")]
    [SerializeField]
    public GameObject cam;
    [Header("�J�����v���t�@�u")]
    public GameObject cam_prefab;

    [Header("����N���X")]
    [SerializeField]
    private M_PlayerMove pl_Move;
    [SerializeField]
    private M_PlayerAttack pl_Attack;
    [SerializeField]
    private PlayerAnimationComparator pl_AnimCompo;
    [SerializeField]
    private M_CharactorStatus pl_Status;
    //[SerializeField]
    //private M_HSV_Controler hsv;

    [Header("�\���p�[�c")]
    [SerializeField]
    private GameObject[] pl_Parts;
    [SerializeField]
    private Transform[] pl_Parts_Pos;


    void Start()
    {
        // �J�����𐶐�����
        //cam = Instantiate(cam_prefab);
        //cam.GetComponent<SeekCamera>().SetParent(this.gameObject);

        // ����N���X�̃R���|�[�l���g�擾
        pl_Move = GetComponent<M_PlayerMove>();
        pl_Attack = GetComponent<M_PlayerAttack>();
        pl_AnimCompo = GetComponent<PlayerAnimationComparator>();
        pl_Status = GetComponent<M_CharactorStatus>();
        //hsv = GetComponent<M_HSV_Controler>();

        // �J������ݒ�
        //pl_Move.SetPlayerCamera(cam.GetComponent<Camera>());

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


    public GameObject[] GetParts()
    {
        return pl_Parts;
    }
}
