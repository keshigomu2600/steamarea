using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossStageGimmickScript : MonoBehaviour
{
    //�M�~�b�N�������ɏ�ɏグ���
    public GameObject LowerWall;
    //�M�~�b�N�������ɉ��ɉ������
    public GameObject UpperWall;
    //�ǂ̏����ʒu��ۑ�
    Vector3 LowerWallStartPos;
    Vector3 UpperWallStartPos;
    //�ǂ���������
    const float wallMoveDistance= 12f;

    //�M�~�b�N�����ɂЂ悤�ȗ��Ƃ��G�̐�
    public int gimmicksNeedEnemyCount;
    //�������G�̐��𐔂���ϐ�
    int enemyCount;
    //�M�~�b�N�������ɃX�|�[������G�̐�
    public GameObject[] spawnEnemy;
    //�M�~�b�N���������邩�̔���
    bool startGimmick;

    //�Ԃ��_��
    public Image redBlinkingLight;
    //�T�C�����T�E���h
    AudioSource sailen;

    public GameObject Canvas;
    public GameObject finalsound;

    float time;
    bool increaseTransparency;
    int blinkingNumberOfTime;
    float count = 0;
    // Start is called before the first frame update
    void Start()
    {
        startGimmick = false;
        LowerWallStartPos = LowerWall.transform.position;
        UpperWallStartPos = UpperWall.transform.position;
        enemyCount = 0;
        time = 0f;
        increaseTransparency = true;
        blinkingNumberOfTime = 0;
        sailen = GetComponent<AudioSource>();

    }
    // Update is called once per frame
    void Update()
    {
        if (startGimmick)
        {
            if (LowerWall.transform.position.y < LowerWallStartPos.y + 0.6 * wallMoveDistance)
            {
                LowerWall.transform.Translate(Vector3.up * 1.05f * Time.deltaTime);
            }

            if (UpperWall.transform.position.y > UpperWallStartPos.y - 0.6 * (wallMoveDistance + 4))
            {
                UpperWall.transform.Translate(Vector3.down * 1.5f * Time.deltaTime);
            }

            if (increaseTransparency)
            {
                time += Time.deltaTime;
            }
            else
            {
                time -= Time.deltaTime;
            }

            if (time > 1)
            {
                time = 1;
                increaseTransparency = false;
            }
            else if (time < 0)
            {
                time = 0;
                increaseTransparency = true;
                blinkingNumberOfTime++;
            }

            if (blinkingNumberOfTime == 3)
            {
                for (int i = 0; i < spawnEnemy.Length; i++)
                {
                    spawnEnemy[i].SetActive(true);
                }
                gameObject.SetActive(false);
            }

            redBlinkingLight.color = new Color(255, 0, 0, time / 5);

            if (!sailen.isPlaying)
            {
                Canvas.GetComponent<AudioSource>().Stop();

                sailen.Play();

                if (!sailen.isPlaying)
                {
                    finalsound.GetComponent<AudioSource>().Play();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.gameObject.SetActive(false);
            enemyCount++;
            if(enemyCount == gimmicksNeedEnemyCount)
            {
                startGimmick = true;
            }
        }
    }
}
