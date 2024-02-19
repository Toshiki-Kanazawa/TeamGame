using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_HSV_Controler : MonoBehaviour
{
    [Header("設定は手動")]
    [Header("カスタムパーツ１")]
    public GameObject[] parts_custom1;
    [Header("カスタムパーツ２")]
    public GameObject[] parts_custom2;

    [Header("自動設定")]
    public Material[] parts_mat_custom1;
    public Material[] parts_mat_custom2;
    public int custom1_count = 0;
    public int custom2_count = 0;

    [Header("変更可能カラー：カスタムパーツ１")]
    [Header("カスタムカラー１：色彩")]
    [Range(0f, 1f)]
    public float hue_1 = 0f;

    [Header("カスタムカラー１：彩度")]
    [Range(0f, 1f)]
    public float sat_1 = 1f;

    [Header("カスタムカラー１：明度")]
    [Range(0f, 1f)]
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
        //// マネージャーから構成パーツを取得
        //GameObject[] tmp_obj = GetComponent<M_PlayerManager>().GetParts();
        //int count = tmp_obj.Length;

        // 構成パーツのゲームオブジェクトの取得
        // ExTag のコンポーネントを持つ子オブジェクトを検索する
        ExTag_PlayerParts[] tmp = GetComponentsInChildren<ExTag_PlayerParts>();
        int count = tmp.Length;

        custom1_count = 0;
        custom2_count = 0;

        // 取得したパーツがカスタム可能か判断する
        for (int i = 0; i < count; i++)
        {
            // カスタムパーツ以外は排除
            if (tmp[i].tag != ExTag_PlayerParts.eTag.CustomParts) continue;

            // カスタム出来る場合
            if (tmp[i].customColor == true)
            {
                custom1_count++;
            }
            else
            {
                custom2_count++;
            }
        }

        // パーツごとのマテリアルを格納する配列を作成
        // 変更可能カラー
        parts_custom1 = new GameObject[custom1_count];
        // 固定カラー
        parts_custom2 = new GameObject[custom2_count];

        // 挿入用変数
        int insert1 = 0;
        int insert2 = 0;

        // カスタムパーツ毎に変数に格納する
        for (int i = 0; i < (custom1_count + custom2_count); i++)
        {
            if (tmp[i].GetComponent<ExTag_PlayerParts>().customColor == true)
            {
                parts_custom1[insert1] = tmp[i].gameObject;
                insert1++;
            }
            else
            {
                parts_custom2[insert2] = tmp[i].gameObject;
                insert2++;
            }
        }

        // パーツごとのマテリアルを格納する配列を作成
        // 変更可能カラー
        parts_mat_custom1 = new Material[custom1_count];
        // 固定カラー
        parts_mat_custom2 = new Material[custom2_count];


        // マテリアルを配列に格納
        for (int i = 0; i < custom1_count; i++)
        {
            parts_mat_custom1[i] = parts_custom1[i].GetComponent<Renderer>().material;
        }


        for (int i = 0; i < custom2_count; i++)
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
        for (int i = 0; i < custom1_count; i++)
        {
            parts_mat_custom1[i].SetFloat("_Hue", hue_1);
            parts_mat_custom1[i].SetFloat("_Sat", sat_1);
            parts_mat_custom1[i].SetFloat("_Val", val_1);
        }
    }

    public void InitParts_HSV()
    {
        // マネージャーから構成パーツを取得
        GameObject[] tmp_obj = GetComponent<M_PlayerManager>().GetParts();
        int count = tmp_obj.Length;

        // 取得したパーツがカスタム可能か判断する
        for (int i = 0; i < count; i++)
        {
            // カスタム出来る場合
            if (tmp_obj[i].GetComponent<ExTag_PlayerParts>().customColor == true)
            {
                custom1_count++;
            }
            else
            {
                custom2_count++;
            }
        }

        // パーツごとのマテリアルを格納する配列を作成
        // 変更可能カラー
        parts_custom1 = new GameObject[custom1_count];
        // 固定カラー
        parts_custom2 = new GameObject[custom2_count];

        // 挿入用変数
        int insert1 = 0;
        int insert2 = 0;

        // カスタムパーツ毎に変数に格納する
        for (int i = 0; i < count; i++)
        {
            if (tmp_obj[i].GetComponent<ExTag_PlayerParts>().customColor == true)
            {
                parts_custom1[insert1] = tmp_obj[i];
                insert1++;
            }
            else
            {
                parts_custom2[insert2] = tmp_obj[i];
                insert2++;
            }
        }

        // パーツごとのマテリアルを格納する配列を作成
        // 変更可能カラー
        parts_mat_custom1 = new Material[custom1_count];
        // 固定カラー
        parts_mat_custom2 = new Material[custom2_count];


        // マテリアルを配列に格納
        for (int i = 0; i < custom1_count; i++)
        {
            parts_mat_custom1[i] = parts_custom1[i].GetComponent<Renderer>().material;
        }


        for (int i = 0; i < custom2_count; i++)
        {
            parts_mat_custom2[i] = parts_custom2[i].GetComponent<Renderer>().material;
            parts_mat_custom2[i].SetFloat("_Hue", hue_2);
            parts_mat_custom2[i].SetFloat("_Sat", sat_2);
            parts_mat_custom2[i].SetFloat("_Val", val_2);
        }
    }
}