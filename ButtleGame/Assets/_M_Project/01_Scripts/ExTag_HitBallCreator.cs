using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExTag_HitBallCreator : MonoBehaviour
{
    [Header("•”ˆÊ‚Ì–¼‘OF¯•Ê—p")]
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
