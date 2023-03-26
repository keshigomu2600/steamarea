using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;

public class RoundCount : MonoBehaviour
{
    //���E���h�؂�ւ����̉�ʕ\��
    public GameObject image;

    public GameObject Player;

    bool spawnFlag;

    bool StageEnemyNumberFlag = false;

    public GameObject roundCondition;

    public AudioSource gamebgm;

    public int roundCount = 5;
    public TimeCount timeCount;
    public Text round;
    //�����ɕ\��������e�L�X�g
    public Text roundLeft;
    //���b�\�������邩
    public const float app = 1.0f;
    //�N�[���^�C���͉��b��
    public const float coolTime = 6.0f;
    float appCount = app;
    float coolCount = coolTime;
    //MaxCount�̏����l�i�[
    float max = 0.0f;

    //�X�`�[�����ǂꂭ�炢���܂��Ă��邩�\��������
    public GameObject steamMeter;

    //ROUNDx�\��
    [HideInInspector]
    public int count = 1;
    //�t�H���g�o���t���O
    bool textFlag = true;
    //�N�[���^�C���t���O
    bool coolFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        round.text = "ROUND" + count;
        roundLeft.text = " ";
        max = timeCount.MaxCount;

        gamebgm = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timeCount.MaxCount == 0.0f && !textFlag && !coolFlag)
        {
            //�\��
            count++;
            round.text = "ROUND" + count;
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
            roundLeft.text = "ROUND" + count;
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

            //�J�E���g�_�E����3�b�ȉ��̎�
            if (coolCount < 3f)
            {
                if (spawnFlag)
                {
                    Player.SendMessage("Spawn");
                    spawnFlag = false;
                }
                //�摜��\�����Ȃ�
                image.SetActive(false);
                //�J�E���g�_�E���\��(�ォ�玝���Ă��܂���)
                round.text = "" + ((int)(coolCount % 10) + 1);
            }
            else if (coolCount < 5f)
            {
                round.text = "ROUND" + count;
                //�N���A������\������
                roundCondition.SetActive(true);
            }

            //�\�����鎞�Ԃ��z������
            if (coolCount <= 0.0f)
            {
                //��\��
                round.text = " ";

                //�T�E���h�𗬂�
                gamebgm.Play();

                //���Ԃ�߂�(�Z�b�g)
                coolCount = coolTime;
                timeCount.MaxCount = max;

                //�t���O�I�t
                ChangeFlag(ref coolFlag);
                //Time�J�E���gON
                ChangeFlag(ref timeCount.activeFlag);

                //�X�`�[�����ǂꂭ�炢���܂��Ă��邩������悤�ɂ���
                steamMeter.SetActive(true);

                //�v���C���[��������悤�ɂ���
                Player.SendMessage("CanMove", true);
            }
        }

        if (count == roundCount + 1)
        {
            //�I���̏���
            Debug.Log("END");
            round.text = "";


        }


   

    }

    //�t���O��ς���
    void ChangeFlag(ref bool flag)
    {
        if (flag)
        {
            flag = false;

            gamebgm.Stop();
        }
        else
        {
            flag = true;
        }
    }

    
       

}
