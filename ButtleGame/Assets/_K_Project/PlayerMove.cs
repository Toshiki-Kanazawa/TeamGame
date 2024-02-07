using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1.0f; // à⁄ìÆë¨ìx

    [SerializeField]
    private float horizontal; // â°

    [SerializeField]
    private float vertical; // èc

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
