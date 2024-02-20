using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_PlayerHitManager : MonoBehaviour
{
    private M_CharactorStatus cs;

    void Start()
    {

        cs = GetComponentInParent<M_CharactorStatus>();
    }

    void Update()
    {
        
    }

    /* 改良 */
    // 武器との接触判定を取る
    // Triggerオンとオフは接触出来ない
    // プレイヤーの空のオブジェクトにヒット判定用のTriggerオブジェクトを作る
    private void OnTriggerEnter(Collider other)
    {
        System_HitBall sh = other.gameObject.GetComponent<System_HitBall>();

        if (sh)
        {
            cs.TakeDamage(sh.atk);
            Debug.Log(cs.GetHitPoint());
        }
    }
}
