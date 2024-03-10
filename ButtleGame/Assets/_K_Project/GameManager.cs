using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using Cysharp.Threading.Tasks;

[RequireComponent(typeof(SceneController))]
public class GameManager : SingletonMonoBehaviour<GameManager>
{
    // --------- �C���X�y�N�^�[�Őݒ�ł���ϐ� ---------
    [Header("EventSystem"), SerializeField]
    private EventSystem eventSystem = null;
    public EventSystem mEventSystem { get { return eventSystem; } }

    [Header("�N�����̃V�[��"), SerializeField]
    private string startSceneName = "Title";

    [Header("�J�[�\���̕\��"), SerializeField]
    private bool visibleFlag = false;

    // �X�J�C�{�b�N�X�̃}�e���A��
    public Material skyBox { get; private set; } = null;

    // --------- ����̊֐� ---------

    /// <summary>
    /// Awake���̏������֐�
    /// </summary>
    protected override void Initialize()
    {
        //VisibleCursor(visibleFlag);
    }

    private void VisibleCursor(bool flag)
    {
        UnityEngine.Cursor.visible = flag;

        if (flag)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        }
    }

    /// <summary>
    /// �Q�[���I���֐�
    /// </summary>
    public void QuitGame()
    {
#if UNITY_EDITOR            // �G�f�B�^�[����̋N����
        EditorApplication.isPlaying = false;

#elif UNITY_STANDALONE      // �A�v���P�[�V��������̋N����
            Application.Quit();
#endif
    }

    // --------- Unity �̊֐� ---------

    private async UniTask Start()
    {
        // �X�J�C�{�b�N�X�̃}�e���A���̎擾
        skyBox = new Material(RenderSettings.skybox);
        RenderSettings.skybox = skyBox;

        // �ŏ��̃V�[�����J��
        SceneController.Instance.ChangeScene(startSceneName,0.8f).Forget();
    }

    private void Update()
    {
        // Esc �ŃQ�[�����I������
        if (Input.GetKeyDown(KeyCode.Escape)) QuitGame();
    }
}
