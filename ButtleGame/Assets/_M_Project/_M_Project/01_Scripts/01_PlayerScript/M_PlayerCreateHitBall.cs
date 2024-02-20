using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_PlayerCreateHitBall : MonoBehaviour
{
    [Header("親オブジェクトのマネージャー")]
    [SerializeField]
    private M_PlayerManager pl_MGR;

    [Header("自動設定：出現させる場所：ExTag_HitBallCreator")]
    [SerializeField]
    private GameObject[] HitBall_Creators;
    [SerializeField]
    private Transform[] HitBall_CreatePos;

    [Header("攻撃判定のプレハブ")]
    public GameObject HitBall_Prefab;
    private GameObject tmp_HitBall;


    void Start()
    {
        // マネージャーの取得
        pl_MGR = this.gameObject.GetComponent<M_PlayerManager>();

        // ExTag のコンポーネントを持つ子オブジェクトを検索する
        ExTag_HitBallCreator[] tmp = GetComponentsInChildren<ExTag_HitBallCreator>();
        int count = tmp.Length;

        // ExTagの数で配列数でメモリに載せる
        HitBall_Creators = new GameObject[count];
        HitBall_CreatePos = new Transform[count];

        for (int i = 0; i < count; i++)
        {
            // ExTag を持つオブジェクトを代入
            HitBall_Creators[i] = tmp[i].gameObject;

            // 座標情報をコピー
            HitBall_CreatePos[i] = HitBall_Creators[i].transform;
        }
    }

    void Update()
    {
        
    }

    public void CreateHitBall()
    {
        // ヒット判定を作成
        tmp_HitBall = null;
        tmp_HitBall = Instantiate(HitBall_Prefab);

        // ヒット判定の所有者を登録：識別用
        tmp_HitBall.GetComponent<System_HitBall>().SetCreator(this.gameObject);

        // 当たり判定ボールをヒットPosに追従させる
        tmp_HitBall.transform.position = HitBall_CreatePos[0].transform.position;
        tmp_HitBall.transform.rotation = HitBall_CreatePos[0].transform.rotation;
        tmp_HitBall.transform.SetParent(HitBall_Creators[0].transform);    // ボールの親を設定(判定追従用)
    }

    public void DeleteHitBall()
    {
        // ヒット判定を削除する
        Destroy(tmp_HitBall);
        tmp_HitBall = null;     // クリアしておく
    }

}
