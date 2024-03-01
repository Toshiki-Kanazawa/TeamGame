using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultCameraMove : MonoBehaviour
{
    private Vector3 startPos = new Vector3(-5.8f,6.0f,-10.5f);
    private Vector3 endPos = new Vector3(1.4f,1.7f,-8.0f);

    [SerializeField]
    private GameObject spawnPos;

    private Vector3 target = Vector3.zero;

    [SerializeField]
    private float zoomSpeed;

    void Start()
    {
        float y = spawnPos.transform.position.y + 0.9f;
        target = new Vector3(spawnPos.transform.position.x, y, spawnPos.transform.position.z);

        transform.position = startPos;
    }

    void Update()
    {
        if (transform.position != endPos)
        {
            transform.position = Vector3.Lerp(transform.position, endPos, zoomSpeed * Time.deltaTime);
        }

        // ターゲットの設定
        transform.LookAt(target);
    }
}
