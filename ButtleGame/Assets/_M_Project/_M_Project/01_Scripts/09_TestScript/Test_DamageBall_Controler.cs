using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_DamageBall_Controler : MonoBehaviour
{
    // ���������e�I�u�W�F�N�g
    public GameObject creator;

    // �Փ˂��Ă��邩�ǂ����̔���
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
        // ����͉����ɐڐG����� true
        hit = true;
        Debug.Log("Hit");
        //this.gameObject.GetComponent<Material>().color = new Color(1.0f, 0.0f, 0.0f);
        this.gameObject.GetComponent<Renderer>().material.color = new Color(1.0f, 0.0f, 0.0f);
    }
}
