using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title_Easing : MonoBehaviour
{
    // インスペクタで変更できる値
    [SerializeField] private float initX = 0.0f;      // 初期位置x
    [SerializeField] private float moveSpeed = 50.0f; // 移動速度
    [SerializeField] private float initScale = 0.2f;  // 初期スケール

    // プライベート変数
    private bool directing_f; // true→演出中

    // イージング関連
    private float easingTime = 0;         // イージング経過時間
    private float easingTotalTime = 1.0f; // イージング終了時間
    private bool easing_f = false;     // true→イージング中
    private bool easingFinish = false; // true→イージング終了

    void Start()
    {
        initialize();
    }

    void Update()
    {
        if (!directing_f) return;

        // 中心に来るまで移動
        if (transform.position.x < Screen.width * 0.5f)
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f);
        }
        else
        {
            // イージング関数で拡大
            Easing();
        }
    }

    /// <summary>
    /// 初期化
    /// </summary>
    private void initialize()
    {
        // 初期位置を設定
        var pos = transform.position;
        pos.x = initX;
        transform.position = pos;

        // 演出フラグをtrue
        directing_f = true;

        // 初期スケールを設定
        transform.localScale = new Vector3(initScale, initScale, 0);
    }

    /// <summary>
    /// イージング関数呼び出し
    /// </summary>
    void Easing()
    {
        // イージング終了してたらreturn
        if (easingFinish) return;

        easing_f = true;

        // カウントを動かす
        easingTime += 1 * Time.deltaTime;

        // カウントを超えたら終了
        if (easingTime > easingTotalTime)
        {
            easingFinish = true;
            easing_f = false;
            directing_f = false;
            easingTime = easingTotalTime;
        }
        // イージング拡大
        float easingScale = OutBounce(easingTime, easingTotalTime, 1.0f, 0.1f);
        transform.localScale = new Vector3(easingScale, easingScale, 0);
    }


    /// <summary>
    /// イージング関数 (アウトバウンス)
    /// </summary>
    public static float OutBounce(float time, float totaltime, float max = 1f, float min = 0f)
    {
        float _2_75 = 2.75f;
        float _7_5625 = 7.5625f;
        float _0_75 = 0.75f;
        float _0_9375 = 0.9375f;
        float _0_984375 = 0.984375f;

        max -= min;
        time /= totaltime;

        if (time < 1f / _2_75)
            return max * (_7_5625 * time * time) + min;
        else if (time < 2f / _2_75)
        {
            time -= 1.5f / _2_75;
            return max * (_7_5625 * time * time + _0_75) + min;
        }
        else if (time < 2.5f / _2_75)
        {
            time -= 2.25f / _2_75;
            return max * (_7_5625 * time * time + _0_9375) + min;
        }
        else
        {
            time -= 2.625f / _2_75;
            return max * (_7_5625 * time * time + _0_984375) + min;
        }
    }
}
