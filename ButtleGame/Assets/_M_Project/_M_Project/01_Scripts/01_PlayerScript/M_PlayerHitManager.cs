using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_PlayerHitManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* 改良 */
    // 武器との接触判定を取る
    // Triggerオンとオフは接触出来ない
    // プレイヤーの空のオブジェクトにヒット判定用のTriggerオブジェクトを作る
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<System_HitBall>())
        {
            Debug.Log("lllllllllllllllllllllllllllllllllllllllll");
            this.gameObject.SetActive(false);
        }
    }
}
