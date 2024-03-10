using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;

/// <summary>
/// �V�[���@�ۂ��Ǘ�����N���X
/// </summary>
public class SceneController : SingletonMonoBehaviour<SceneController>
{
    [Header("�t�F�[�h�p�L�����o�X�O���[�v"), SerializeField]
    private CanvasGroup fade = null;

    // ���݂̃V�[��
    public string currentSceneName { get; private set; } = null;

    // true �� �t�F�[�h��
    public bool IsFade { get; private set; } = false;

    // �t���O�𗧂Ă�� Update�Ńt�F�[�h����������
    private bool fadeIn_f = false;
    private bool fadeOut_f = false;

    /// <summary>
    /// �t�F�[�h���ăV�[����؂�ւ���
    /// </summary>
    /// <param name="nextSceneName"> ���̃V�[�� </param>
    /// <param name="fadeTime"> �t�F�[�h���� </param>
    public IEnumerator ChangeScene(string nextSceneName, float fadeTime)
    {
        if (!string.IsNullOrEmpty(currentSceneName))
        {
            // �t�F�[�h�A�E�g��ɃV�[���؂�ւ����s��
            yield return FadeOut(nextSceneName, fadeTime);
        }
        else fade.alpha = 1.0f;

        currentSceneName = nextSceneName;
        yield return FadeIn(fadeTime);
        Debug.Log("�V�[���@�� (" + currentSceneName + "��" + nextSceneName + ")");
    }

    /// <summary>
    /// �t�F�[�h�A�E�g����
    /// </summary>
    /// <param name="nextSceneName">���̃V�[���̖��O</param>
    /// <param name="fadeTime">�t�F�[�h�ɂ����鎞��</param>
    public IEnumerator FadeOut(string nextSceneName, float fadeTime)
    {
        IsFade = true;
        fade.alpha = 0f;

        // �t�F�[�h�A�E�g�̃A�j���[�V���������s���A������ҋ@����
        yield return fade.DOFade(1f, fadeTime).WaitForCompletion();

        IsFade = false;
        Debug.Log("�t�F�[�h�A�E�g����");

        // �t�F�[�h�����������玟�̃V�[�����A�����[�h����
        SceneManager.UnloadSceneAsync(currentSceneName);
    }

    /// <summary>
    /// �t�F�[�h�C������
    /// </summary>
    /// <param name="fadeTime">�t�F�[�h�ɂ����鎞��</param>
    public IEnumerator FadeIn(float fadeTime)
    {
        IsFade = true;
        fade.alpha = 1f;
        yield return fade.DOFade(0f, fadeTime).OnComplete(() =>
        {
            IsFade = false;
            Debug.Log("�t�F�[�h�C������");
            // �t�F�[�h�C�������������玟�̃V�[�������[�h����
            SceneManager.LoadSceneAsync(currentSceneName, LoadSceneMode.Additive);
        });
    }
}