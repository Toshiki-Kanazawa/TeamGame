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

    [Header("���ʂ̖��O�F���ʗp")]
    [SerializeField]
    private string parts_name = "None";
    [Header("���ʂ̐F�ύX���\���ǂ���")]
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
