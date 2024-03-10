using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// シーン繊維を管理するクラス
/// </summary>
public class SceneController : SingletonMonoBehaviour<SceneController>
{
    [Header("シーントランジション"), SerializeField]
    private SceneTransition transition = null;

    [Header("フェード用キャンバスグループ"), SerializeField]
    private CanvasGroup fade = null;
    public CanvasGroup Fade { get { return fade; } }

    // 現在のシーン
    public string currentSceneName { get; private set; } = null;

    public bool IsFade { get; private set; } = false;

    /// <summary>
    /// シーントランジションをしてシーンを切り替える
    /// </summary>
    /// <param name="nextSceneName"> 次のシーンの名前 </param>
    /// <returns> 非同期処理の完了 </returns>
    public async UniTask ChangeScene(string nextSceneName,float fadeTime)
    {
        // 文字列がnullまたは空でないとき
        if (!string.IsNullOrEmpty(currentSceneName))
        {
            // トランジションが終わったら次のシーンに遷移する
            await FadeOut(fadeTime);
            Debug.Log("シーン繊維 (" + currentSceneName + "→" + nextSceneName + ")");
            await SceneManager.UnloadSceneAsync(currentSceneName);
        }
        else
        {
            currentSceneName = nextSceneName;

            Debug.Log("NULL シーン繊維 (" + currentSceneName + "→" + nextSceneName + ")");
            await SceneManager.LoadSceneAsync(currentSceneName, LoadSceneMode.Additive);
        }
    }
    /// <summary>
    /// フェードアウト処理
    /// </summary>
    /// <param name="fadeTime">フェードにかける時間</param>
    /// <returns></returns>
    public async UniTask FadeOut(float fadeTime)
    {
        Debug.Log("フェード中");
        IsFade = true;
        fade.alpha = 0f;
        //await fade.DOFade(1f,fadeTime);
        IsFade = false;
    }

    /// <summary>
    /// フェードイン処理
    /// </summary>
    /// <param name="fadeTime">フェードにかける時間</param>
    /// <returns></returns>
    public async UniTask FadeIn(float fadeTime)
    {
        IsFade = true;
        fade.alpha = 1f;
        //await fade.DOFade(0f, fadeTime);
        IsFade = false;
    }

}
