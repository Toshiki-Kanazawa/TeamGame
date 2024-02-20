using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �v���C���[��HP���Q�Ƃ���Image��fillAmount��ݒ�
/// </summary>
public class HPSystem : MonoBehaviour
{
    [SerializeField]Image image;

    [SerializeField]private M_CharactorStatus status;

    void Update()
    {
        var current = status.GetHitPoint();
        var max = status.GetHitPointMax();

        HPDown(current,max);
    }

    /// <summary>
    /// HP��������
    /// </summary>
    public void HPDown(int current, int max)
    {
        // HP��0�`1�ɂ���fillAmount�ɐݒ�
        image.fillAmount = Mathf.Clamp01((float)current / max);
    }
}
