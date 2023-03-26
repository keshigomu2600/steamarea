using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ق�ollenemy�ƈꏏ�ł�
public class EnemyBoxIn : MonoBehaviour
{
    //���Ԃ��������f���邽�߂ɒǉ��œ���܂���
    public TimeCount timeCount;

    public PlayerOut playerOut;

    public AudioSource boxInSound;

    public int allEnemyCount = 20;
    //�ʃX�e�[�W�̓G�̐��ł�
    public int allEnemyOutside = 10;

    [HideInInspector]
    public bool clearFlag = false;

    int enemyCount = 0;
    int initializeEnemy = 0;
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
        Debug.Log(allEnemyCount);
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

        if (playerOut.outFlag)
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

    private void OnTriggerEnter(Collider other)
    {

    }

    void DropEnemyCount()
    {
        enemyCount++;
        boxInSound.Play();
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
        allEnemyCount = initializeEnemy;
    }
}
