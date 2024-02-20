using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_HitBall : MonoBehaviour
{
    [Header("自動設定：生成した親オブジェクト")]
    public GameObject creator = null;

    [Header("自動設定：接触したオブジェクト")]
    public GameObject[] targets;
   
    [Header("ヒットした回数：配列追加用")]
    public int HitCount_Target = 0;

    [Header("与えるダメージ")]
    public int atk = 10;

    // 衝突したかどうかのフラグ
    public bool hit;

    void Start()
    {
        hit = false;
    }

    public void SetCreator(GameObject gameObject)
    {
        creator = gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 現状は何かに接触すれば true
        hit = true;
        Debug.Log("Hit");
        this.gameObject.GetComponent<Renderer>().material.color = new Color(1.0f, 0.0f, 0.0f);
    }

}
