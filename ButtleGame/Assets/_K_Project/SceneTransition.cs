using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

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

    // フェード中のフラグ
    public bool IsTransition { get; private set; } = false;

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

    public async UniTask Execute(enInitState state)
    {
        if (finish_f) return;   // 終了フラグ
        Debug.Log("トランジション中");
        IsTransition = true;

        // 参照（最後に代入する）
        var scale = transform.localScale;

        switch(state)
        {
            case enInitState.In:
                if(scale.x < maxScale && scale.y < maxScale)
                {
                    scale.x += scaleSpeed * Time.deltaTime;
                    scale.y += scaleSpeed * Time.deltaTime;
                }
                else
                {
                    scale.x = maxScale;
                    scale.y = maxScale;
                    finish_f = true;
                    IsTransition = false;
                    Debug.Log("トランジション終了");
                }
                break;
            case enInitState.Out:
                if (scale.x > minScale && scale.y > minScale)
                {
                    scale.x -= scaleSpeed * Time.deltaTime;
                    scale.y -= scaleSpeed * Time.deltaTime;
                }
                else
                {
                    scale.x = minScale;
                    scale.y = minScale;
                    finish_f = true;
                    IsTransition = false;
                    Debug.Log("トランジション終了");
                }
                break;
        }

        // 代入
        transform.localScale = scale;
    }
}
