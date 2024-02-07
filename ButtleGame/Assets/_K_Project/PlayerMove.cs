using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1.0f; // �ړ����x

    [SerializeField]
    private float horizontal; // ��

    [SerializeField]
    private float vertical; // �c

    void Start()
    {
        
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        vertical = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        this.transform.position += new Vector3(horizontal, 0, vertical);
    }
}
