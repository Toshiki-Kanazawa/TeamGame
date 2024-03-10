using System;
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

    [Header("�o�g���V�[���̃X�J�C�{�b�N�X"), SerializeField]
    private Material skyBox;

    private bool win_f = false;

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

    private void Start()
    {
        // �X�J�C�{�b�N�X�̃}�e���A���̎擾
        skyBox = new Material(RenderSettings.skybox);
        RenderSettings.skybox = skyBox;
    }
    void JudgePlayer()
    {
        if (win_f) return;
        foreach (var s in status)
        {
            if (s.GetHitPoint() <= 0)
            {
                if (s.gameObject.CompareTag("Player_Boy"))
                {
                    WinnerPlayer = enPlayer.Girl;
                }
                else if (s.gameObject.CompareTag("Player_Girl"))
                {
                    WinnerPlayer = enPlayer.Boy;
                }
                StartCoroutine(Coroutine());
                win_f = true;
            }
        }
    }
    private IEnumerator Coroutine()
    {
        yield return SceneController.Instance.ChangeScene("Result", 0.02f * Time.deltaTime);
    }

}
