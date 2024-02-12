using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_PlayerManager : MonoBehaviour
{
    [Header("動作クラス")]
    [SerializeField]
    private M_PlayerMove pl_Move;
    [SerializeField]
    private M_PlayerAttack pl_Attack;
    [SerializeField]
    private PlayerAnimationComparator pl_AnimCompo;

    [Header("構成パーツ")]
    [SerializeField]
    private GameObject[] pl_Parts;
    [SerializeField]
    private Transform[] pl_Parts_Pos;


    void Start()
    {
        // 動作クラスのコンポーネント取得
        pl_Move = this.gameObject.GetComponent<M_PlayerMove>();
        pl_Attack = this.gameObject.GetComponent<M_PlayerAttack>();
        pl_AnimCompo = this.gameObject.GetComponent<PlayerAnimationComparator>();

        // 構成パーツのゲームオブジェクトの取得
        // ExTag のコンポーネントを持つ子オブジェクトを検索する
        ExTag_PlayerParts[] tmp = GetComponentsInChildren<ExTag_PlayerParts>();
        int count = tmp.Length;

        // ExTagの数で配列数でメモリに載せる
        pl_Parts = new GameObject[count];
        pl_Parts_Pos = new Transform[count];

        for (int i = 0; i < count - 1; i++)
        {
            // ExTag を持つオブジェクトを代入
            pl_Parts[i] = tmp[i].gameObject;

            // 座標情報をコピー
            pl_Parts_Pos[i] = pl_Parts[i].transform;
        }
    }

    void Update()
    {

    }
}
