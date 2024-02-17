using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title_Easing : MonoBehaviour
{
    // 初期位置
    [SerializeField] private float initX = 0.0f;
    private float initY = Screen.height * 0.5f;
    public float easingTime = 0;
    private float easingTotalTime = 1.0f;

    [SerializeField] private float moveSpeed = 50.0f; 
    [SerializeField] private float initScale = 0.2f; 

    void Start()
    {
        // 初期位置を設定
        transform.position = new Vector3(initX,initY,0);
        transform.localScale = new Vector3(initScale,initScale,0);

        InvokeRepeating("OutBounce",0,1.0f);
    }

    void Update()
    {



        // 中心に来るまで移動
        if (transform.position.x < Screen.width * 0.5f)
        {
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0.0f,0.0f);
        }
        else
        {
            easingTime += 1 * Time.deltaTime;

            if(easingTime > easingTotalTime)
            {
                easingTime = easingTotalTime;
            }
            // 中心に来たらイージング拡大する
            float easingScale = OutBounce(easingTime, easingTotalTime, 1.0f, 0.1f);
            transform.localScale = new Vector3(easingScale,easingScale,0);
        }
    }

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
