using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespornRand : MonoBehaviour
{
    //出現させる敵
    public GameObject[] enemy;
    //対象ステージ
    public GameObject stage;
    //カウント（一定時間になったら出現させる）
    public TimeCount timeCount;
    //範囲用
    public Transform xMin;
    public Transform xMax;
    public Transform zMin;
    public Transform zMax;

    //その出現させる一定時間
    //public float count = 30.0f;

    //敵を全部消去したか
    public bool delateEnemy = false;

    //範囲
    float xNega, xPosi, zNega, zPosi;
    //敵は出現していいか
    bool enemyApp = true;
    //非アクティブだったか
    bool enemyNotActive = false;
    //格納されたか
    [SerializeField]
    public bool cpFlag = false;

    //格納先
    [SerializeField]
    /*public */ArrayList copiedEnemy = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < enemy.Length; i++)
        {
            copiedEnemy.Add(enemy[i]);
        }
        Initial();
        Apper();
    }

    // Update is called once per frame
    void Update()
    {
        if(stage.activeSelf)
        {
            if (enemyApp)
            {
                Apper();
            }
        }

        if (!timeCount.activeFlag)
        {
            for (int i = 0; i < enemy.Length; i++)
            {
                //ラウンドが経過したら全消去
                Destroy((Object)copiedEnemy[i]);
                Initial();
                delateEnemy = true;
            }
        }
    }

    public void Initial()
    {
        cpFlag = false;

        xNega = xMin.position.x;
        xPosi = xMax.position.x;
        zNega = zMin.position.z;
        zPosi = zMax.position.z;

        enemyApp = true;
        cpFlag = true;
    }

    void Apper()
    {
        //一定数越えたら
        for (int i = 0; i < enemy.Length; i++)
        {
            //0 ~ 要素数 - 1の敵の種類をランダムで排出
            //int randEnemy = Random.Range(0, enemy.Length);

            //敵が非アクティブだった場合の時一時的にアクティブにする
            if (!enemy[i].activeSelf)
            {
                enemy[i].gameObject.SetActive(true);
                enemyNotActive = true;
            }
            //複製作成(transform.positionを変える)
            float vecX = Random.Range(xNega, xPosi);
            float vecZ = Random.Range(zNega, zPosi);
            copiedEnemy[i] = Instantiate(enemy[i], new Vector3(vecX, transform.position.y, vecZ), Quaternion.identity);

            //Debug.Log(vecX);
            //Debug.Log(vecZ);
            //元々非アクティブだった場合は戻す
            if (enemyNotActive)
            {
                enemy[i].gameObject.SetActive(false);
            }
            //敵を出現させるかどうかのフラグをオフ
            enemyApp = false;
            //敵が出現したフラグ
            delateEnemy = false;
        }
    }
}

