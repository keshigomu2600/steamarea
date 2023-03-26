using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyResporn : MonoBehaviour
{
    //�o��������G
    public GameObject enemy;
    //�J�E���g�i��莞�ԂɂȂ�����o��������j
    public TimeCount timeCount;
    //���̏o���������莞��
    public float count = 30.0f;

    //�i�[��
    GameObject copiedEnemy;
    //�G�͏o�����Ă�����
    bool enemyApp = true;
    //��A�N�e�B�u��������
    bool enemyNotActive = false;

    // Start is called before the first frame update
    void Start()
    {
        //�I�u�W�F�N�g�i�[
        copiedEnemy = enemy;
    }

    // Update is called once per frame
    void Update()
    {
        //�J�E���g���n�܂�����
        if(timeCount.MaxCount < count && timeCount.MaxCount > count - 1.0f && enemyApp)
        {
            //�G����A�N�e�B�u�������ꍇ�̎��ꎞ�I�ɃA�N�e�B�u�ɂ���
            if(!enemy.activeSelf)
            {
                enemy.gameObject.SetActive(true);
                enemyNotActive = true;
            }
            //�����쐬
            Instantiate(copiedEnemy, transform.position, Quaternion.identity);
            //���X��A�N�e�B�u�������ꍇ�͖߂�
            if(enemyNotActive)
            {
                enemy.gameObject.SetActive(false);
            }
            //�G���o�������邩�ǂ����̃t���O���ꎞ�I�ɃI�t
            enemyApp = false;
        }
        if(timeCount.MaxCount < count - 1.0f)
        {
            //1�b�߂�����t���O�I��
            enemyApp = true;
        }
    }
}
