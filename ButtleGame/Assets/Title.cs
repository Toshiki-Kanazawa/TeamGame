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
        // �@ Action�X�N���v�g�̃C���X�^���X����
        inputs = new Inputs();

        // �A Input Action���@�\�����邽�߂ɗL����������
        inputs.Enable();
    }

    void OnDestroy()
    {
        // �B ���\�[�X�̉��
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
