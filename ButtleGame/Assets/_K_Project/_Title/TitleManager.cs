using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �V�[���J�ڂȂǂ��s��
/// </summary>
public class TitleManager : MonoBehaviour
{
    private static TitleManager titleManager;

    [SerializeField] Cursor titleCursor; // �^�C�g����ʃJ�[�\��
    [SerializeField] Cursor optionCursor; // �I�v�V������ʃJ�[�\��
    [SerializeField] Option option;
    //=====================================================================
    //�O���Q�Ɨp�֐�
    //=====================================================================

    // BGM�̒l���擾����
    public static void GetBGMVolume() => titleManager.option.GetBGMValue_();

    // SE�̒l���擾����
    public static void GetSEVolume() => titleManager.option.GetSEValue_();

    //// BGM�l��Volume�ɃZ�b�g����
    //public static void SetBGMVolume() => titleManager.SetVolumeBGM();

    //// SE�l��Volume�ɃZ�b�g����
    //public static void SetSEVolume() => titleManager.SetVolumeSE();

    // �V�[���̃t�F�[�h�A�E�g

    // �V�[���̃t�F�[�h�C��

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
        // Option,Credit�̔�\��
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
    //    // ���s���̃t�F�[�h
    //    if (state == State.enState.FADE)
    //    {
    //        canvasFade.interactable = false;
    //        canvasFade.blocksRaycasts = false;
    //    }
    //    // ���j���[���
    //    else if (state == State.enState.TITLE)
    //    {
    //        // �����Ȃ�
    //    }
    //    //�I�v�V������ʑJ�ڒ��̃t�F�[�h
    //    else if (state == State.enState.OPTION_FADE)
    //    {
    //        canvasOption.interactable = false;
    //        canvasOption.blocksRaycasts = false;
    //    }
    //    // �I�v�V�������
    //    else if (state == State.enState.OPTION)
    //    {
    //        // �����Ȃ�
    //    }
    //    // �N���W�b�g��ʑJ�ڒ��̃t�F�[�h
    //    else if (state == State.enState.CREDIT_FADE)
    //    {
    //        canvasCredit.interactable = false;
    //        canvasCredit.blocksRaycasts = false;
    //    }
    //    // �N���W�b�g���
    //    else if (state == State.enState.CREDIT)
    //    {
    //        // �����Ȃ�
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

    //    // �؂芷��
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

//        // ���s���̃t�F�[�h
//        if (state == State.enState.FADE)
//        {
//            canvasFade.alpha -= fadeSpeed * Time.deltaTime;
//            if (canvasFade.alpha <= 0.0f)
//            {
//                ChangeState(State.enState.TITLE);
//            }
//        }
//        // ���j���[���
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
//        // �I�v�V������ʑJ�ڒ��̃t�F�[�h
//        else if (state == State.enState.OPTION_FADE)
//        {
//            canvasOption.alpha += Time.deltaTime;
//            if (canvasOption.alpha >= 1.0f)
//            {
//                ChangeState(State.enState.OPTION);
//            }
//        }
//        // �I�v�V�������
//        else if (state == State.enState.OPTION) { }
//        // �N���W�b�g��ʑJ�ڒ��̃t�F�[�h
//        else if (state == State.enState.CREDIT_FADE)
//        {
//            canvasCredit.alpha += Time.deltaTime;
//            if (canvasCredit.alpha >= 1.0f)
//            {
//                ChangeState(State.enState.CREDIT);
//            }
//        }
//        // �N���W�b�g���
//        else if (state == State.enState.CREDIT) { }
//    }

//    /// <summary>
//    /// �Q�[���I��
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
//    /// �t�F�[�h�A�E�g
//    /// </summary>
//    public void Fadeout(CanvasGroup canvasGroup)
//    {
//        canvasGroup.alpha = 0.0f;
//        canvasGroup.interactable = false;
//        canvasGroup.blocksRaycasts = false;
//    }

//    /// <summary>
//    /// �I�v�V�������^�C�g��
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
//    /// �N���W�b�g���^�C�g��
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
