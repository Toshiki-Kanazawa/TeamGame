using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExTag_PlayerParts : MonoBehaviour
{
    [Header("部位の名前：識別用")]
    [SerializeField]
    private string parts_name = "None";

    void Start()
    {
        
    }

    public GameObject GetObject()
    {
        return this.gameObject;
    }
}
