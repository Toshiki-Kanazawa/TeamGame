using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ���ꂪ������������������
/// </summary>
public class Judge : MonoBehaviour
{
    // �v���C���[�̎��
    public enum enPlayer
    {
        Boy,  // �j
        Girl, // ��
        Pl_Max, // ��O
    }

    static byte winner = 255;

    [SerializeField] private M_CharactorStatus[] status;

    void Update()
    {
        JudgePlayer();
    }

    void JudgePlayer()
    {
        for(int i = 0; i < (int)enPlayer.Pl_Max; i++)
        {
            if(status[i].GetHitPoint() <= 0)
            {
                if (status[i].gameObject.CompareTag("Player_Boy"))
                {
                    setWinnerPlayer(enPlayer.Girl);
                    SceneManager.LoadScene("Result");
                    
                }
                else if (status[i].gameObject.CompareTag("Player_Girl"))
                {
                    setWinnerPlayer(enPlayer.Boy);
                    SceneManager.LoadScene("Result");
                }
            }
        }
    }

    public void setWinnerPlayer(enPlayer player)
    {
        winner = (byte)player;
    }

    static public enPlayer GetWinnerPlayer()
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
}
