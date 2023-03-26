using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class allEnemyDrop : MonoBehaviour
{
    //時間を強制中断するために追加で入れました
    public TimeCount timeCount;
    //ステージによって敵の数が変わるので該当するステージオブジェクトを追加しました。
    public GameObject stageDonuts;

    public PlayerOut playerOut;

    public int allEnemyCount = 20;
    //別ステージの敵の数です
    public int allEnemyOutside = 10;
    [HideInInspector]
    public bool clearFlag = false;

    int enemyCount = 0;
    int initializeEnemy = 0;
    bool StageEnemyNumberFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        initializeEnemy = allEnemyCount;
        Initialize();
        StageEnemyNumber();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyCount >= allEnemyCount)
        {
            timeCount.MaxCount = 0.0f;
            clearFlag = true;
        }
        else
        {
            clearFlag = false;
        }

        //時間切れ
        if(enemyCount < allEnemyCount && timeCount.MaxCount == 0.0f)
        {
            clearFlag = false;
            enemyCount = 0;
        }

        if (clearFlag)
        {
            print("clear");
            enemyCount = 0;
        }

        if (playerOut.outFlag)
        {
            RoundFailed();
        }

        //条件が終わったら次のステージの敵の数を再構築する
        if (clearFlag || timeCount.MaxCount == 0.0f)
        {
            if(!StageEnemyNumberFlag)
            {
                StageEnemyNumber();
                StageEnemyNumberFlag = true;
            }
        }
        else
        {
            StageEnemyNumberFlag = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    void RoundFailed()
    {
        print("Failed");
        clearFlag = false;
        timeCount.MaxCount = 0.0f;
        enemyCount = 0;
    }

    void DropEnemyCount()
    {
        enemyCount++;
    }

    void Initialize()
    {
        clearFlag = false;
    }

    void StageEnemyNumber()
    {
        //ドーナツステージがアクティブなら敵の数加算+時間
        if (stageDonuts.activeSelf)
        {
            allEnemyCount = initializeEnemy + allEnemyOutside;
        }
        else
        {
            allEnemyCount = initializeEnemy;
        }
    }
}
