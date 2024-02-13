using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title_Easing : MonoBehaviour
{
    void Start()
    {
        // 左下を原点とするスクリーン座標を取得
        float screenX = 0.0f; // X座標を中心にするため0
        float screenY = 0.0f; // Y座標を中心にするため0

        // 3D座標に変換
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenX, screenY, Camera.main.nearClipPlane));
    }

    void Update()
    {
        
    }
}
