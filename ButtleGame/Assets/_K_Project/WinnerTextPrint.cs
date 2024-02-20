using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinnerTextPrint : MonoBehaviour
{
    public Text text;

    // Judgeクラスから勝者を取得してTextで反映する
    void Start()
    {
        if(Judge.GetWinnerPlayer() == Judge.enPlayer.Boy)
        {
            text.text = "男の勝ち";
        }
        else if (Judge.GetWinnerPlayer() == Judge.enPlayer.Girl)
        {
            text.text = "女の勝ち";
        }
        else
        {
            text.text = "例外処理";
        }
    }
}
