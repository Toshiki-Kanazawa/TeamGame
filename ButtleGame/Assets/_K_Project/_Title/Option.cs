using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour
{
    // --------------------------------------------------------------------------------------------------
    // �C���X�y�N�^�[�Őݒ�ł���ϐ�

    [SerializeField] private Cursor optionCursor;

    public Slider BGMslider;
    public Slider SEslider;
    [SerializeField] private float BGMVolume;
    [SerializeField] private float SEVolume;
    [SerializeField] private float timeInterval = 0.1f;
    [SerializeField] private float startBGMValue = 0.5f;
    [SerializeField] private float startSEValue = 0.5f;
    [SerializeField] private const float sliderMax = 1.0f;
    [SerializeField] private const float sliderMin = 0.0f;
    [SerializeField] private const float addValue = 0.01f;
    [SerializeField] private TitleManager titleManager;
    [SerializeField] private KeyCode inputKey = KeyCode.Space;

    //public static float GetBGMVolume() => option.GetBGMValue_();
    //public static float GetSEVolume() => option.GetBGMValue_();



    // --------------------------------------------------------------------------------------------------
    // �v���C�x�[�g�ϐ�

    private float time;

    // --------------------------------------------------------------------------------------------------
    // Unity �֐�
    
    void Start()
    {
        BGMVolume = startBGMValue;
        SEVolume = startSEValue;
        Debug.Log(titleManager);
    }

    void Update()
    {
        /// �J�[�\���N���X�� List �� text �𒲐�����
        
        //if (titleManager.GetState() == TitleManager.State.enState.OPTION)
        {
            int cursor = optionCursor.cursor;

            Debug.Log("Option");
            if (cursor == 0)
            {
                BGMVolume = Slider(BGMVolume);
                BGMslider.value = BGMVolume;
            }
            if (cursor == 1)
            {
                SEVolume = Slider(SEVolume);
                SEslider.value = SEVolume;
            }
            if (cursor == 2 && Input.GetKeyDown(inputKey))
            {
                SceneManager.LoadScene("Title");
                optionCursor.CursorReset();
            }
            if (cursor == 3 && Input.GetKeyDown(inputKey))
            {
                //titleManager.OnCancel();
                optionCursor.CursorReset();
            }
        }
    }


    // --------------------------------------------------------------------------------------------------
    // ����֐�

    // StateManagr�N���X�ɂ킽���p�̊֐�
    public void StateUpdate()
    {

    }

    private float Slider(float sliderValue)
    {
        time += Time.deltaTime;

        if (time > timeInterval)
        {
            if (Input.GetKey(KeyCode.RightArrow) && sliderValue <= sliderMax)
            {
                sliderValue += addValue;
                time = 0;
            }

            if (Input.GetKey(KeyCode.LeftArrow) && sliderValue >= sliderMin)
            {
                sliderValue -= addValue;
                time = 0;
            }
        }
        return sliderValue;
    }

    public float GetBGMValue_()
    {
        return BGMslider.value;
    }
    public float GetSEValue_()
    {
        return SEslider.value;
    }
}
