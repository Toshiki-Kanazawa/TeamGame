using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_DamageBall_Controler : MonoBehaviour
{
    // 生成した親オブジェクト
    public GameObject creator;

    // 衝突しているかどうかの判定
    public bool hit;

    void Start()
    {
        hit = false;
    }

    void Update()
    {
        
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
        //this.gameObject.GetComponent<Material>().color = new Color(1.0f, 0.0f, 0.0f);
        this.gameObject.GetComponent<Renderer>().material.color = new Color(1.0f, 0.0f, 0.0f);
    }
}
