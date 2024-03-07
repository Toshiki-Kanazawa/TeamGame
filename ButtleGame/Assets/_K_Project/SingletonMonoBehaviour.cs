using UnityEngine;

/// <summary>
/// MonoBehaviourを継承したジェネリック型のシングルトンクラス
/// </summary>
/// <typeparam name="T"> 制約条件 : MonoBehaviour派生 </typeparam>
public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    // インスタンス
    public static T Instance { get; private set; } = null;

    // インスタンスが有効かどうか
    // Instance が null でない時に true を返す
    public static bool IsValid() => Instance != null;

    /// <summary>
    /// Start() よりも先に実行する初期化
    /// インスタンスを1つに制限する
    /// </summary>
    private void Awake()
    {
        if(!Instance)
        {
            // 自身を T 型にキャストする
            Instance = this as T;
            Initialize();
            Debug.Log(typeof(T).Name + "のシングルトンが作成されました");
            return;
        }
        else
        {
            Debug.Log("※ Instance が nullじゃないです : SingletonMonoBehaviour");
        }
    }

    /// <summary>
    /// 破棄されるときに null を入れる
    /// </summary>
    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
        OnRelease();
        Debug.Log(typeof(T).Name + "のシングルトンが削除されました");
    }

    /// <summary>
    /// 初期化
    /// </summary>
    protected virtual void Initialize() {}

    /// <summary>
    /// 終了関数
    /// </summary>
    protected virtual void OnRelease() {}
}
