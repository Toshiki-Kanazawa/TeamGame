using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer_HSV_Controler : MonoBehaviour
{
    //private Material material = null;

    [Header("�ݒ�͎蓮")]
    [Header("�J�X�^���p�[�c�P")]
    public GameObject[] parts_custom1;
    [Header("�J�X�^���p�[�c�Q")]
    public GameObject[] parts_custom2;

    [Header("�����ݒ�")]
    public Material[] parts_mat_custom1;
    public Material[] parts_mat_custom2;

    [Header("�ύX�\�J���[�F�J�X�^���p�[�c�P")]
    [Header("�J�X�^���J���[�P�F�F��")][Range(0f, 1f)]
    public float hue_1 = 0f;

    [Header("�J�X�^���J���[�P�F�ʓx")][Range(0f, 1f)]
    public float sat_1 = 1f;

    [Header("�J�X�^���J���[�P�F���x")][Range(0f, 1f)]
    public float val_1 = 1f;

    [Header("�J���[�ύX�s�F�J�X�^���p�[�c�Q")]
    [Header("�J�X�^���J���[�Q�F�F��")]
    const float hue_2 = 0f;

    [Header("�J�X�^���J���[�Q�F�ʓx")]
    const float sat_2 = 1f;

    [Header("�J�X�^���J���[�Q�F���x")]
    const float val_2 = 1f;


    // Use this for initialization
    void Start()
    {
        // �p�[�c���Ƃ̃}�e���A�����i�[����z����쐬
        parts_mat_custom1 = new Material[parts_custom1.Length];

        // �}�e���A����z��Ɋi�[
        for (int i = 0; i < parts_custom1.Length; i++)
        {
            parts_mat_custom1[i] = parts_custom1[i].GetComponent<Renderer>().material;
        }

        // �Œ�J���[
        parts_mat_custom2 = new Material[parts_custom2.Length];

        for (int i = 0; i < parts_custom2.Length; i++)
        {
            parts_mat_custom2[i] = parts_custom2[i].GetComponent<Renderer>().material;
            parts_mat_custom2[i].SetFloat("_Hue", hue_2);
            parts_mat_custom2[i].SetFloat("_Sat", sat_2);
            parts_mat_custom2[i].SetFloat("_Val", val_2);
        }



    }

    // Update is called once per frame
    void Update()
    {
        // ��C�Ƀ}�e���A���̐F��ύX
        for (int i = 0; i < parts_custom1.Length; i++)
        {
            parts_mat_custom1[i].SetFloat("_Hue", hue_1);
            parts_mat_custom1[i].SetFloat("_Sat", sat_1);
            parts_mat_custom1[i].SetFloat("_Val", val_1);
        }
    }
}
