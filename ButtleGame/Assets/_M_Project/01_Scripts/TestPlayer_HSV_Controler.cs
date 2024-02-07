using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer_HSV_Controler : MonoBehaviour
{
    //private Material material = null;

    [Header("設定は手動")]
    [Header("カスタムパーツ１")]
    public GameObject[] parts_custom1;
    [Header("カスタムパーツ２")]
    public GameObject[] parts_custom2;

    [Header("自動設定")]
    public Material[] parts_mat_custom1;
    public Material[] parts_mat_custom2;

    [Header("変更可能カラー：カスタムパーツ１")]
    [Header("カスタムカラー１：色彩")][Range(0f, 1f)]
    public float hue_1 = 0f;

    [Header("カスタムカラー１：彩度")][Range(0f, 1f)]
    public float sat_1 = 1f;

    [Header("カスタムカラー１：明度")][Range(0f, 1f)]
    public float val_1 = 1f;

    [Header("カラー変更不可：カスタムパーツ２")]
    [Header("カスタムカラー２：色彩")]
    const float hue_2 = 0f;

    [Header("カスタムカラー２：彩度")]
    const float sat_2 = 1f;

    [Header("カスタムカラー２：明度")]
    const float val_2 = 1f;


    // Use this for initialization
    void Start()
    {
        // パーツごとのマテリアルを格納する配列を作成
        parts_mat_custom1 = new Material[parts_custom1.Length];

        // マテリアルを配列に格納
        for (int i = 0; i < parts_custom1.Length; i++)
        {
            parts_mat_custom1[i] = parts_custom1[i].GetComponent<Renderer>().material;
        }

        // 固定カラー
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
        // 一気にマテリアルの色を変更
        for (int i = 0; i < parts_custom1.Length; i++)
        {
            parts_mat_custom1[i].SetFloat("_Hue", hue_1);
            parts_mat_custom1[i].SetFloat("_Sat", sat_1);
            parts_mat_custom1[i].SetFloat("_Val", val_1);
        }
    }
}
