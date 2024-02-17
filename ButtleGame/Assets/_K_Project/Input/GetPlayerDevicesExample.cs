using UnityEngine;
using UnityEngine.InputSystem;

public class GetPlayerDevicesExample : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;

    private void Start()
    {
        if (_playerInput == null)
            return;

        // プレイヤーはアクティブかどうかチェック
        if (!_playerInput.user.valid)
        {
            Debug.Log("アクティブなプレイヤーではありません");
            return;
        }

        // プレイヤー番号をログ出力
        Debug.Log($"===== プレイヤー#{_playerInput.user.index} =====");

        // デバイス一覧を取得
        foreach (var device in _playerInput.devices)
        {
            // デバイス名をログ出力
            Debug.Log(device.name);
        }
    }
}