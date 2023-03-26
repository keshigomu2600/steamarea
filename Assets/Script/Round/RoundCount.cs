using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;

public class RoundCount : MonoBehaviour
{
    //ラウンド切り替え時の画面表示
    public GameObject image;

    public GameObject Player;

    bool spawnFlag;

    bool StageEnemyNumberFlag = false;

    public GameObject roundCondition;

    public AudioSource gamebgm;

    public int roundCount = 5;
    public TimeCount timeCount;
    public Text round;
    //左側に表示させるテキスト
    public Text roundLeft;
    //何秒表示させるか
    public const float app = 1.0f;
    //クールタイムは何秒か
    public const float coolTime = 6.0f;
    float appCount = app;
    float coolCount = coolTime;
    //MaxCountの初期値格納
    float max = 0.0f;

    //スチームがどれくらいたまっているか表示するやつ
    public GameObject steamMeter;

    //ROUNDx表示
    [HideInInspector]
    public int count = 1;
    //フォント出すフラグ
    bool textFlag = true;
    //クールタイムフラグ
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
            //表示
            count++;
            round.text = "ROUND" + count;
            round.text = "";
            roundLeft.text = " ";
            ChangeFlag(ref textFlag);
            //画像を表示する
            image.SetActive(true);
            //クリア条件を表示しない
            roundCondition.SetActive(false);

            //スチームがどれくらいたまっているか見えないようにする
            steamMeter.SetActive(false);

            //プレイヤーが動けないようにする
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
            //表示する時間を越えたら
            if (appCount <= 0.0f)
            {
                //時間を戻す(セット)
                appCount = app;
                //フラグオフ
                ChangeFlag(ref textFlag);
                ChangeFlag(ref coolFlag);
                //Time.timeScale = 1f;
            }
        }

        {
            if (coolFlag)
            {
                coolCount -= Time.deltaTime;

                //ラウンド表示
                //round.text = "ROUND" + count;
            }

            //カウントダウンが3秒以下の時
            if (coolCount < 3f)
            {
                if (spawnFlag)
                {
                    Player.SendMessage("Spawn");
                    spawnFlag = false;
                }
                //画像を表示しない
                image.SetActive(false);
                //カウントダウン表示(上から持ってきました)
                round.text = "" + ((int)(coolCount % 10) + 1);
            }
            else if (coolCount < 5f)
            {
                round.text = "ROUND" + count;
                //クリア条件を表示する
                roundCondition.SetActive(true);
            }

            //表示する時間を越えたら
            if (coolCount <= 0.0f)
            {
                //非表示
                round.text = " ";

                //サウンドを流す
                gamebgm.Play();

                //時間を戻す(セット)
                coolCount = coolTime;
                timeCount.MaxCount = max;

                //フラグオフ
                ChangeFlag(ref coolFlag);
                //TimeカウントON
                ChangeFlag(ref timeCount.activeFlag);

                //スチームがどれくらいたまっているか見えるようにする
                steamMeter.SetActive(true);

                //プレイヤーが動けるようにする
                Player.SendMessage("CanMove", true);
            }
        }

        if (count == roundCount + 1)
        {
            //終わりの処理
            Debug.Log("END");
            round.text = "";


        }


   

    }

    //フラグを変える
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
