using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [SerializeField, Tooltip("�C�� or �A�E�g")]
    public enum enInitState
    {
        In,
        Out,
    }

    [SerializeField,Tooltip("�g�k���x")]
    private float scaleSpeed = 1.0f;

    [HideInInspector] public bool finish_f = false;

    public bool execute_f = false;

    public enInitState initState;

    [SerializeField, Tooltip("�k���ŏ��l")]
    private float minScale = 0.0f;

    [SerializeField, Tooltip("�g��ő�l")]
    private float maxScale = 10.0f;

    void Start()
    {
        switch (initState)
        {
            case enInitState.In:
                // �ŏ�������
                transform.localScale = new Vector2(minScale, minScale);
                break;
            case enInitState.Out:
                // �ő剻����
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
        if (!execute_f) return; // ���s�t���O
        if (finish_f) return;   // �I���t���O

        // �Q�Ɓi�Ō�ɑ������j
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
                    execute_f = false;
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
                    execute_f = false;
                }
                break;
        }

        // ���
        transform.localScale = scale;
    }
}
