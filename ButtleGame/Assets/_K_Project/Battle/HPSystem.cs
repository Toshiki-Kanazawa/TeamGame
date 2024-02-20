using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プレイヤーのHPを参照してImageのfillAmountを設定
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
    /// HPを下げる
    /// </summary>
    public void HPDown(int current, int max)
    {
        // HPを0～1にしてfillAmountに設定
        image.fillAmount = Mathf.Clamp01((float)current / max);
    }
}
