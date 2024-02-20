using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// だれが勝利したかを教える
/// </summary>
public class Judge : MonoBehaviour
{
    // プレイヤーの種類
    public enum enPlayer
    {
        Boy,  // 男
        Girl, // 女
        Pl_Max, // 例外
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
