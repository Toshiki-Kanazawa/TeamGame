using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SeekCamera : MonoBehaviour
{
    [Header("追従するオブジェクト")]
    public GameObject target;
    private Transform target_transform;
    public Vector3 prevTargetPos = new Vector3(0.0f, 0.0f, 0.0f);

    public enum eCameraMode
    {
        GameMode,
        LockDown,
        BackView,
    }
    public eCameraMode camMode = eCameraMode.GameMode;

    public Vector3 offset_LockDown = new Vector3(0.0f, 7.5f, -1.5f);
    public Vector3 offset_BackView = new Vector3(0.0f, 1.0f, -10.0f);
    public float seekSpeed = 2.0f;  // 追従速度
    public float cameraSensity = 100.0f; // 感度

    public bool input_Rstick;
    public float inputHorizontal;
    public float inputVertical;

    public float maxAngle = 70.0f;
    public float minAngle = -10.0f;
    private Vector3 totalAngle = Vector3.zero;

void Start()
    {
        target_transform = target.transform;
        prevTargetPos = target.transform.position;

        input_Rstick = false;
    }

    void Update()
    {

        if (camMode == eCameraMode.GameMode) // 一般的なヤツ
        {
            this.transform.position += target.transform.position - prevTargetPos;
            prevTargetPos = target.transform.position;

            var newAngle = Vector3.zero;
            newAngle.x = inputHorizontal * Time.deltaTime * cameraSensity;
            newAngle.y = (inputVertical * -1) * Time.deltaTime * cameraSensity;

            if(maxAngle < totalAngle.y + newAngle.y)
            {
                newAngle.y = maxAngle - totalAngle.y;
            }
            if(totalAngle.y + newAngle.y < minAngle)
            {
                newAngle.y = minAngle - totalAngle.y;
            }

            totalAngle.y += newAngle.y;

            // ターゲットの位置のY座標を中心に回転する
            transform.RotateAround(prevTargetPos, Vector3.up, newAngle.x);
            transform.RotateAround(prevTargetPos, transform.right, newAngle.y);

            //Mathf.Clamp(this.transform.rotation.x, -10.0f, 65.0f);

            Debug.Log("カメラが回転しました");
        }


        //if(camMode == eCameraMode.GameMode) // 一般的なヤツ
        //{
        //    this.transform.position += target.transform.position - prevTargetPos;
        //    prevTargetPos = target.transform.position;

        //    //if(Input.GetMouseButton(1))
        //    //{
        //    //    // マウスの移動量
        //    //    float mouseInputX = Input.GetAxis("Mouse X");
        //    //    float mouseInputY = Input.GetAxis("Mouse Y");

        //    //    // ターゲットの位置のY座標を中心に回転する
        //    //    transform.RotateAround(prevTargetPos, Vector3.up, mouseInputX * Time.deltaTime * 200.0f);
        //    //}
        //}
        //if(camMode == eCameraMode.LockDown) // 見下ろし
        //{
        //    this.gameObject.transform.position = target_transform.position + offset_LockDown;
        //    // プレイヤーの方向を向く
        //    this.transform.LookAt(target_transform);

        //}
        //if (camMode == eCameraMode.BackView) // 背後から
        //{
        //    this.transform.position = Vector3.Lerp(this.transform.position,
        //                                            target_transform.position + offset_BackView,
        //                                            2.0f * Time.deltaTime
        //                                            );
        //    // プレイヤーの方向を向く
        //    this.transform.LookAt(target_transform);
        //}

        // カメラ追従を制限する
        //this.transform.position = new Vector3(Mathf.Clamp(target_transform.position.x, cameraMin.x, cameraMax.x), Mathf.Clamp(target_transform.position., cameraMin.x, cameraMax.x))


        // 追従する位置にカメラ差分を足しこむ
        //offset_player = new Vector3(0.0f, 0.0f, (target.GetComponent<Test_Charactor_Controler>().moveZ * 100.0f));
        //this.gameObject.transform.position = (target_transform.position + offset_LockDown) + offset_player;

        //this.transform.Rotate(new Vector3(65.0f, 0.0f, 0.0f));
        //this.transform.rotation = new Quaternion(65.0f, 0.0f, 0.0f, 1.0f);

        // プレイヤーの方向を向く
        //this.transform.LookAt(target_transform);
    }

    public void GamePad_RightStick_CameraControl(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<Vector2>();
        inputHorizontal = value.x;
        inputVertical = value.y;
    }
}
