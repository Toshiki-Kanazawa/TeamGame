using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float amplitude = 1.0f; // 振幅
    public float frequency = 1.0f; // 周波数

    private float initY; // 初期Y座標

    void Start()
    {
        initY = transform.position.y; // 初期Y座標を記録
    }

    void Update()
    {
        // オブジェクトを上下に動かす
        float posY = initY + amplitude * Mathf.Sin(Time.time * frequency);
        transform.position = new Vector3(transform.position.x, posY, transform.position.z);
    }
}
