using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

/// <summary>
/// �V�[���@�ۂ��Ǘ�����N���X
/// </summary>
public class SceneController : SingletonMonoBehaviour<SceneController>
{
    [Header("�V�[���g�����W�V����"), SerializeField]
    private SceneTransition transition = null;

    // ���݂̃V�[��
    public string currentSceneName { get; private set; } = null;

    public async UniTask ChangeScene(string nextSceneName)
    {
        if (!string.IsNullOrEmpty(currentSceneName))
        {
            // �g�����W�V�������I������玟�̃V�[���ɑJ�ڂ���
            Debug.Log("�V�[���@�� (" + currentSceneName + "��" + nextSceneName + ")");
            await transition.Execute(SceneTransition.enInitState.Out);
            await SceneManager.UnloadSceneAsync(currentSceneName);
        }
        currentSceneName = nextSceneName;

        Debug.Log("NULL �V�[���@�� (" + currentSceneName + "��" + nextSceneName + ")");
        await SceneManager.LoadSceneAsync(currentSceneName, LoadSceneMode.Additive);
        await transition.Execute(SceneTransition.enInitState.In);
    }
}
