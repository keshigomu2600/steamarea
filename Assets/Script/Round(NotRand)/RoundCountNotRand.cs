using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundCountNotRand : MonoBehaviour
{
    //���E���h�؂�ւ����̉�ʕ\��
    public GameObject image;
    public GameObject failedImage;

    public GameObject Player;

    bool spawnFlag;

    public GameObject roundCondition;

    [Header("�J�E���g�C���[�W")]
    public GameObject[] countImage;

    public GameObject mainCamera;

    [Header("�����E���h�ڂ̃X�e�[�W�H")]
    public int roundCount = 5;
    public TimeCount timeCount;
    public Text round;
    //�����ɕ\��������e�L�X�g
    public Text roundLeft;
    //���b�\�������邩
    public const float app = 1.0f;
    ////�N�[���^�C���͉��b��
    //public const float coolTime = 6.0f;

    //����ăX�e�[�W�ŃK�C�h�̃J�����ړ��̂��߂Ɏg��
    [Header("���ۂɃv���C���[���\������Ă���J�E���g�_�E�����鎞��")]
    public float countDownTime = 3f;
    [Header("�\������J�E���g�_�E���̎���")]
    public float printCountDownTime = 3f;

    public Image imageRound = null;

    float appCount = app;
    float coolCount;
    //MaxCount�̏����l�i�[
    float max = 0.0f;

    //�X�`�[�����ǂꂭ�炢���܂��Ă��邩�\��������
    public GameObject steamMeter;

    //ROUNDx�\��
    [HideInInspector]
    public int count = 0;
    //�t�H���g�o���t���O
    bool textFlag = true;
    //�N�[���^�C���t���O
    bool coolFlag = false;
    //���E���h�摜�͊��ɕ\�����ꂽ��
    bool imageRoundFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        roundCondition.SetActive(false);
        coolCount = countDownTime + 3f;
        //round.text = "ROUND" + roundCount;
        round.text = "";
        roundLeft.text = " ";
        count = roundCount;
        max = timeCount.MaxCount;
        //���E���h�摜��\������
        if (imageRound != null && !imageRoundFlag)
        {
            imageRound.enabled = true;
            imageRoundFlag = true;
        }

        for (int i = 0; i < countImage.Length; i++)
        {
            countImage[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timeCount.MaxCount == 0.0f && !textFlag && !coolFlag)
        {
            count++;
            //�\��
            //round.text = "ROUND" + roundCount;
            round.text = "";
            roundLeft.text = " ";
            ChangeFlag(ref textFlag);
            //�摜��\������
            image.SetActive(true);

            //�N���A������\�����Ȃ�
            roundCondition.SetActive(false);

            //�X�`�[�����ǂꂭ�炢���܂��Ă��邩�����Ȃ��悤�ɂ���
            steamMeter.SetActive(false);

            //�v���C���[�������Ȃ��悤�ɂ���
            Player.SendMessage("CanMove", false);

            spawnFlag = true;
        }

        if (timeCount.activeFlag)
        {
            //roundLeft.text = "ROUND" + roundCount;
        }

        if (textFlag)
        {
            appCount -= Time.deltaTime;
            //�\�����鎞�Ԃ��z������
            if (appCount <= 0.0f)
            {
                //���Ԃ�߂�(�Z�b�g)
                appCount = app;
                //�t���O�I�t
                ChangeFlag(ref textFlag);
                ChangeFlag(ref coolFlag);
                //Time.timeScale = 1f;
            }
        }

        {
            if (coolFlag)
            {
                coolCount -= Time.deltaTime;

                //���E���h�\��
                //round.text = "ROUND" + count;
            }

            //��{�I�ɂ̓J�E���g�_�E����3�b�ȉ��̎�
            if (coolCount < countDownTime)
            {
                if (spawnFlag)
                {
                    Player.SendMessage("Spawn");
                    spawnFlag = false;
                }
                //�摜��\�����Ȃ�
                image.SetActive(false);
                failedImage.SetActive(false);
                //���E���h�摜��\�����Ȃ�
                if (imageRound != null)
                {
                    imageRound.enabled = false;
                }

                if (coolCount < printCountDownTime)
                {
                    //�J�E���g�_�E���\��(�ォ�玝���Ă��܂���)
                    //round.text = "" + ((int)(coolCount % 10) + 1);
                    //�R���Q���P�ɂȂ�
                    switch ((int)(coolCount % 10) + 1)
                    {
                        case 3:
                            countImage[2].SetActive(true);
                            break;
                        case 2:
                            countImage[2].SetActive(false);
                            countImage[1].SetActive(true);
                            break;
                        case 1:
                            countImage[1].SetActive(false);
                            countImage[0].SetActive(true);
                            break;
                        default:
                            countImage[0].SetActive(false);
                            countImage[1].SetActive(false);
                            countImage[2].SetActive(false); break;
                    }
                }
            }
            else if (coolCount < countDownTime + 2f)
            {
                //round.text = "ROUND" + roundCount;
                //�N���A������\������
                //roundCondition.SetActive(true);
            }

            //�\�����鎞�Ԃ��z������
            if (coolCount <= 0.0f)
            {
                //��\��
                //round.text = " ";
                countImage[0].SetActive(false);
                countImage[1].SetActive(false);
                countImage[2].SetActive(false);
                //���Ԃ�߂�(�Z�b�g)
                coolCount = countDownTime + 3f;
                timeCount.MaxCount = max;

                //�t���O�I�t
                ChangeFlag(ref coolFlag);
                //Time�J�E���gON
                ChangeFlag(ref timeCount.activeFlag);

                //�X�`�[�����ǂꂭ�炢���܂��Ă��邩������悤�ɂ���
                steamMeter.SetActive(true);

                //�v���C���[��������悤�ɂ���
                Player.SendMessage("CanMove", true);

                mainCamera.SendMessage("CanMove", true);
            }
        }

        if (count == roundCount + 1)
        {
            //�I���̏���

            round.text = "";
        }
    }

    //�t���O��ς���
    void ChangeFlag(ref bool flag)
    {
        if (flag)
        {
            flag = false;
        }
        else
        {
            flag = true;
        }
    }
}
