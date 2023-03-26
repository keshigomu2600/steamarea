using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConditionsText : MonoBehaviour
{
    public TimeCount timeCount;
    [Header("条件")]
    public GameObject enemyDropOutSide;
    public GameObject AllEnemyDrop;
    public GameObject enemyDropInSide;
    public GameObject enemyBoxIn;

    //表記用
    EnemyDropOutSide enemyOutSide;
    EnemyDropInSide enemyInSide;
    EnemyBoxIn enemyBox;

    [Header("条件テキスト")]
    public Text condition;
    public Text roundCondition;

    bool conditionFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        //ゲームオブジェクト内のスクリプトを格納
        enemyOutSide = enemyDropOutSide.GetComponent<EnemyDropOutSide>();
        enemyInSide = enemyDropInSide.GetComponent<EnemyDropInSide>();
        enemyBox = enemyBoxIn.GetComponent<EnemyBoxIn>();

        TextCondition(roundCondition);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(timeCount.MaxCount > 0.0f && timeCount.activeFlag)
        {
            conditionFlag = true;
        }
        else
        {
            TextCondition(roundCondition);
            NotText(condition);
        }

        if(conditionFlag)
        {
            TextCondition(condition);
            NotText(roundCondition);

            conditionFlag = false;
        }
    }

    void TextCondition(Text textCondition)
    {
        if (enemyDropOutSide.activeSelf)
        {
            textCondition.text = enemyOutSide.allEnemyCount + "体の敵を外側の穴に落とせ!";
            //textCondition.text = "内側には敵を落とすな!";
            // textCondition.text = "外側にだけ" + enemyOutSide.allEnemyCount + "匹の敵を外側に落とせ！";
        }

        if (AllEnemyDrop.activeSelf)
        {
            textCondition.text = "とにかく敵を落とせ！";
            //textCondition.text = enemyDrop.allEnemyCount + "匹の敵を落とせ！";
        }

        if(enemyDropInSide.activeSelf)
        {
            textCondition.text = enemyInSide.allEnemyCount + "体の敵を内側の穴に落とせ！";
            //textCondition.text = "外側には敵を落とすな！";
            //textCondition.text = "内側の穴にだけ" + enemyInSide.allEnemyCount + "匹の敵を落とせ！";
        }

        if(enemyBoxIn.activeSelf)
        {
            textCondition.text = "かごに敵を" + enemyBox.allEnemyCount + "体入れろ！";
        }
    }

    //テキストを表示させないようにする
    void NotText(Text textCondition)
    {
        textCondition.text = " ";
    }
}
