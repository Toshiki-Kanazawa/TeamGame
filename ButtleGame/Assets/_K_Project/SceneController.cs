using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

/// <summary>
/// シーン繊維を管理するクラス
/// </summary>
public class SceneController : SingletonMonoBehaviour<SceneController>
{
    [Header("シーントランジション"), SerializeField]
    private SceneTransition transition = null;

    // 現在のシーン
    public string currentSceneName { get; private set; } = null;

    public async UniTask ChangeScene(string nextSceneName)
    {
        if (!string.IsNullOrEmpty(currentSceneName))
        {
            // トランジションが終わったら次のシーンに遷移する
            Debug.Log("シーン繊維 (" + currentSceneName + "→" + nextSceneName + ")");
            await transition.Execute(SceneTransition.enInitState.Out);
            await SceneManager.UnloadSceneAsync(currentSceneName);
        }
        currentSceneName = nextSceneName;

        Debug.Log("NULL シーン繊維 (" + currentSceneName + "→" + nextSceneName + ")");
        await SceneManager.LoadSceneAsync(currentSceneName, LoadSceneMode.Additive);
        await transition.Execute(SceneTransition.enInitState.In);
    }
}
