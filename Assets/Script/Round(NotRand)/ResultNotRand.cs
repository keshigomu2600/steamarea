using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultNotRand : MonoBehaviour
{
    [Header("下記の対応したスクリプトの降順にゲームオブジェクトを置いてください")]
    public GameObject[] enemyDrop;
    //条件スクリプト
    [Header("条件スクリプト（対応したスクリプト）")]
    public EnemyDropOutSide enemyDropOutSide;
    public allEnemyDrop allEnemyDrop;
    public EnemyDropInSide enemyDropInSide;
    public EnemyBoxIn enemyBoxIn;
    [Header("条件スクリプトの数(数を同数にしないとエラーが起こります)")]
    public int scriptCount;
    [Header("該当スクリプトの番号(0 ~ n)")]
    public int scriptNumber;
    [Header("ラウンド終了処理に使う")]
    public BrackOutGameOverPlus brackOutGameOver;

    [Header("以下Roundや表記関係")]
    public RoundCount round;
    public TimeCount timeCount;
    public Text resultText;
    public Text resultTextCenter;

    AudioSource clearsound;
    AudioSource roundmiss;
    public GameObject roundmissobject;
    public GameObject clearsoundobject;

    //クリア時の音楽をいれます。
    

    //結果発表に使う
    [HideInInspector]
    public ArrayList roundResult = new ArrayList();
    //すぐゲームオーバーさせるかどうかに使う
    [HideInInspector]
    public bool gameoverFlag = false;
    //条件スクリプトの配列（クリアしたのかどうかに使う）bool型にする
    ArrayList scriptList = new ArrayList();
    //要素
    int count = 0;
    //実行するフラグ
    bool ActiveFlag = true;
    //処理終了フラグ
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
        //ラウンド数分、配列の要素数を格納
        for (int i = 0; i < round.roundCount; i++)
        {
            roundResult.Add(RESULT.None);
        }

        //scriptの配列格納
        for (int i = 0; i < scriptCount; i++)
        {
            scriptList.Add(false);
        }

        // 条件を決める(それ以外のオブジェクトを非アクティブにする)
        Select();
    }

    // Update is called once per frame
    void Update()
    {
        //ラウンド内のみ処理
        if (proccesFlag)
        {
            //時間切れの処理
            if (timeCount.MaxCount == 0.0f && ActiveFlag)
            {
                //スクリプトの条件結果を格納
                scriptList[0] = enemyDropOutSide.clearFlag;
                scriptList[1] = allEnemyDrop.clearFlag;
                scriptList[2] = enemyDropInSide.clearFlag;
                scriptList[3] = enemyBoxIn.clearFlag;

                for (int i = 0; i < scriptCount; i++)
                {
                    gameoverFlag = true;
                    //一つでもクリアしていたら〇表記させて抜け出す
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

                //ランダムで条件を決める
                Select();

                ActiveFlag = false;
            }

            if (timeCount.MaxCount == 0.0f)
            {
                //↓●×から文字に変えました
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
                //表示時間を保存
                resultPrintTime += Time.deltaTime;
            }
            else
            {
                resultPrintTime = 0f;
            }

            //一定時間ラウンド結果を表示したら何も表示しない
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

            //countがroundCountに達したら処理終了
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

    // 条件を決める(それ以外のオブジェクトを非アクティブにする)
    void Select()
    {

        for (int i = 0; i < enemyDrop.Length; i++)
        {
            //該当する番号と同じ要素数のオブジェクトをアクティブにする
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

    //フラグリセット
    void FlagInitialize()
    {
        enemyDropOutSide.clearFlag = false;
        allEnemyDrop.clearFlag = false;
        enemyDropInSide.clearFlag = false;
        enemyBoxIn.clearFlag = false;
    }
}