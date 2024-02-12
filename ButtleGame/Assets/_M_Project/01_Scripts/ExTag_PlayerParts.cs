using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExTag_PlayerParts : MonoBehaviour
{
    [Header("•”ˆÊ‚Ì–¼‘OF¯•Ê—p")]
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
