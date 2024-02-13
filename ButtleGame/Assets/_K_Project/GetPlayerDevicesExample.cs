using UnityEngine;
using UnityEngine.InputSystem;

public class GetPlayerDevicesExample : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;

    private void Start()
    {
        if (_playerInput == null)
            return;

        // �v���C���[�̓A�N�e�B�u���ǂ����`�F�b�N
        if (!_playerInput.user.valid)
        {
            Debug.Log("�A�N�e�B�u�ȃv���C���[�ł͂���܂���");
            return;
        }

        // �v���C���[�ԍ������O�o��
        Debug.Log($"===== �v���C���[#{_playerInput.user.index} =====");

        // �f�o�C�X�ꗗ���擾
        foreach (var device in _playerInput.devices)
        {
            // �f�o�C�X�������O�o��
            Debug.Log(device.name);
        }
    }
}