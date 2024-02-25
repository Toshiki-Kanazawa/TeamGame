using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(RectTransform))]
public class Cursor : MonoBehaviour
{
    // --- �C���X�y�N�^�ŕύX�ł���ϐ� -----------------------
    [SerializeField, Header("�J�[�\��������Text���Q��")]
    private List<Text> text;

    [SerializeField, Tooltip("�J�[�\���ړ��̃L�[���̓^�C�v"),Space(15)]
     private enKeyDirection keyDirection = enKeyDirection.Vertical;

    [SerializeField, Tooltip("�J�[�\���̍��W�^�C�v")]
    private enPosition cursorPos;

    [Header("X����]")]
    [SerializeField, Tooltip("�J�[�\����X����]�̗L��")]
    private bool rotXValid = true;

    [SerializeField, Tooltip("�J�[�\����X����]�̑��x")]
    private float rotXSpeed = 5.0f;

    [SerializeField, Header("�X�e�B�b�N��臒l")]
    private float threshold = 0.8f;

    // --- �v���C�x�[�g�ϐ� ----------------------

    // �L�[���̓^�C�v
    private enum enKeyDirection
    {
        Vertical,
        Horizontal,
    }

    // ���W�^�C�v
    private enum enPosition
    {
        Left,
        Center,
    }

    // ���݂̃J�[�\���̒l
    private int _cursor;
    public int cursor
    {
        get { return _cursor; }         // Getter
        private set { _cursor = value; } // Setter(private)
    }

    // ���W�ύX�p�̃R���|�[�l���g
    private RectTransform rectTransform;

    // --- Unity �֐� --------------------------------

    void Start()
    {
        cursor = 0;
        rectTransform = GetComponent<RectTransform>();

        PositionSetting(cursor);
    }

    void Update()
    {
        PositionSetting(cursor);

        CrusorRotX();
    }

    // --- ����֐� -----------------------------
    
    /// <summary>
    /// InputSystem�p�̃J�[�\���ړ�����
    /// </summary>
    /// <param name="context"></param>
    public void OnCursor(InputAction.CallbackContext context)
    {
        float inputValue = 0;

        switch(keyDirection)
        {
            case enKeyDirection.Horizontal:
                inputValue = context.ReadValue<Vector2>().x;
                break;
            case enKeyDirection.Vertical:
                inputValue = context.ReadValue<Vector2>().y;
                break;
        }

        if (inputValue >= threshold && --cursor < 0)
        {
            cursor = text.Count - 1;

        }
        if (inputValue <= -threshold && ++cursor >= text.Count)
        {
            cursor = 0;
        }
    }

    /// <summary>
    /// ���g�̍��W�ݒ�
    /// </summary>
    /// <param name="cursor"> ���݂̃J�[�\���̒l </param>
    private void PositionSetting(int cursor)
    {
        switch (cursorPos)
        {
            case enPosition.Left:
                float x, y;
                x = text[cursor].rectTransform.localPosition.x - (text[cursor].rectTransform.rect.width * 0.5f);
                y = text[cursor].rectTransform.localPosition.y;
                transform.localPosition = new Vector2(x, y);
                break;
            case enPosition.Center:
                transform.localPosition = text[cursor].rectTransform.localPosition;
                break;
        }
    }

    /// <summary>
    /// ���g��X����]����
    /// </summary>
    private void CrusorRotX()
    {
        if (rotXValid == false) return;

        float x = Mathf.Sin(Time.time * rotXSpeed);
        float y = transform.localScale.y;
        transform.localScale = new Vector2(x,y);
    }
}