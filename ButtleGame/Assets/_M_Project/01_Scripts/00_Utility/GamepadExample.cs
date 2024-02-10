using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadExample : MonoBehaviour
{
    public bool inputButton = false;

    public GameObject obj;
    void Start()
    {
        obj.SetActive(false);
    }

    void Update()
    {
        // �Q�[���p�b�h���ڑ�����Ă��Ȃ��� null
        if (Gamepad.current == null) return;

        // �Q�[���p�b�h�� Y�{�^�� �������ꂽ�ꍇ
        if(Gamepad.current.buttonNorth.wasPressedThisFrame)
        {
            inputButton = true;
            Debug.Log("Button North �������ꂽ");
        }
        // �Q�[���p�b�h�� Y�{�^�� �������ꂽ�ꍇ
        if(Gamepad.current.buttonNorth.wasReleasedThisFrame)
        {
            inputButton = false;
            Debug.Log("Button North �������ꂽ");
        }
        // �Q�[���p�b�h�� A�{�^�� �������ꂽ�ꍇ
        if(Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            Debug.Log("Button South �������ꂽ");
        }

        obj.SetActive(inputButton);

    }

    // ���͏�Ԃ���ʂɕ\������
    void OnGUI()
    {
        if (Gamepad.current == null) return;

        GUILayout.Label($"leftStick: {Gamepad.current.leftStick.ReadValue()}");
        GUILayout.Label($"rightStick: {Gamepad.current.rightStick.ReadValue()}");
        GUILayout.Label($"buttonNorth: {Gamepad.current.buttonNorth.isPressed}");
        GUILayout.Label($"buttonSouth: {Gamepad.current.buttonSouth.isPressed}");
        GUILayout.Label($"buttonEast: {Gamepad.current.buttonEast.isPressed}");
        GUILayout.Label($"buttonWest: {Gamepad.current.buttonWest.isPressed}");
        GUILayout.Label($"leftShoulder: {Gamepad.current.leftShoulder.ReadValue()}");
        GUILayout.Label($"leftTrigger: {Gamepad.current.leftTrigger.ReadValue()}");
        GUILayout.Label($"rightShoulder: {Gamepad.current.rightShoulder.ReadValue()}");
        GUILayout.Label($"rightTrigger: {Gamepad.current.rightTrigger.ReadValue()}");
    }
}
