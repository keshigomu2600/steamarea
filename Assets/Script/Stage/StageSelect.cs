using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelect : MonoBehaviour
{
    //�ق�Result�ˑ��ł�

   

    [Header("�X�e�[�W(������Ă������ł�)")]
    public GameObject[] stage;
    public TimeCount timeCount;

    //�X�e�[�W�I��p�̗v�f��
    int value = 0;
    [HideInInspector]
    public int valueOld = 0;

    // Update is called once per frame
    void Update()
    {

    }

    public void RandStage()
    {
        //�����_���ŃX�e�[�W�I��
        value = Random.Range(0, stage.Length);

        stage[value].SetActive(true);
        valueOld = value;

    }
}
