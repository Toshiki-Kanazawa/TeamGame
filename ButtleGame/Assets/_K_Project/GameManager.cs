using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using Cysharp.Threading.Tasks;

[RequireComponent(typeof(SceneController))]
public class GameManager : SingletonMonoBehaviour<GameManager>
{
    // --------- インスペクターで設定できる変数 ---------
    [Header("EventSystem"), SerializeField]
    private EventSystem eventSystem = null;
    public EventSystem mEventSystem { get { return eventSystem; } }

    [Header("起動時のシーン"), SerializeField]
    private string startSceneName = "Title";

    [Header("カーソルの表示"), SerializeField]
    private bool visibleFlag = false;

    // スカイボックスのマテリアル
    public Material skyBox { get; private set; } = null;

    // --------- 自作の関数 ---------

    /// <summary>
    /// Awake時の初期化関数
    /// </summary>
    protected override void Initialize()
    {
        //VisibleCursor(visibleFlag);
    }

    private void VisibleCursor(bool flag)
    {
        UnityEngine.Cursor.visible = flag;

        if (flag)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        }
    }

    /// <summary>
    /// ゲーム終了関数
    /// </summary>
    public void QuitGame()
    {
#if UNITY_EDITOR            // エディターからの起動時
        EditorApplication.isPlaying = false;

#elif UNITY_STANDALONE      // アプリケーションからの起動時
            Application.Quit();
#endif
    }

    // --------- Unity の関数 ---------

    private async UniTask Start()
    {
        // スカイボックスのマテリアルの取得
        skyBox = new Material(RenderSettings.skybox);
        RenderSettings.skybox = skyBox;

        // 最初のシーンを開く
        SceneController.Instance.ChangeScene(startSceneName,0.8f).Forget();
    }

    private void Update()
    {
        // Esc でゲームを終了する
        if (Input.GetKeyDown(KeyCode.Escape)) QuitGame();
    }
}
