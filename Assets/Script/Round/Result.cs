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
    [Header("下記の対応したスクリプトの降順にゲームオブジェクトを置いてください")]
    public GameObject[] enemyDrop;
    //条件スクリプト
    [Header("条件スクリプト（対応したスクリプト）")]
    public EnemyDropOutSide enemyDropOutSide;
    public allEnemyDrop allEnemyDrop;
    public EnemyDropInSide enemyDropInSide;
    //条件スクリプトの数
    [Header("条件スクリプトの数")]
    public int scriptCount;

    [Header("ステージによって出す条件を変える")]
    public StageSelect stageSelect;

    [Header("以下Roundや表記関係")]
    public RoundCount round;
    public TimeCount timeCount;
    public Text resultText;
    public Text resultTextCenter;

    //結果発表に使う
    [HideInInspector]
    public static ArrayList roundResult = new ArrayList();
    //条件スクリプトの配列（クリアしたのかどうかに使う）bool型にする
    ArrayList scriptList = new ArrayList();
    //要素
    int count = 0;
    //ランダムでどの条件にするか決める用
    int randScript = 0;
    //実行するフラグ
    bool ActiveFlag = true;
    //処理終了フラグ
    bool proccesFlag = true;

    //ラウンドをクリアしたかどうか表示するフラグ
    //bool resultPrintFlag = false;
    float resultPrintTime = 0f;
    float resultPrintMaxTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        resultTextCenter.text = "";
        resultText.text = "";
        //ラウンド数分、配列の要素数を格納
        for (int i = 0; i < round.roundCount; i++)
        {
            roundResult.Add(RESULT.None);
        }

        //scriptの配列格納
        for(int i = 0;i< scriptCount;i++)
        {
            scriptList.Add(false);
        }

        // 条件を決める(それ以外のオブジェクトを非アクティブにする)
        RandSelect();
    }

    // Update is called once per frame
    void Update()
    {
        //ラウンド内のみ処理
        if(proccesFlag)
        {
            //時間切れの処理
            if (timeCount.MaxCount == 0.0f && ActiveFlag)
            {
                //スクリプトの条件結果を格納
                scriptList[0] = enemyDropOutSide.clearFlag;
                scriptList[1] = allEnemyDrop.clearFlag;
                scriptList[2] = enemyDropInSide.clearFlag;

                for (int i = 0; i < scriptCount; i++) 
                {
                    //一つでもクリアしていたら〇表記させて抜け出す
                    if((bool)scriptList[i])
                    {
                        roundResult[count] = RESULT.Success;
                        FlagInitialize();

                        break;
                    }
                    roundResult[count] = RESULT.False;
                }

                TextContents();

                //ランダムで条件を決める
                RandSelect();

                ActiveFlag = false;
            }

            if(timeCount.MaxCount == 0.0f)
            {
                //↓●×から文字に変えました
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
    void RandSelect()
    {

        stageSelect.stage[stageSelect.valueOld].SetActive(false);
        stageSelect.RandStage();
        randScript = Random.Range(0, scriptCount);

        //ドーナツステージで内側に敵がスポーンするようにしたので条件変えました。
        //逆にjapanの時に外側に落とすなを出さないようにしました。
        if (randScript == 2 && stageSelect.stage[1].activeSelf)
        {
            //ドーナツステージの確立が下がるので変更しました。
            randScript = Random.Range(0, 1);
        }

        for (int i = 0; i < enemyDrop.Length; i++)
        {
            //ランダムで出た番号と同じ要素数のオブジェクトをアクティブにする
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

    //フラグリセット
    void FlagInitialize()
    {
        enemyDropOutSide.clearFlag = false;
        allEnemyDrop.clearFlag = false;
        enemyDropInSide.clearFlag = false;
    }
}