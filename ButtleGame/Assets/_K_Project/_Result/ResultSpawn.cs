using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム内の全てのキャラクターを参照して
/// 勝者のキャラクターだけ出現させる
/// </summary>
public class ResultSpawn : MonoBehaviour
{
    [SerializeField,Tooltip("ゲーム内の全てのキャラクターを参照する" +
                            "0 に 男、1 に 女 のキャラクターにしてください")]
    private GameObject[] player;

    void Start()
    {
        var winner = Judge.WinnerPlayer;
        switch (winner)
        {
            case Judge.enPlayer.Boy:
                Instantiate(player[0],transform.position,transform.rotation);
                break;
            case Judge.enPlayer.Girl:
                Instantiate(player[1], transform.position, transform.rotation);
                break;
            default:
                Debug.Log("例外処理");
                break;
        }
    }

    //void Update()
    //{
        
    //}
}
