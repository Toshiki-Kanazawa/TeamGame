using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationComparator : MonoBehaviour
{
    [Header("参照するオブジェクト：プレイヤー")]
    public GameObject player;
    private PlayerMove player_move;
    private Animator player_anim;

    void Start()
    {
        // コンポーネントを取得
        player_move = player.GetComponent<PlayerMove>();
        player_anim = player.GetComponent<Animator>();
    }

    void Update()
    {
        player_anim.SetBool("isMoving", player_move.isMoving);
        player_anim.SetBool("isAttack", player_move.isAttack);
        player_anim.SetBool("isJumping", player_move.isJumping);
        player_anim.SetBool("isSecondJump", player_move.isSecondJump);

        //player_anim.SetInteger("moveDir", (int)moveDir);
    }
}
