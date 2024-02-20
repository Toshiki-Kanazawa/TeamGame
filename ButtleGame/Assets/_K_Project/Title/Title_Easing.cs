using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title_Easing : MonoBehaviour
{
    // �C���X�y�N�^�ŕύX�ł���l
    [SerializeField] private float initX = 0.0f;      // �����ʒux
    [SerializeField] private float moveSpeed = 50.0f; // �ړ����x
    [SerializeField] private float initScale = 0.2f;  // �����X�P�[��

    // �v���C�x�[�g�ϐ�
    private bool directing_f; // true�����o��

    // �C�[�W���O�֘A
    private float easingTime = 0;         // �C�[�W���O�o�ߎ���
    private float easingTotalTime = 1.0f; // �C�[�W���O�I������
    private bool easing_f = false;     // true���C�[�W���O��
    private bool easingFinish = false; // true���C�[�W���O�I��

    void Start()
    {
        initialize();
    }

    void Update()
    {
        if (!directing_f) return;

        // ���S�ɗ���܂ňړ�
        if (transform.position.x < Screen.width * 0.5f)
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f);
        }
        else
        {
            // �C�[�W���O�֐��Ŋg��
            Easing();
        }
    }

    /// <summary>
    /// ������
    /// </summary>
    private void initialize()
    {
        // �����ʒu��ݒ�
        var pos = transform.position;
        pos.x = initX;
        transform.position = pos;

        // ���o�t���O��true
        directing_f = true;

        // �����X�P�[����ݒ�
        transform.localScale = new Vector3(initScale, initScale, 0);
    }

    /// <summary>
    /// �C�[�W���O�֐��Ăяo��
    /// </summary>
    void Easing()
    {
        // �C�[�W���O�I�����Ă���return
        if (easingFinish) return;

        easing_f = true;

        // �J�E���g�𓮂���
        easingTime += 1 * Time.deltaTime;

        // �J�E���g�𒴂�����I��
        if (easingTime > easingTotalTime)
        {
            easingFinish = true;
            easing_f = false;
            directing_f = false;
            easingTime = easingTotalTime;
        }
        // �C�[�W���O�g��
        float easingScale = OutBounce(easingTime, easingTotalTime, 1.0f, 0.1f);
        transform.localScale = new Vector3(easingScale, easingScale, 0);
    }


    /// <summary>
    /// �C�[�W���O�֐� (�A�E�g�o�E���X)
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
