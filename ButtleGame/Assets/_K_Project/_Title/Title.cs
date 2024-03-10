using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Cysharp.Threading.Tasks;

public class Title : MonoBehaviour
{
    [Header("次のシーン"),SerializeField] private string nextScene;

    [SerializeField] private Cursor cCursor;
    [SerializeField] private SceneTransition transition;

    private void Start()
    {
        cCursor.GetComponent<Cursor>();
        transition.GetComponent<SceneTransition>();
    }

    private void Update()
    {
        int cur = cCursor.cursor;
        if(Input.GetKeyDown(KeyCode.Return))
        {
            switch (cur)
            {
                case 0:
                    SceneController.Instance.ChangeScene(nextScene,0.8f).Forget();
                    Debug.Log("シーン繊維");
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

    /// <summary>
    /// InputSystem用の関数
    /// </summary>
    /// <param name="context"></param>
    public void OnDecision(InputAction.CallbackContext context)
    {
        //    // Cursor クラスから値を受け取り、処理を変更する
        //    int cur = cCursor.cursor;

        //    switch (cur)
        //    {
        //        case 0:
        //            SceneController.Instance.ChangeScene(nextScene).Forget();
        //            break;
        //        case 1:
        //            Debug.Log("OptionScene");
        //            break;
        //        default:
        //            Debug.Log("Title.csの例外参照");
        //            break;
        //    }
        //}
    }
}