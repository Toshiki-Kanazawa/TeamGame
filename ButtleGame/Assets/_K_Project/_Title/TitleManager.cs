using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーン遷移などを行う
/// </summary>
public class TitleManager : MonoBehaviour
{
    private static TitleManager titleManager;

    [SerializeField] Cursor titleCursor; // タイトル画面カーソル
    [SerializeField] Cursor optionCursor; // オプション画面カーソル
    [SerializeField] Option option;
    //=====================================================================
    //外部参照用関数
    //=====================================================================

    // BGMの値を取得する
    public static void GetBGMVolume() => titleManager.option.GetBGMValue_();

    // SEの値を取得する
    public static void GetSEVolume() => titleManager.option.GetSEValue_();

    //// BGM値をVolumeにセットする
    //public static void SetBGMVolume() => titleManager.SetVolumeBGM();

    //// SE値をVolumeにセットする
    //public static void SetSEVolume() => titleManager.SetVolumeSE();

    // シーンのフェードアウト

    // シーンのフェードイン

    [SerializeField] KeyCode inputKey = KeyCode.Space;

    public enum TitleCursor
    {
        GameStart,
        Option,
        Credit,
    }

    public enum OptionCursor
    {
        BGM,
        SE,
        BackTitle,
    }

    public enum CreditCursor
    {
        BackTitle,
    }

    public enum enTitleState
    {
        InitFadeOut,

        TitleAnimation,
        TitleScene,
        TitleSceneFadeIn,
        TitleSceneFadeOut,

        OptionScene,
        OptionSceneFadeIn,
        OptionSceneFadeOut,

        CreditScene,
        CreditSceneFadeIn,
        CreditSceneFadeOut,
    }
    public enTitleState state;

    [SerializeField] float fadeSpeed;

    [SerializeField] CanvasGroup canvasFade;
    [SerializeField] CanvasGroup canvasTitle;
    [SerializeField] CanvasGroup canvasOption;
    [SerializeField] CanvasGroup canvasCredit;

    private bool fadeinOption_f = false;
    private bool fadeinCredit_f = false;

    private void Start()
    {
        // Option,Creditの非表示
        canvasOption.alpha = 0.0f;
        canvasOption.interactable = false;
        canvasOption.blocksRaycasts = false;

        canvasCredit.alpha = 0.0f;
        canvasCredit.interactable = false;
        canvasCredit.blocksRaycasts = false;
    }
}

    //private void ChangeState(State.enState enNextState)
    //{
    //    // 実行時のフェード
    //    if (state == State.enState.FADE)
    //    {
    //        canvasFade.interactable = false;
    //        canvasFade.blocksRaycasts = false;
    //    }
    //    // メニュー画面
    //    else if (state == State.enState.TITLE)
    //    {
    //        // 処理なし
    //    }
    //    //オプション画面遷移中のフェード
    //    else if (state == State.enState.OPTION_FADE)
    //    {
    //        canvasOption.interactable = false;
    //        canvasOption.blocksRaycasts = false;
    //    }
    //    // オプション画面
    //    else if (state == State.enState.OPTION)
    //    {
    //        // 処理なし
    //    }
    //    // クレジット画面遷移中のフェード
    //    else if (state == State.enState.CREDIT_FADE)
    //    {
    //        canvasCredit.interactable = false;
    //        canvasCredit.blocksRaycasts = false;
    //    }
    //    // クレジット画面
    //    else if (state == State.enState.CREDIT)
    //    {
    //        // 処理なし
    //    }

    //    switch (enNextState)
    //    {
    //        case State.enState.FADE: break;
    //        case State.enState.TITLE: break;
    //        case State.enState.OPTION_FADE: break;
    //        case State.enState.OPTION: break;
    //        case State.enState.CREDIT_FADE: break;
    //        case State.enState.CREDIT: break;
    //    }

    //    // 切り換え
    //    state = enNextState;
    //}

//    private void Update()
//    {
//        if (state == State.enState.TITLE)
//        {
//            if (titleCursor.GetCursorNum() == 0 && Input.GetKeyDown(inputKey))
//            {
//                SetVolumeBGM();
//                SetVolumeSE();
//                SceneManager.LoadScene("GameScene");
//            }
//            else if (titleCursor.GetCursorNum() == 1 && Input.GetKeyDown(inputKey)) // Option
//            {
//                optionCursor.cursorReset();
//                fadeinOption_f = true;
//                Fadeout(canvasTitle);
//            }
//            else if (titleCursor.GetCursorNum() == 2 && Input.GetKeyDown(inputKey)) // Credit
//            {
//                optionCursor.cursorReset();
//                fadeinCredit_f = true;
//                Fadeout(canvasTitle);
//            }
//            else if (titleCursor.GetCursorNum() == 3 && Input.GetKeyDown(inputKey)) // Quit
//            {
//                OnQuit();
//            }
//        }

//        // 実行時のフェード
//        if (state == State.enState.FADE)
//        {
//            canvasFade.alpha -= fadeSpeed * Time.deltaTime;
//            if (canvasFade.alpha <= 0.0f)
//            {
//                ChangeState(State.enState.TITLE);
//            }
//        }
//        // メニュー画面
//        else if (state == State.enState.TITLE)
//        {
//            if (fadeinOption_f == true)
//            {
//                fadeinOption_f = false;
//                ChangeState(State.enState.OPTION_FADE);
//            }
//            if (fadeinCredit_f == true)
//            {
//                fadeinCredit_f = false;
//                ChangeState(State.enState.CREDIT_FADE);
//            }
//        }
//        // オプション画面遷移中のフェード
//        else if (state == State.enState.OPTION_FADE)
//        {
//            canvasOption.alpha += Time.deltaTime;
//            if (canvasOption.alpha >= 1.0f)
//            {
//                ChangeState(State.enState.OPTION);
//            }
//        }
//        // オプション画面
//        else if (state == State.enState.OPTION) { }
//        // クレジット画面遷移中のフェード
//        else if (state == State.enState.CREDIT_FADE)
//        {
//            canvasCredit.alpha += Time.deltaTime;
//            if (canvasCredit.alpha >= 1.0f)
//            {
//                ChangeState(State.enState.CREDIT);
//            }
//        }
//        // クレジット画面
//        else if (state == State.enState.CREDIT) { }
//    }

//    /// <summary>
//    /// ゲーム終了
//    /// </summary>
//    public void OnQuit()
//    {
//#if UNITY_EDITOR
//        UnityEditor.EditorApplication.isPlaying = false;
//#else
//        UnityEngine.Application.Quit();
//#endif
//    }

//    /// <summary>
//    /// フェードアウト
//    /// </summary>
//    public void Fadeout(CanvasGroup canvasGroup)
//    {
//        canvasGroup.alpha = 0.0f;
//        canvasGroup.interactable = false;
//        canvasGroup.blocksRaycasts = false;
//    }

//    /// <summary>
//    /// オプション→タイトル
//    /// </summary>
//    public void OnCancel()
//    {
//        canvasTitle.alpha = 1.0f;
//        canvasTitle.interactable = true;
//        canvasTitle.blocksRaycasts = true;

//        canvasOption.alpha = 0.0f;
//        canvasOption.interactable = false;
//        canvasOption.blocksRaycasts = false;
//        ChangeState(State.enState.TITLE);
//    }

//    /// <summary>
//    /// クレジット→タイトル
//    /// </summary>
//    public void OnBuck()
//    {
//        canvasTitle.alpha = 1.0f;
//        canvasTitle.interactable = true;
//        canvasTitle.blocksRaycasts = true;

//        canvasCredit.alpha = 0.0f;
//        canvasCredit.interactable = false;
//        canvasCredit.blocksRaycasts = false;
//        ChangeState(State.enState.TITLE);
//    }

//    public State.enState GetState()
//    {
//        return state;
//    }

//    public void SetVolumeBGM()
//    {
//        Debug.Log("BGM");
//        VolumeGetSet.volGetSet.VolumeSetBGM(option.GetBGMValue_());
//    }

//    public void SetVolumeSE()
//    {
//        VolumeGetSet.volGetSet.VolumeSetSE(option.GetSEValue_());
//    }
//}
