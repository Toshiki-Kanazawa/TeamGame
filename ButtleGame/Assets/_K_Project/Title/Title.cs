using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Title : MonoBehaviour
{
    [SerializeField] private string nextScene;

    [SerializeField] private Cursor cCursor; // Cursor クラスのインスタンス

    void Start()
    {
        cCursor.GetComponent<Cursor>();
    }

    /// <summary>
    /// InputSystem用の関数
    /// </summary>
    /// <param name="context"></param>
    public void OnDecision(InputAction.CallbackContext context)
    {
        // Cursor クラスから値を受け取り、処理を変更する
        int cur = cCursor.cursor;

        switch (cur)
        {
            case 0:
                SceneManager.LoadScene(nextScene);
                break;
            case 1:
                Debug.Log("OptionScene");
                break;
            default:
                Debug.Log("Title.csの例外参照");
                break;
        }
    }
}