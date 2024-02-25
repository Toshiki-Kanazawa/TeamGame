using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Title : MonoBehaviour
{
    [SerializeField] private string nextScene;

    [SerializeField] private Cursor cCursor; // Cursor �N���X�̃C���X�^���X

    void Start()
    {
        cCursor.GetComponent<Cursor>();
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
                SceneManager.LoadScene(nextScene);
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