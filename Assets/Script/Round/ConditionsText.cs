using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConditionsText : MonoBehaviour
{
    public TimeCount timeCount;
    [Header("����")]
    public GameObject enemyDropOutSide;
    public GameObject AllEnemyDrop;
    public GameObject enemyDropInSide;
    public GameObject enemyBoxIn;

    //�\�L�p
    EnemyDropOutSide enemyOutSide;
    EnemyDropInSide enemyInSide;
    EnemyBoxIn enemyBox;

    [Header("�����e�L�X�g")]
    public Text condition;
    public Text roundCondition;

    bool conditionFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        //�Q�[���I�u�W�F�N�g���̃X�N���v�g���i�[
        enemyOutSide = enemyDropOutSide.GetComponent<EnemyDropOutSide>();
        enemyInSide = enemyDropInSide.GetComponent<EnemyDropInSide>();
        enemyBox = enemyBoxIn.GetComponent<EnemyBoxIn>();

        TextCondition(roundCondition);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(timeCount.MaxCount > 0.0f && timeCount.activeFlag)
        {
            conditionFlag = true;
        }
        else
        {
            TextCondition(roundCondition);
            NotText(condition);
        }

        if(conditionFlag)
        {
            TextCondition(condition);
            NotText(roundCondition);

            conditionFlag = false;
        }
    }

    void TextCondition(Text textCondition)
    {
        if (enemyDropOutSide.activeSelf)
        {
            textCondition.text = enemyOutSide.allEnemyCount + "�̂̓G���O���̌��ɗ��Ƃ�!";
            //textCondition.text = "�����ɂ͓G�𗎂Ƃ���!";
            // textCondition.text = "�O���ɂ���" + enemyOutSide.allEnemyCount + "�C�̓G���O���ɗ��Ƃ��I";
        }

        if (AllEnemyDrop.activeSelf)
        {
            textCondition.text = "�Ƃɂ����G�𗎂Ƃ��I";
            //textCondition.text = enemyDrop.allEnemyCount + "�C�̓G�𗎂Ƃ��I";
        }

        if(enemyDropInSide.activeSelf)
        {
            textCondition.text = enemyInSide.allEnemyCount + "�̂̓G������̌��ɗ��Ƃ��I";
            //textCondition.text = "�O���ɂ͓G�𗎂Ƃ��ȁI";
            //textCondition.text = "�����̌��ɂ���" + enemyInSide.allEnemyCount + "�C�̓G�𗎂Ƃ��I";
        }

        if(enemyBoxIn.activeSelf)
        {
            textCondition.text = "�����ɓG��" + enemyBox.allEnemyCount + "�̓����I";
        }
    }

    //�e�L�X�g��\�������Ȃ��悤�ɂ���
    void NotText(Text textCondition)
    {
        textCondition.text = " ";
    }
}
