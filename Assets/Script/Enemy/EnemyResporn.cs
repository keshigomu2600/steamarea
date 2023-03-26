using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyResporn : MonoBehaviour
{
    //出現させる敵
    public GameObject enemy;
    //カウント（一定時間になったら出現させる）
    public TimeCount timeCount;
    //その出現させる一定時間
    public float count = 30.0f;

    //格納先
    GameObject copiedEnemy;
    //敵は出現していいか
    bool enemyApp = true;
    //非アクティブだったか
    bool enemyNotActive = false;

    // Start is called before the first frame update
    void Start()
    {
        //オブジェクト格納
        copiedEnemy = enemy;
    }

    // Update is called once per frame
    void Update()
    {
        //カウントが始まったら
        if(timeCount.MaxCount < count && timeCount.MaxCount > count - 1.0f && enemyApp)
        {
            //敵が非アクティブだった場合の時一時的にアクティブにする
            if(!enemy.activeSelf)
            {
                enemy.gameObject.SetActive(true);
                enemyNotActive = true;
            }
            //複製作成
            Instantiate(copiedEnemy, transform.position, Quaternion.identity);
            //元々非アクティブだった場合は戻す
            if(enemyNotActive)
            {
                enemy.gameObject.SetActive(false);
            }
            //敵を出現させるかどうかのフラグを一時的にオフ
            enemyApp = false;
        }
        if(timeCount.MaxCount < count - 1.0f)
        {
            //1秒過ぎたらフラグオン
            enemyApp = true;
        }
    }
}
