using System;
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

    [Header("バトルシーンのスカイボックス"), SerializeField]
    private Material skyBox;

    private bool win_f = false;

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

    private void Start()
    {
        // スカイボックスのマテリアルの取得
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
