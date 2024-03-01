using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Q�[�����̑S�ẴL�����N�^�[���Q�Ƃ���
/// ���҂̃L�����N�^�[�����o��������
/// </summary>
public class ResultSpawn : MonoBehaviour
{
    [SerializeField,Tooltip("�Q�[�����̑S�ẴL�����N�^�[���Q�Ƃ���" +
                            "0 �� �j�A1 �� �� �̃L�����N�^�[�ɂ��Ă�������")]
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
                Debug.Log("��O����");
                break;
        }
    }

    //void Update()
    //{
        
    //}
}
