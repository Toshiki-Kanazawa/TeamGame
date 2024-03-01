using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ���ꂪ������������������
/// �}�l�[�W���[�Ɏ�������\��
/// </summary>
public class Judge : MonoBehaviour
{
    // �C���X�y�N�^�Őݒ�ł���ϐ�
    [SerializeField] string nextScene = "Result";

    [Header("�v���C���[�̎��")]
    [SerializeField] private List<M_CharactorStatus> status = new List<M_CharactorStatus>();

    // �v���C���[�̎��
    public enum enPlayer
    {
        Boy,  // �j
        Girl, // ��
        Pl_Max, // ��O
    }

    // ���҂̎擾�A�ݒ�
    static public enPlayer WinnerPlayer
    {
        get
        {
            switch (winner)
            {
                case (byte)enPlayer.Boy:
                    return enPlayer.Boy;

                case (byte)enPlayer.Girl:
                    return enPlayer.Girl;

                default:
                    return enPlayer.Pl_Max;
            }
        }
        set
        {
            winner = (byte)value;
        }
    }

    static byte winner = 255;

    void Update()
    {
        JudgePlayer();
    }

    void JudgePlayer()
    {
        foreach (var playerStatus in status)
        {
            if (playerStatus.GetHitPoint() <= 0)
            {
                if (playerStatus.gameObject.CompareTag("Player_Boy"))
                {
                    WinnerPlayer = enPlayer.Girl;
                }
                else if (playerStatus.gameObject.CompareTag("Player_Girl"))
                {
                    WinnerPlayer = enPlayer.Boy;
                }
                SceneManager.LoadScene(nextScene);
            }
        }
    }


}
