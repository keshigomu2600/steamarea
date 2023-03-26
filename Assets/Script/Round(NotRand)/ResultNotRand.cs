using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultNotRand : MonoBehaviour
{
    [Header("���L�̑Ή������X�N���v�g�̍~���ɃQ�[���I�u�W�F�N�g��u���Ă�������")]
    public GameObject[] enemyDrop;
    //�����X�N���v�g
    [Header("�����X�N���v�g�i�Ή������X�N���v�g�j")]
    public EnemyDropOutSide enemyDropOutSide;
    public allEnemyDrop allEnemyDrop;
    public EnemyDropInSide enemyDropInSide;
    public EnemyBoxIn enemyBoxIn;
    [Header("�����X�N���v�g�̐�(���𓯐��ɂ��Ȃ��ƃG���[���N����܂�)")]
    public int scriptCount;
    [Header("�Y���X�N���v�g�̔ԍ�(0 ~ n)")]
    public int scriptNumber;
    [Header("���E���h�I�������Ɏg��")]
    public BrackOutGameOverPlus brackOutGameOver;

    [Header("�ȉ�Round��\�L�֌W")]
    public RoundCount round;
    public TimeCount timeCount;
    public Text resultText;
    public Text resultTextCenter;

    AudioSource clearsound;
    AudioSource roundmiss;
    public GameObject roundmissobject;
    public GameObject clearsoundobject;

    //�N���A���̉��y������܂��B
    

    //���ʔ��\�Ɏg��
    [HideInInspector]
    public ArrayList roundResult = new ArrayList();
    //�����Q�[���I�[�o�[�����邩�ǂ����Ɏg��
    [HideInInspector]
    public bool gameoverFlag = false;
    //�����X�N���v�g�̔z��i�N���A�����̂��ǂ����Ɏg���jbool�^�ɂ���
    ArrayList scriptList = new ArrayList();
    //�v�f
    int count = 0;
    //���s����t���O
    bool ActiveFlag = true;
    //�����I���t���O
    [HideInInspector]
    public bool proccesFlag = true;

    float resultPrintTime = 0f;
    float resultPrintMaxTime = 2f;

    public GameObject failedImage;

    // Start is called before the first frame update
    void Start()
    {
        
        clearsound = clearsoundobject.GetComponent<AudioSource>();
        roundmiss = roundmissobject.GetComponent<AudioSource>();
        resultTextCenter.text = "";
        resultText.text = "";
        //���E���h�����A�z��̗v�f�����i�[
        for (int i = 0; i < round.roundCount; i++)
        {
            roundResult.Add(RESULT.None);
        }

        //script�̔z��i�[
        for (int i = 0; i < scriptCount; i++)
        {
            scriptList.Add(false);
        }

        // ���������߂�(����ȊO�̃I�u�W�F�N�g���A�N�e�B�u�ɂ���)
        Select();
    }

    // Update is called once per frame
    void Update()
    {
        //���E���h���̂ݏ���
        if (proccesFlag)
        {
            //���Ԑ؂�̏���
            if (timeCount.MaxCount == 0.0f && ActiveFlag)
            {
                //�X�N���v�g�̏������ʂ��i�[
                scriptList[0] = enemyDropOutSide.clearFlag;
                scriptList[1] = allEnemyDrop.clearFlag;
                scriptList[2] = enemyDropInSide.clearFlag;
                scriptList[3] = enemyBoxIn.clearFlag;

                for (int i = 0; i < scriptCount; i++)
                {
                    gameoverFlag = true;
                    //��ł��N���A���Ă�����Z�\�L�����Ĕ����o��
                    if ((bool)scriptList[i])
                    {
                        roundResult[count] = RESULT.Success;
                        FlagInitialize();
                        gameoverFlag = false;
                        break;
                    }
                    roundResult[count] = RESULT.False;
                }

                TextContents();

                //�����_���ŏ��������߂�
                Select();

                ActiveFlag = false;
            }

            if (timeCount.MaxCount == 0.0f)
            {
                //�����~���當���ɕς��܂���
                if ((RESULT)roundResult[count] == RESULT.Success)
                {
                   

                        resultTextCenter.text = " ROUND CLEAR! ";

                    if (!clearsound.isPlaying)
                    {
                        clearsound.Play();
                    }
                }
                else if ((RESULT)roundResult[count] == RESULT.False)
                {
                    failedImage.SetActive(true);
                    resultTextCenter.text = " ROUND FAILED ";

                    if (!roundmiss.isPlaying)
                    {
                        roundmiss.Play();
                    }
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
                brackOutGameOver.SceneChange();
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
    void Select()
    {

        for (int i = 0; i < enemyDrop.Length; i++)
        {
            //�Y������ԍ��Ɠ����v�f���̃I�u�W�F�N�g���A�N�e�B�u�ɂ���
            if (i == scriptNumber)
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
        enemyBoxIn.clearFlag = false;
    }
}