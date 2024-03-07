using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タイトルシーンのステートの切り換えを行う
/// </summary>
public class TitleStateManager : MonoBehaviour
{
    [SerializeField]
    private SceneTransition transition; // シーントランジション

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

    // 各ステート
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
    
    [SerializeField] // 後で消す＿＿＿＿＿＿＿＿＿＿
    private TitleState nowState; // 現在のステート

    void Start()
    {
        transition.GetComponent<SceneTransition>();
    }

    void Update()
    {
        // 各ステートのUpdate処理
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
        //1 OnExit （現在状態から出る処理）
        // 現在のステートの削除等を行う
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


        //2 OnEnter（新しい状態に入る処理）
        // 次のステートの生成等を行う
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

        //3 代入　（現在状態を更新する）
        nowState = nextState;
    }
}