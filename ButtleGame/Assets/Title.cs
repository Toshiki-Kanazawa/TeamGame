using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Title : MonoBehaviour
{
    [SerializeField] private string nextScene;

    private Inputs inputs;

    void Start()
    {
        // ① Actionスクリプトのインスタンス生成
        inputs = new Inputs();

        // ② Input Actionを機能させるために有効化させる
        inputs.Enable();
    }

    void OnDestroy()
    {
        // ③ リソースの解放
        inputs?.Dispose();
    }

    void Update()
    {
        if (inputs.Player.Jump.triggered)
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
