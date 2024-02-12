using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Init_InputSystem : MonoBehaviour
{
    private PlayerInput pi;

    void Start()
    {
        pi = this.GetComponent<PlayerInput>();

    }

    void Update()
    {
        
    }
}
