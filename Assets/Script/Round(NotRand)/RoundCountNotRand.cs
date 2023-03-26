using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundCountNotRand : MonoBehaviour
{
    //ラウンド切り替え時の画面表示
    public GameObject image;
    public GameObject failedImage;

    public GameObject Player;

    bool spawnFlag;

    public GameObject roundCondition;

    [Header("カウントイメージ")]
    public GameObject[] countImage;

    public GameObject mainCamera;

    [Header("何ラウンド目のステージ？")]
    public int roundCount = 5;
    public TimeCount timeCount;
    public Text round;
    //左側に表示させるテキスト
    public Text roundLeft;
    //何秒表示させるか
    public const float app = 1.0f;
    ////クールタイムは何秒か
    //public const float coolTime = 6.0f;

    //主に籠ステージでガイドのカメラ移動のために使う
    [Header("実際にプレイヤーが表示されてからカウントダウンする時間")]
    public float countDownTime = 3f;
    [Header("表示するカウントダウンの時間")]
    public float printCountDownTime = 3f;

    public Image imageRound = null;

    float appCount = app;
    float coolCount;
    //MaxCountの初期値格納
    float max = 0.0f;

    //スチームがどれくらいたまっているか表示するやつ
    public GameObject steamMeter;

    //ROUNDx表示
    [HideInInspector]
    public int count = 0;
    //フォント出すフラグ
    bool textFlag = true;
    //クールタイムフラグ
    bool coolFlag = false;
    //ラウンド画像は既に表示されたか
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
        //ラウンド画像を表示する
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
            //表示
            //round.text = "ROUND" + roundCount;
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
            //roundLeft.text = "ROUND" + roundCount;
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

            //基本的にはカウントダウンが3秒以下の時
            if (coolCount < countDownTime)
            {
                if (spawnFlag)
                {
                    Player.SendMessage("Spawn");
                    spawnFlag = false;
                }
                //画像を表示しない
                image.SetActive(false);
                failedImage.SetActive(false);
                //ラウンド画像を表示しない
                if (imageRound != null)
                {
                    imageRound.enabled = false;
                }

                if (coolCount < printCountDownTime)
                {
                    //カウントダウン表示(上から持ってきました)
                    //round.text = "" + ((int)(coolCount % 10) + 1);
                    //３→２→１になる
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
                //クリア条件を表示する
                //roundCondition.SetActive(true);
            }

            //表示する時間を越えたら
            if (coolCount <= 0.0f)
            {
                //非表示
                //round.text = " ";
                countImage[0].SetActive(false);
                countImage[1].SetActive(false);
                countImage[2].SetActive(false);
                //時間を戻す(セット)
                coolCount = countDownTime + 3f;
                timeCount.MaxCount = max;

                //フラグオフ
                ChangeFlag(ref coolFlag);
                //TimeカウントON
                ChangeFlag(ref timeCount.activeFlag);

                //スチームがどれくらいたまっているか見えるようにする
                steamMeter.SetActive(true);

                //プレイヤーが動けるようにする
                Player.SendMessage("CanMove", true);

                mainCamera.SendMessage("CanMove", true);
            }
        }

        if (count == roundCount + 1)
        {
            //終わりの処理

            round.text = "";
        }
    }

    //フラグを変える
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
