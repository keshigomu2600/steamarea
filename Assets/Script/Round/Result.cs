using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum RESULT
{
    Success,
    False,
    None
}

public class Result : MonoBehaviour
{
    [Header("���L�̑Ή������X�N���v�g�̍~���ɃQ�[���I�u�W�F�N�g��u���Ă�������")]
    public GameObject[] enemyDrop;
    //�����X�N���v�g
    [Header("�����X�N���v�g�i�Ή������X�N���v�g�j")]
    public EnemyDropOutSide enemyDropOutSide;
    public allEnemyDrop allEnemyDrop;
    public EnemyDropInSide enemyDropInSide;
    //�����X�N���v�g�̐�
    [Header("�����X�N���v�g�̐�")]
    public int scriptCount;

    [Header("�X�e�[�W�ɂ���ďo��������ς���")]
    public StageSelect stageSelect;

    [Header("�ȉ�Round��\�L�֌W")]
    public RoundCount round;
    public TimeCount timeCount;
    public Text resultText;
    public Text resultTextCenter;

    //���ʔ��\�Ɏg��
    [HideInInspector]
    public static ArrayList roundResult = new ArrayList();
    //�����X�N���v�g�̔z��i�N���A�����̂��ǂ����Ɏg���jbool�^�ɂ���
    ArrayList scriptList = new ArrayList();
    //�v�f
    int count = 0;
    //�����_���łǂ̏����ɂ��邩���߂�p
    int randScript = 0;
    //���s����t���O
    bool ActiveFlag = true;
    //�����I���t���O
    bool proccesFlag = true;

    //���E���h���N���A�������ǂ����\������t���O
    //bool resultPrintFlag = false;
    float resultPrintTime = 0f;
    float resultPrintMaxTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        resultTextCenter.text = "";
        resultText.text = "";
        //���E���h�����A�z��̗v�f�����i�[
        for (int i = 0; i < round.roundCount; i++)
        {
            roundResult.Add(RESULT.None);
        }

        //script�̔z��i�[
        for(int i = 0;i< scriptCount;i++)
        {
            scriptList.Add(false);
        }

        // ���������߂�(����ȊO�̃I�u�W�F�N�g���A�N�e�B�u�ɂ���)
        RandSelect();
    }

    // Update is called once per frame
    void Update()
    {
        //���E���h���̂ݏ���
        if(proccesFlag)
        {
            //���Ԑ؂�̏���
            if (timeCount.MaxCount == 0.0f && ActiveFlag)
            {
                //�X�N���v�g�̏������ʂ��i�[
                scriptList[0] = enemyDropOutSide.clearFlag;
                scriptList[1] = allEnemyDrop.clearFlag;
                scriptList[2] = enemyDropInSide.clearFlag;

                for (int i = 0; i < scriptCount; i++) 
                {
                    //��ł��N���A���Ă�����Z�\�L�����Ĕ����o��
                    if((bool)scriptList[i])
                    {
                        roundResult[count] = RESULT.Success;
                        FlagInitialize();

                        break;
                    }
                    roundResult[count] = RESULT.False;
                }

                TextContents();

                //�����_���ŏ��������߂�
                RandSelect();

                ActiveFlag = false;
            }

            if(timeCount.MaxCount == 0.0f)
            {
                //�����~���當���ɕς��܂���
                if ((RESULT)roundResult[count] == RESULT.Success)
                {
                    resultTextCenter.text = " ROUND CLEAR! ";
                }
                else if ((RESULT)roundResult[count] == RESULT.False)
                {
                    resultTextCenter.text = " ROUND FAILED ";
                }
                else
                {
                    resultTextCenter.text = "  ";
                }
                //�\�����Ԃ�ۑ�
                resultPrintTime += Time.deltaTime;
            }
            else
            {
                resultPrintTime = 0f;
            }

            //��莞�ԃ��E���h���ʂ�\�������牽���\�����Ȃ�
            if (resultPrintTime >= resultPrintMaxTime)
            {
                resultTextCenter.text = "  ";
            }

            if (timeCount.MaxCount > 0.0f && !ActiveFlag)
            {
                count++;
                resultTextCenter.text = "  ";

                ActiveFlag = true;
            }

            //count��roundCount�ɒB�����珈���I��
            if (count == round.roundCount)
            {
                proccesFlag = false;
            }
        }
    }

    void TextContents()
    {

        if ((RESULT)roundResult[count] == RESULT.Success)
        {
            resultText.text += " O ";
        }
        else if ((RESULT)roundResult[count] == RESULT.False)
        {
            resultText.text += " X ";
        }
        else
        {
            resultText.text += "  ";
        }
    }

    // ���������߂�(����ȊO�̃I�u�W�F�N�g���A�N�e�B�u�ɂ���)
    void RandSelect()
    {

        stageSelect.stage[stageSelect.valueOld].SetActive(false);
        stageSelect.RandStage();
        randScript = Random.Range(0, scriptCount);

        //�h�[�i�c�X�e�[�W�œ����ɓG���X�|�[������悤�ɂ����̂ŏ����ς��܂����B
        //�t��japan�̎��ɊO���ɗ��Ƃ��Ȃ��o���Ȃ��悤�ɂ��܂����B
        if (randScript == 2 && stageSelect.stage[1].activeSelf)
        {
            //�h�[�i�c�X�e�[�W�̊m����������̂ŕύX���܂����B
            randScript = Random.Range(0, 1);
        }

        for (int i = 0; i < enemyDrop.Length; i++)
        {
            //�����_���ŏo���ԍ��Ɠ����v�f���̃I�u�W�F�N�g���A�N�e�B�u�ɂ���
            if (i == randScript)
            {
                enemyDrop[i].SetActive(true);
            }
            else
            {
                enemyDrop[i].SetActive(false);
            }
        }
    }

    //�t���O���Z�b�g
    void FlagInitialize()
    {
        enemyDropOutSide.clearFlag = false;
        allEnemyDrop.clearFlag = false;
        enemyDropInSide.clearFlag = false;
    }
}