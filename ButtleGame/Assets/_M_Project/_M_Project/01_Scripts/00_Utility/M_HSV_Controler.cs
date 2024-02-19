using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_HSV_Controler : MonoBehaviour
{
    [Header("�ݒ�͎蓮")]
    [Header("�J�X�^���p�[�c�P")]
    public GameObject[] parts_custom1;
    [Header("�J�X�^���p�[�c�Q")]
    public GameObject[] parts_custom2;

    [Header("�����ݒ�")]
    public Material[] parts_mat_custom1;
    public Material[] parts_mat_custom2;
    public int custom1_count = 0;
    public int custom2_count = 0;

    [Header("�ύX�\�J���[�F�J�X�^���p�[�c�P")]
    [Header("�J�X�^���J���[�P�F�F��")]
    [Range(0f, 1f)]
    public float hue_1 = 0f;

    [Header("�J�X�^���J���[�P�F�ʓx")]
    [Range(0f, 1f)]
    public float sat_1 = 1f;

    [Header("�J�X�^���J���[�P�F���x")]
    [Range(0f, 1f)]
    public float val_1 = 1f;

    [Header("�J���[�ύX�s�F�J�X�^���p�[�c�Q")]
    [Header("�J�X�^���J���[�Q�F�F��")]
    const float hue_2 = 0f;

    [Header("�J�X�^���J���[�Q�F�ʓx")]
    const float sat_2 = 1f;

    [Header("�J�X�^���J���[�Q�F���x")]
    const float val_2 = 1f;


    // Use this for initialization
    void Start()
    {
        //// �}�l�[�W���[����\���p�[�c���擾
        //GameObject[] tmp_obj = GetComponent<M_PlayerManager>().GetParts();
        //int count = tmp_obj.Length;

        // �\���p�[�c�̃Q�[���I�u�W�F�N�g�̎擾
        // ExTag �̃R���|�[�l���g�����q�I�u�W�F�N�g����������
        ExTag_PlayerParts[] tmp = GetComponentsInChildren<ExTag_PlayerParts>();
        int count = tmp.Length;

        custom1_count = 0;
        custom2_count = 0;

        // �擾�����p�[�c���J�X�^���\�����f����
        for (int i = 0; i < count; i++)
        {
            // �J�X�^���p�[�c�ȊO�͔r��
            if (tmp[i].tag != ExTag_PlayerParts.eTag.CustomParts) continue;

            // �J�X�^���o����ꍇ
            if (tmp[i].customColor == true)
            {
                custom1_count++;
            }
            else
            {
                custom2_count++;
            }
        }

        // �p�[�c���Ƃ̃}�e���A�����i�[����z����쐬
        // �ύX�\�J���[
        parts_custom1 = new GameObject[custom1_count];
        // �Œ�J���[
        parts_custom2 = new GameObject[custom2_count];

        // �}���p�ϐ�
        int insert1 = 0;
        int insert2 = 0;

        // �J�X�^���p�[�c���ɕϐ��Ɋi�[����
        for (int i = 0; i < (custom1_count + custom2_count); i++)
        {
            if (tmp[i].GetComponent<ExTag_PlayerParts>().customColor == true)
            {
                parts_custom1[insert1] = tmp[i].gameObject;
                insert1++;
            }
            else
            {
                parts_custom2[insert2] = tmp[i].gameObject;
                insert2++;
            }
        }

        // �p�[�c���Ƃ̃}�e���A�����i�[����z����쐬
        // �ύX�\�J���[
        parts_mat_custom1 = new Material[custom1_count];
        // �Œ�J���[
        parts_mat_custom2 = new Material[custom2_count];


        // �}�e���A����z��Ɋi�[
        for (int i = 0; i < custom1_count; i++)
        {
            parts_mat_custom1[i] = parts_custom1[i].GetComponent<Renderer>().material;
        }


        for (int i = 0; i < custom2_count; i++)
        {
            parts_mat_custom2[i] = parts_custom2[i].GetComponent<Renderer>().material;
            parts_mat_custom2[i].SetFloat("_Hue", hue_2);
            parts_mat_custom2[i].SetFloat("_Sat", sat_2);
            parts_mat_custom2[i].SetFloat("_Val", val_2);
        }



    }

    // Update is called once per frame
    void Update()
    {
        // ��C�Ƀ}�e���A���̐F��ύX
        for (int i = 0; i < custom1_count; i++)
        {
            parts_mat_custom1[i].SetFloat("_Hue", hue_1);
            parts_mat_custom1[i].SetFloat("_Sat", sat_1);
            parts_mat_custom1[i].SetFloat("_Val", val_1);
        }
    }

    public void InitParts_HSV()
    {
        // �}�l�[�W���[����\���p�[�c���擾
        GameObject[] tmp_obj = GetComponent<M_PlayerManager>().GetParts();
        int count = tmp_obj.Length;

        // �擾�����p�[�c���J�X�^���\�����f����
        for (int i = 0; i < count; i++)
        {
            // �J�X�^���o����ꍇ
            if (tmp_obj[i].GetComponent<ExTag_PlayerParts>().customColor == true)
            {
                custom1_count++;
            }
            else
            {
                custom2_count++;
            }
        }

        // �p�[�c���Ƃ̃}�e���A�����i�[����z����쐬
        // �ύX�\�J���[
        parts_custom1 = new GameObject[custom1_count];
        // �Œ�J���[
        parts_custom2 = new GameObject[custom2_count];

        // �}���p�ϐ�
        int insert1 = 0;
        int insert2 = 0;

        // �J�X�^���p�[�c���ɕϐ��Ɋi�[����
        for (int i = 0; i < count; i++)
        {
            if (tmp_obj[i].GetComponent<ExTag_PlayerParts>().customColor == true)
            {
                parts_custom1[insert1] = tmp_obj[i];
                insert1++;
            }
            else
            {
                parts_custom2[insert2] = tmp_obj[i];
                insert2++;
            }
        }

        // �p�[�c���Ƃ̃}�e���A�����i�[����z����쐬
        // �ύX�\�J���[
        parts_mat_custom1 = new Material[custom1_count];
        // �Œ�J���[
        parts_mat_custom2 = new Material[custom2_count];


        // �}�e���A����z��Ɋi�[
        for (int i = 0; i < custom1_count; i++)
        {
            parts_mat_custom1[i] = parts_custom1[i].GetComponent<Renderer>().material;
        }


        for (int i = 0; i < custom2_count; i++)
        {
            parts_mat_custom2[i] = parts_custom2[i].GetComponent<Renderer>().material;
            parts_mat_custom2[i].SetFloat("_Hue", hue_2);
            parts_mat_custom2[i].SetFloat("_Sat", sat_2);
            parts_mat_custom2[i].SetFloat("_Val", val_2);
        }
    }
}