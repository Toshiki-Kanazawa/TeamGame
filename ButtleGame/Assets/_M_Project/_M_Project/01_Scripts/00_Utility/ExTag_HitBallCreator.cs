using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExTag_HitBallCreator : MonoBehaviour
{
    [Header("部位の名前：識別用")]
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
