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
        // ゲームパッドが接続されていないと null
        if (Gamepad.current == null) return;

        // ゲームパッドの Yボタン が押された場合
        if(Gamepad.current.buttonNorth.wasPressedThisFrame)
        {
            inputButton = true;
            Debug.Log("Button North が押された");
        }
        // ゲームパッドの Yボタン が離された場合
        if(Gamepad.current.buttonNorth.wasReleasedThisFrame)
        {
            inputButton = false;
            Debug.Log("Button North が押された");
        }
        // ゲームパッドの Aボタン が押された場合
        if(Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            Debug.Log("Button South が押された");
        }

        obj.SetActive(inputButton);

    }

    // 入力状態を画面に表示する
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
