using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �^�C�g���V�[���̃X�e�[�g�̐؂芷�����s��
/// </summary>
public class TitleStateManager : MonoBehaviour
{
    [SerializeField]
    private SceneTransition transition; // �V�[���g�����W�V����

    //[SerializeField]
    //private Option option;

    [SerializeField,Tooltip("0 : Main , 1 : Option , 2 : Credit")]
    private CanvasGroup[] canvasGroup;

    public enum canvasGroupType
    {
        Main,
        Option,
        Credit,
    }

    // �e�X�e�[�g
    public enum TitleState
    {
        Title_Init_FO,
        Title_Anim,

        Title_Main,
        Title_Main_FI,
        Title_Main_FO,

        Title_Option,
        Title_Option_FI,
        Title_Option_FO,

        Title_Credit,
        Title_Credit_FI,
        Title_Credit_FO,
    }
    
    [SerializeField] // ��ŏ����Q�Q�Q�Q�Q�Q�Q�Q�Q�Q
    private TitleState nowState; // ���݂̃X�e�[�g

    void Start()
    {
        transition.GetComponent<SceneTransition>();
    }

    void Update()
    {
        // �e�X�e�[�g��Update����
        switch (nowState)
        {
            case TitleState.Title_Init_FO:
                break;
            case TitleState.Title_Anim:
                break;
            case TitleState.Title_Main:
                break;
            case TitleState.Title_Main_FI:
                break;
            case TitleState.Title_Main_FO:
                break;
            case TitleState.Title_Option:
                //option.StateUpdate();
                break;
            case TitleState.Title_Option_FI:
                break;
            case TitleState.Title_Option_FO:
                break;
            case TitleState.Title_Credit:
                break;
            case TitleState.Title_Credit_FI:
                break;
            case TitleState.Title_Credit_FO:
                break;
        }
    }

    public void TitleChangeState(TitleState nextState)
    {
        //1 OnExit �i���ݏ�Ԃ���o�鏈���j
        // ���݂̃X�e�[�g�̍폜�����s��
        switch (nowState)
        {
            case TitleState.Title_Init_FO:
                break;
            case TitleState.Title_Anim:
                break;
            case TitleState.Title_Main:
                break;
            case TitleState.Title_Main_FI:
                break;
            case TitleState.Title_Main_FO:
                canvasGroup[(int)canvasGroupType.Main].blocksRaycasts = false;
                canvasGroup[(int)canvasGroupType.Main].interactable = false;
                break;
            case TitleState.Title_Option:
                break;
            case TitleState.Title_Option_FI:
                break;
            case TitleState.Title_Option_FO:
                canvasGroup[(int)canvasGroupType.Option].blocksRaycasts = false;
                canvasGroup[(int)canvasGroupType.Option].interactable = false;
                break;
            case TitleState.Title_Credit:
                break;
            case TitleState.Title_Credit_FI:
                break;
            case TitleState.Title_Credit_FO:
                canvasGroup[(int)canvasGroupType.Credit].blocksRaycasts = false;
                canvasGroup[(int)canvasGroupType.Credit].interactable = false;
                break;
        }


        //2 OnEnter�i�V������Ԃɓ��鏈���j
        // ���̃X�e�[�g�̐��������s��
        switch (nextState)
        {
            case TitleState.Title_Init_FO:
                break;
            case TitleState.Title_Anim:
                break;
            case TitleState.Title_Main:
                break;
            case TitleState.Title_Main_FI:
                canvasGroup[(int)canvasGroupType.Main].blocksRaycasts = true;
                canvasGroup[(int)canvasGroupType.Main].interactable = true;
                break;
            case TitleState.Title_Main_FO:
                break;
            case TitleState.Title_Option:
                break;
            case TitleState.Title_Option_FI:
                canvasGroup[(int)canvasGroupType.Option].blocksRaycasts = true;
                canvasGroup[(int)canvasGroupType.Option].interactable = true;
                break;
            case TitleState.Title_Option_FO:
                break;
            case TitleState.Title_Credit:
                break;
            case TitleState.Title_Credit_FI:
                canvasGroup[(int)canvasGroupType.Credit].blocksRaycasts = true;
                canvasGroup[(int)canvasGroupType.Credit].interactable = true;
                break;
            case TitleState.Title_Credit_FO:
                break;
        }

        //3 ����@�i���ݏ�Ԃ��X�V����j
        nowState = nextState;
    }
}