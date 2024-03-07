using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Cysharp.Threading.Tasks;

public class Title : MonoBehaviour
{
    [Header("���̃V�[��"),SerializeField] private string nextScene;

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
                    SceneController.Instance.ChangeScene(nextScene).Forget();
                    Debug.Log("�V�[���@��");
                    break;
                case 1:
                    Debug.Log("OptionScene");
                    break;
                default:
                    Debug.Log("Title.cs�̗�O�Q��");
                    break;
            }
        }
    }

    /// <summary>
    /// InputSystem�p�̊֐�
    /// </summary>
    /// <param name="context"></param>
    public void OnDecision(InputAction.CallbackContext context)
    {
        //    // Cursor �N���X����l���󂯎��A������ύX����
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
        //            Debug.Log("Title.cs�̗�O�Q��");
        //            break;
        //    }
        //}
    }
}