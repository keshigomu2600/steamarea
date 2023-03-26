using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNotMove : MonoBehaviour
{
    //敵として動く最低限の処理しかしてません

    //ターゲットオブジェクトの Transformコンポーネントを格納する変数
    public Transform target;
    //真ん中に落ちたか判定するフラグ
    bool dropMid = false;

    //外側に落とした数を数えるスクリプトをアタッチしたオブジェクト
    public GameObject enemyDropOutside;

    //内側
    public GameObject enemyDropInside;

    //落とした敵の数を数えるスクリプトをアタッチしたオブジェクト
    public GameObject allEnemuDropCondition;

    public GameObject EnemyBoxIn;

    [Header("outAreaの後に敵を消すか")]
    public bool appearEnemy = true;

    public Animator anim;
    float browedAwayPlayTime;

    void Start()
    {

    }

    void Update()
    {

        //吹き飛ばされてるアニメーションが再生される時間を求める
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("BrowedAway"))
            browedAwayPlayTime += Time.deltaTime;
        else
            browedAwayPlayTime = 0;

        if (browedAwayPlayTime > 3f)
            anim.Play("Base Layer.None");

        // 変数 targetPos を作成してターゲットオブジェクトの座標を格納
        Vector3 targetPos = target.position;
        targetPos.y = transform.position.y;

        // 変数 distance を作成してオブジェクトの位置とターゲットオブジェクトの距離を格納
        float distance = Vector3.Distance(transform.position, target.position);

        // オブジェクトを変数 target の座標方向に向かせる
        transform.LookAt(targetPos);
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.position = transform.position + new Vector3(0.0f, 0.0f, 0.0f) * Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //outAreaに入ったら消滅
        if (other.tag == "outArea")
        {
            //敵を全部落とすラウンドの落ちた敵をカウントするための鮮度メッセージを追加しました
            allEnemuDropCondition.SendMessage("DropEnemyCount", SendMessageOptions.DontRequireReceiver);
            EnemyBoxIn.SendMessage("DropEnemyCount", SendMessageOptions.DontRequireReceiver);

            if (!dropMid)
            {
                enemyDropOutside.SendMessage("DropEnemyCount", SendMessageOptions.DontRequireReceiver);
                enemyDropInside.SendMessage("RoundFailed", SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                enemyDropOutside.SendMessage("RoundFailed", SendMessageOptions.DontRequireReceiver);
                enemyDropInside.SendMessage("DropEnemyCount", SendMessageOptions.DontRequireReceiver);
            }

            if (appearEnemy)
            {
                this.gameObject.SetActive(false);
            }
        }

        //内側に落ちたかどうかを確認するための判定を追加しました
        if (other.tag == "BlueZone")
        {
            dropMid = true;
        }
    }
    void PlayBrowedAwayAnimation()
    {
        anim.Play("Base Layer.BrowedAway");
        browedAwayPlayTime = 0f;
    }
}

