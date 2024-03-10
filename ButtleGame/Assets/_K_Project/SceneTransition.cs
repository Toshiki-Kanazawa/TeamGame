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

    [SerializeField, Tooltip("�g�k���x")]
    private float scaleSpeed = 1.0f;

    public bool finish_f { get; private set; } = false;

    // �t�F�[�h���̃t���O
    public bool IsTransition { get; private set; } = false;

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

    public void Execute(enInitState state)
    {
        Debug.Log("�g�����W�V������");
        IsTransition = true;

        // �Q�Ɓi�Ō�ɑ������j
        var scale = transform.localScale;

        switch (state)
        {
            case enInitState.In: // �V�[������ �i�ۂ��k������j
                if (scale.x < maxScale && scale.y < maxScale)
                {
                    scale.x -= scaleSpeed * Time.deltaTime;
                    scale.y -= scaleSpeed * Time.deltaTime;
                    Debug.Log("���s��");
                }
                else
                {
                    scale.x = minScale;
                    scale.y = minScale;
                    finish_f = true;
                    IsTransition = false;
                    Debug.Log("�C�� �g�����W�V�����I��");
                }
                break;
            case enInitState.Out: // �V�[���I�� �i�ۂ��g�傷��j
                if (scale.x > minScale && scale.y > minScale)
                {
                    scale.x += scaleSpeed * Time.deltaTime;
                    scale.y += scaleSpeed * Time.deltaTime;

                    Debug.Log("���s��");
                }
                else
                {
                    scale.x = maxScale;
                    scale.y = maxScale;
                    finish_f = true;
                    IsTransition = false;
                    Debug.Log("�A�E�g �g�����W�V�����I��");
                }
                break;
        }

        // ���
        transform.localScale = scale;
    }
}