using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExTag_HitBallCreator : MonoBehaviour
{
    [Header("���ʂ̖��O�F���ʗp")]
    [SerializeField]
    private string pos_name = "None";

    void Start()
    {

    }

    public GameObject GetAttackPosObject()
    {
        return this.gameObject;
    }
}
