using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Charactor_Controler : MonoBehaviour
{
    public float speed = 3;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0.0f, 0.0f, speed) * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0.0f, 0.0f, -speed) * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-speed, 0.0f, 0.0f) * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(speed, 0.0f, 0.0f) * Time.deltaTime;
        }
    }
}
