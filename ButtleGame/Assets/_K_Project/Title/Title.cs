using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Title : MonoBehaviour
{
    [SerializeField] private string nextScene;

    [SerializeField] private Cursor cCursor;
    [SerializeField] private SceneTransition transition;

    private void Start()
    {
        cCursor.GetComponent<Cursor>();
        transition.GetComponent<SceneTransition>();
    }

    private void Update()
    {
        if (transition.finish_f)
        {
            SceneManager.LoadScene(nextScene);
        }
    }

    /// <summary>
    /// InputSystem�p�̊֐�
    /// </summary>
    /// <param name="context"></param>
    public void OnDecision(InputAction.CallbackContext context)
    {
        // Cursor �N���X����l���󂯎��A������ύX����
        int cur = cCursor.cursor;

        switch (cur)
        {
            case 0:
                transition.execute_f = true;
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