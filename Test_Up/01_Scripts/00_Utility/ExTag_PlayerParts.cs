using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExTag_PlayerParts : MonoBehaviour
{
    public enum eTag
    {
        None,
        CustomParts,
        Weapons,
    }

    [Header("部位の名前：識別用")]
    [SerializeField]
    private string parts_name = "None";
    [Header("部位の色変更が可能かどうか")]
    public bool customColor = false;
    public eTag tag = eTag.None;


    void Start()
    {
        
    }

    public GameObject GetObject()
    {
        return this.gameObject;
    }
}
