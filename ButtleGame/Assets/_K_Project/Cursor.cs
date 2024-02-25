using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(RectTransform))]
public class Cursor : MonoBehaviour
{
    // --- インスペクタで変更できる変数 -----------------------
    [SerializeField, Header("カーソルしたいTextを参照")]
    private List<Text> text;

    [SerializeField, Tooltip("カーソル移動のキー入力タイプ"),Space(15)]
     private enKeyDirection keyDirection = enKeyDirection.Vertical;

    [SerializeField, Tooltip("カーソルの座標タイプ")]
    private enPosition cursorPos;

    [Header("X軸回転")]
    [SerializeField, Tooltip("カーソルのX軸回転の有効")]
    private bool rotXValid = true;

    [SerializeField, Tooltip("カーソルのX軸回転の速度")]
    private float rotXSpeed = 5.0f;

    [SerializeField, Header("スティックの閾値")]
    private float threshold = 0.8f;

    // --- プライベート変数 ----------------------

    // キー入力タイプ
    private enum enKeyDirection
    {
        Vertical,
        Horizontal,
    }

    // 座標タイプ
    private enum enPosition
    {
        Left,
        Center,
    }

    // 現在のカーソルの値
    private int _cursor;
    public int cursor
    {
        get { return _cursor; }         // Getter
        private set { _cursor = value; } // Setter(private)
    }

    // 座標変更用のコンポーネント
    private RectTransform rectTransform;

    // --- Unity 関数 --------------------------------

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

    // --- 自作関数 -----------------------------
    
    /// <summary>
    /// InputSystem用のカーソル移動処理
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
    /// 自身の座標設定
    /// </summary>
    /// <param name="cursor"> 現在のカーソルの値 </param>
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
    /// 自身のX軸回転処理
    /// </summary>
    private void CrusorRotX()
    {
        if (rotXValid == false) return;

        float x = Mathf.Sin(Time.time * rotXSpeed);
        float y = transform.localScale.y;
        transform.localScale = new Vector2(x,y);
    }
}