using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;

/// <summary>
/// シーン繊維を管理するクラス
/// </summary>
public class SceneController : SingletonMonoBehaviour<SceneController>
{
    [Header("フェード用キャンバスグループ"), SerializeField]
    private CanvasGroup fade = null;

    // 現在のシーン
    public string currentSceneName { get; private set; } = null;

    // true → フェード中
    public bool IsFade { get; private set; } = false;

    // フラグを立てると Updateでフェード処理をする
    private bool fadeIn_f = false;
    private bool fadeOut_f = false;

    /// <summary>
    /// フェードしてシーンを切り替える
    /// </summary>
    /// <param name="nextSceneName"> 次のシーン </param>
    /// <param name="fadeTime"> フェード時間 </param>
    public IEnumerator ChangeScene(string nextSceneName, float fadeTime)
    {
        if (!string.IsNullOrEmpty(currentSceneName))
        {
            // フェードアウト後にシーン切り替えを行う
            yield return FadeOut(nextSceneName, fadeTime);
        }
        else fade.alpha = 1.0f;

        currentSceneName = nextSceneName;
        yield return FadeIn(fadeTime);
        Debug.Log("シーン繊維 (" + currentSceneName + "→" + nextSceneName + ")");
    }

    /// <summary>
    /// フェードアウト処理
    /// </summary>
    /// <param name="nextSceneName">次のシーンの名前</param>
    /// <param name="fadeTime">フェードにかける時間</param>
    public IEnumerator FadeOut(string nextSceneName, float fadeTime)
    {
        IsFade = true;
        fade.alpha = 0f;

        // フェードアウトのアニメーションを実行し、完了を待機する
        yield return fade.DOFade(1f, fadeTime).WaitForCompletion();

        IsFade = false;
        Debug.Log("フェードアウト完了");

        // フェードが完了したら次のシーンをアンロードする
        SceneManager.UnloadSceneAsync(currentSceneName);
    }

    /// <summary>
    /// フェードイン処理
    /// </summary>
    /// <param name="fadeTime">フェードにかける時間</param>
    public IEnumerator FadeIn(float fadeTime)
    {
        IsFade = true;
        fade.alpha = 1f;
        yield return fade.DOFade(0f, fadeTime).OnComplete(() =>
        {
            IsFade = false;
            Debug.Log("フェードイン完了");
            // フェードインが完了したら次のシーンをロードする
            SceneManager.LoadSceneAsync(currentSceneName, LoadSceneMode.Additive);
        });
    }
}