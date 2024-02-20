using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_HitBall : MonoBehaviour
{
    [Header("�����ݒ�F���������e�I�u�W�F�N�g")]
    public GameObject creator = null;

    [Header("�����ݒ�F�ڐG�����I�u�W�F�N�g")]
    public GameObject[] targets;
   
    [Header("�q�b�g�����񐔁F�z��ǉ��p")]
    public int HitCount_Target = 0;

    [Header("�^����_���[�W")]
    public int atk = 10;

    // �Փ˂������ǂ����̃t���O
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
        // ����͉����ɐڐG����� true
        hit = true;
        Debug.Log("Hit");
        this.gameObject.GetComponent<Renderer>().material.color = new Color(1.0f, 0.0f, 0.0f);
    }

}
