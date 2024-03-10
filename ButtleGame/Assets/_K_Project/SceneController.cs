using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// �V�[���@�ۂ��Ǘ�����N���X
/// </summary>
public class SceneController : SingletonMonoBehaviour<SceneController>
{
    [Header("�V�[���g�����W�V����"), SerializeField]
    private SceneTransition transition = null;

    [Header("�t�F�[�h�p�L�����o�X�O���[�v"), SerializeField]
    private CanvasGroup fade = null;
    public CanvasGroup Fade { get { return fade; } }

    // ���݂̃V�[��
    public string currentSceneName { get; private set; } = null;

    public bool IsFade { get; private set; } = false;

    /// <summary>
    /// �V�[���g�����W�V���������ăV�[����؂�ւ���
    /// </summary>
    /// <param name="nextSceneName"> ���̃V�[���̖��O </param>
    /// <returns> �񓯊������̊��� </returns>
    public async UniTask ChangeScene(string nextSceneName,float fadeTime)
    {
        // ������null�܂��͋�łȂ��Ƃ�
        if (!string.IsNullOrEmpty(currentSceneName))
        {
            // �g�����W�V�������I������玟�̃V�[���ɑJ�ڂ���
            await FadeOut(fadeTime);
            Debug.Log("�V�[���@�� (" + currentSceneName + "��" + nextSceneName + ")");
            await SceneManager.UnloadSceneAsync(currentSceneName);
        }
        else
        {
            currentSceneName = nextSceneName;

            Debug.Log("NULL �V�[���@�� (" + currentSceneName + "��" + nextSceneName + ")");
            await SceneManager.LoadSceneAsync(currentSceneName, LoadSceneMode.Additive);
        }
    }
    /// <summary>
    /// �t�F�[�h�A�E�g����
    /// </summary>
    /// <param name="fadeTime">�t�F�[�h�ɂ����鎞��</param>
    /// <returns></returns>
    public async UniTask FadeOut(float fadeTime)
    {
        Debug.Log("�t�F�[�h��");
        IsFade = true;
        fade.alpha = 0f;
        //await fade.DOFade(1f,fadeTime);
        IsFade = false;
    }

    /// <summary>
    /// �t�F�[�h�C������
    /// </summary>
    /// <param name="fadeTime">�t�F�[�h�ɂ����鎞��</param>
    /// <returns></returns>
    public async UniTask FadeIn(float fadeTime)
    {
        IsFade = true;
        fade.alpha = 1f;
        //await fade.DOFade(0f, fadeTime);
        IsFade = false;
    }

}
