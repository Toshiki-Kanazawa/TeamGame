using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SeekCamera : MonoBehaviour
{
    public enum eCameraMode
    {
        GameMode,
        LockDown,
    }

    [Header("追従するオブジェクト")]
    public GameObject target;
    private Transform target_transform;
    public Vector3 prevTargetPos = new Vector3(0.0f, 0.0f, 0.0f);

    [Header("カメラの位置や追従の仕方")]
    public eCameraMode camMode = eCameraMode.GameMode;

    [Header("各モードにおけるオフセット値")]
    public Vector3 offset_LockDown = new Vector3(0.0f, 7.5f, -1.5f);
    public Vector3 offset_BackView = new Vector3(0.0f, 1.0f, -10.0f);
    public float seekSpeed = 2.0f;  // 追従速度
    public float cameraSensity = 100.0f; // 感度

    [Header("Rスティックでカメラを動かす為の変数")]
    public bool input_Rstick;
    public float inputHorizontal;
    public float inputVertical;

    [Header("カメラの縦方向の角度限界")]
    public float maxAngle = 70.0f;
    public float minAngle = -10.0f;
    private Vector3 totalAngle = Vector3.zero;

void Start()
    {
        // 初期位置の設定
        this.transform.position = new Vector3(0.0f, 1.5f, -5.0f);

        // 追従対象の座標情報を取得する
        target_transform = target.transform;
        // 前のフレームでの座標となるように座標を保存しておく
        prevTargetPos = target.transform.position;

        // 右スティックの入力初期化
        input_Rstick = false;
    }

    void Update()
    {
        // プレイヤーの周囲を回転できるような標準的なカメラ
        if (camMode == eCameraMode.GameMode)
        {
            this.transform.position += target.transform.position - prevTargetPos;
            prevTargetPos = target.transform.position;

            // カメラの回転角度を準備し、右スティックの入力を反映する
            var newAngle = Vector3.zero;
            newAngle.x = inputHorizontal * Time.deltaTime * cameraSensity;
            newAngle.y = (inputVertical * -1) * Time.deltaTime * cameraSensity;

            // 回転値を適応する時に、制限角度を越えていた場合に矯正する
            if (maxAngle < totalAngle.y + newAngle.y)
            {
                newAngle.y = maxAngle - totalAngle.y;
            }
            if (totalAngle.y + newAngle.y < minAngle)
            {
                newAngle.y = minAngle - totalAngle.y;
            }

            // 現在のカメラ角度に、今のフレームで移動した回転値を足しこむ
            totalAngle.y += newAngle.y;

            // ターゲットの位置のY座標を中心に回転する
            transform.RotateAround(prevTargetPos, Vector3.up, newAngle.x);
            transform.RotateAround(prevTargetPos, transform.right, newAngle.y);
        }
        // DSゼルダみたいな上から見た視点のカメラ
        if (camMode == eCameraMode.LockDown) // 見下ろし
        {
            // プレイヤーの座標についていく
            this.gameObject.transform.position = target_transform.position + offset_LockDown;
            // プレイヤーの方向を向く
            this.transform.LookAt(target_transform);
        }
    }

    public void GamePad_RightStick_CameraControl(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<Vector2>();
        inputHorizontal = value.x;
        inputVertical = value.y;
    }
}
