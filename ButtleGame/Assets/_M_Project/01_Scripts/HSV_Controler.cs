using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSV_Controler : MonoBehaviour
{
    private Material material = null;

    [Header("êFç ")]
    [Range(0f, 1f)]
    public float hue = 0f;

    [Header("ç ìx")]
    [Range(0f, 1f)]
    public float sat = 1f;

    [Header("ñæìx")]
    [Range(0f, 1f)]
    public float val = 1f;

    // Use this for initialization
    void Start()
    {
        this.material = gameObject.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        this.material.SetFloat("_Hue", hue);
        this.material.SetFloat("_Sat", sat);
        this.material.SetFloat("_Val", val);
    }
}
