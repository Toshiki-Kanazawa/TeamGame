using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [SerializeField, Tooltip("イン or アウト")]
    public enum enInitState
    {
        In,
        Out,
    }

    [SerializeField,Tooltip("拡縮速度")]
    private float scaleSpeed = 1.0f;

    [HideInInspector] public bool finish_f = false;

    public bool execute_f = false;

    public enInitState initState;

    [SerializeField, Tooltip("縮小最小値")]
    private float minScale = 0.0f;

    [SerializeField, Tooltip("拡大最大値")]
    private float maxScale = 10.0f;

    void Start()
    {
        switch (initState)
        {
            case enInitState.In:
                // 最小化する
                transform.localScale = new Vector2(minScale, minScale);
                break;
            case enInitState.Out:
                // 最大化する
                transform.localScale = new Vector2(maxScale, maxScale);
                break;
        }
    }

    void Update()
    {
        ScaleMove(initState);
    }

    public void ScaleMove(enInitState state)
    {
        if (!execute_f) return;
        if (finish_f) return;

        // 参照（最後に代入する）
        var scale = transform.localScale;

        switch(state)
        {
            case enInitState.In:
                if(scale.x < maxScale && scale.y < maxScale)
                {
                    scale.x += scaleSpeed;
                    scale.y += scaleSpeed;
                }
                else
                {
                    scale.x = maxScale;
                    scale.y = maxScale;
                    finish_f = true;
                }
                break;
            case enInitState.Out:
                if (scale.x > minScale && scale.y > minScale)
                {
                    scale.x -= scaleSpeed;
                    scale.y -= scaleSpeed;
                }
                else
                {
                    scale.x = minScale;
                    scale.y = minScale;
                    finish_f = true;
                }
                break;
        }

        // 代入
        transform.localScale = scale;
    }
}
