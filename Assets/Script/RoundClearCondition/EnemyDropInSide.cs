using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropInSide : MonoBehaviour
{
    //���Ԃ��������f���邽�߂ɒǉ��œ���܂���
    public TimeCount timeCount;
    //�X�e�[�W�ɂ���ēG�̐����ς��̂ŊY������X�e�[�W�I�u�W�F�N�g��ǉ����܂����B
    public GameObject stageDonuts;

    public PlayerOut playerOut;

    public int allEnemyCount = 20;
    //�ʃX�e�[�W�̓G�̐��ł�
    public int allEnemyOutside = 10;
    int enemyCount = 0;
    //�����̓G�̐��ł�
    int initializeEnemy = 0;
    [HideInInspector]
    public bool clearFlag = false;

    //�ʃX�e�[�W�̓G�̐������킹��̂Ɏg���܂��t���O
    bool StageEnemyNumberFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        initializeEnemy = allEnemyCount;
        Initialize();
        StageEnemyNumber();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCount >= allEnemyCount)
        {
            timeCount.MaxCount = 0.0f;
            clearFlag = true;
        }
        else
        {
            clearFlag = false;
        }

        //���Ԑ؂�
        if (enemyCount < allEnemyCount && timeCount.MaxCount == 0.0f)
        {
            clearFlag = false;
            enemyCount = 0;
        }

        if (clearFlag)
        {
            print("clear");
            enemyCount = 0;
        }

        if(playerOut.outFlag)
        {
            RoundFailed();
        }

        //�������I������玟�̃X�e�[�W�̓G�̐����č\�z����
        if (clearFlag || timeCount.MaxCount == 0.0f)
        {
            if (!StageEnemyNumberFlag)
            {
                StageEnemyNumber();
                StageEnemyNumberFlag = true;
            }
        }
        else
        {
            StageEnemyNumberFlag = false;
        }
    }

    void DropEnemyCount()
    {
        enemyCount++;
    }

    void RoundFailed()
    {
        print("Failed");
        clearFlag = false;
        timeCount.MaxCount = 0.0f;
        enemyCount = 0;
    }

    void Initialize()
    {
        clearFlag = false;
    }

    void StageEnemyNumber()
    {
        //�h�[�i�c�X�e�[�W���A�N�e�B�u�Ȃ�G�̐����Z+����
        if (stageDonuts.activeSelf)
        {
            allEnemyCount = initializeEnemy + allEnemyOutside;
        }
        else
        {
            allEnemyCount = initializeEnemy;
        }
    }
}
