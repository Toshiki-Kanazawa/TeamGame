using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Title : MonoBehaviour
{
    [Header("���̃V�[��"),SerializeField] private string nextScene;

    [SerializeField] private Cursor cCursor;

    private void Start()
    {
        cCursor.GetComponent<Cursor>();
    }

    private void Update()
    {
        int cur = cCursor.cursor;
        if(Input.GetKeyDown(KeyCode.Return))
        {
            switch (cur)
            {
                case 0:
                    StartCoroutine(Coroutine());
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

    private IEnumerator Coroutine()
    {
        yield return SceneController.Instance.ChangeScene(nextScene, 0.02f * Time.deltaTime);
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