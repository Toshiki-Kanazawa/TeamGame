using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinnerTextPrint : MonoBehaviour
{
    public Text text;

    // Judge�N���X���珟�҂��擾����Text�Ŕ��f����
    void Start()
    {
        var winner = Judge.WinnerPlayer;

        if(winner == Judge.enPlayer.Boy)
        {
            text.text = "�j�̏���";
        }
        else if (winner == Judge.enPlayer.Girl)
        {
            text.text = "���̏���";
        }
        else
        {
            text.text = "��O����";
        }
    }
}
