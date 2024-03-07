using UnityEngine;

/// <summary>
/// MonoBehaviour���p�������W�F�l���b�N�^�̃V���O���g���N���X
/// </summary>
/// <typeparam name="T"> ������� : MonoBehaviour�h�� </typeparam>
public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    // �C���X�^���X
    public static T Instance { get; private set; } = null;

    // �C���X�^���X���L�����ǂ���
    // Instance �� null �łȂ����� true ��Ԃ�
    public static bool IsValid() => Instance != null;

    /// <summary>
    /// Start() ������Ɏ��s���鏉����
    /// �C���X�^���X��1�ɐ�������
    /// </summary>
    private void Awake()
    {
        if(!Instance)
        {
            // ���g�� T �^�ɃL���X�g����
            Instance = this as T;
            Initialize();
            Debug.Log(typeof(T).Name + "�̃V���O���g�����쐬����܂���");
            return;
        }
        else
        {
            Debug.Log("�� Instance �� null����Ȃ��ł� : SingletonMonoBehaviour");
        }
    }

    /// <summary>
    /// �j�������Ƃ��� null ������
    /// </summary>
    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
        OnRelease();
        Debug.Log(typeof(T).Name + "�̃V���O���g�����폜����܂���");
    }

    /// <summary>
    /// ������
    /// </summary>
    protected virtual void Initialize() {}

    /// <summary>
    /// �I���֐�
    /// </summary>
    protected virtual void OnRelease() {}
}
