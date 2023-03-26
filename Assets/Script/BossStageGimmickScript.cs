using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossStageGimmickScript : MonoBehaviour
{
    //ギミック発同時に上に上げる壁
    public GameObject LowerWall;
    //ギミック発同時に下に下げる壁
    public GameObject UpperWall;
    //壁の初期位置を保存
    Vector3 LowerWallStartPos;
    Vector3 UpperWallStartPos;
    //壁が動く距離
    const float wallMoveDistance= 12f;

    //ギミック発動にひつような落とす敵の数
    public int gimmicksNeedEnemyCount;
    //落ちた敵の数を数える変数
    int enemyCount;
    //ギミック発動時にスポーンする敵の数
    public GameObject[] spawnEnemy;
    //ギミックが発動するかの判定
    bool startGimmick;

    //赤い点滅
    public Image redBlinkingLight;
    //サイレンサウンド
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
