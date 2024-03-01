using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// だれが勝利したかを教える
/// マネージャーに持たせる予定
/// </summary>
public class Judge : MonoBehaviour
{
    // インスペクタで設定できる変数
    [SerializeField] string nextScene = "Result";

    [Header("プレイヤーの種類")]
    [SerializeField] private List<M_CharactorStatus> status = new List<M_CharactorStatus>();

    // プレイヤーの種類
    public enum enPlayer
    {
        Boy,  // 男
        Girl, // 女
        Pl_Max, // 例外
    }

    // 勝者の取得、設定
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
