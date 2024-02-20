using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_PlayerManager : MonoBehaviour
{
    [Header("所有するカメラ：自動設定")]
    [SerializeField]
    public GameObject cam;
    [Header("カメラプレファブ")]
    public GameObject cam_prefab;

    [Header("動作クラス")]
    [SerializeField]
    private M_PlayerMove pl_Move;
    [SerializeField]
    private M_PlayerAttack pl_Attack;
    [SerializeField]
    private PlayerAnimationComparator pl_AnimCompo;
    [SerializeField]
    private M_CharactorStatus pl_Status;
    //[SerializeField]
    //private M_HSV_Controler hsv;

    [Header("構成パーツ")]
    [SerializeField]
    private GameObject[] pl_Parts;
    [SerializeField]
    private Transform[] pl_Parts_Pos;


    void Start()
    {
        // カメラを生成する
        //cam = Instantiate(cam_prefab);
        //cam.GetComponent<SeekCamera>().SetParent(this.gameObject);

        // 動作クラスのコンポーネント取得
        pl_Move = GetComponent<M_PlayerMove>();
        pl_Attack = GetComponent<M_PlayerAttack>();
        pl_AnimCompo = GetComponent<PlayerAnimationComparator>();
        pl_Status = GetComponent<M_CharactorStatus>();
        //hsv = GetComponent<M_HSV_Controler>();

        // カメラを設定
        //pl_Move.SetPlayerCamera(cam.GetComponent<Camera>());

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


    public GameObject[] GetParts()
    {
        return pl_Parts;
    }
}
