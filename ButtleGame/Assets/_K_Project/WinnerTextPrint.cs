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
        if(Judge.GetWinnerPlayer() == Judge.enPlayer.Boy)
        {
            text.text = "�j�̏���";
        }
        else if (Judge.GetWinnerPlayer() == Judge.enPlayer.Girl)
        {
            text.text = "���̏���";
        }
        else
        {
            text.text = "��O����";
        }
    }
}
