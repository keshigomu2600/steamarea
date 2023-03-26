using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHadou : MonoBehaviour
{
    //ターゲットオブジェクトの Transformコンポーネントを格納する変数
    public Transform target;
    // オブジェクトの移動速度を格納する変数
    public float moveSpeed;
    // オブジェクトが停止するターゲットオブジェクトとの距離を格納する変数
    public float stopDistance;
    // オブジェクトがターゲットから移動を開始する距離を格納する変数
    public float moveDistance;
    //後ろに下がるか前へ進むか
    public bool backEnemy = false;

    //public GameObject target;
    public GameObject shotBall;
    public float speed = 1.0f;

    [Header("攻撃のスパン")]
    public float shotBallCoolTime = 5f;
    [Header("outAreaの後に敵を消すか")]
    public bool appearEnemy = true;

    Rigidbody rb;
    Vector3 targetPos;

    bool flag = false;
    GameObject ballObj;
    Ray ray;
    RaycastHit hit;

    //真ん中に落ちたか判定するフラグ
    bool dropMid = false;

    //外側に落とした数を数えるスクリプトをアタッチしたオブジェクト
    public GameObject enemyDropOutside;

    //外側に落とした数を数えるスクリプトをアタッチしたオブジェクト
    public GameObject enemyDropInside;

    //落とした敵の数を数えるスクリプトをアタッチしたオブジェクト
    public GameObject allEnemuDropCondition;

    public GameObject EnemyBoxIn;

    public Animator anim;
    float browedAwayPlayTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        //pos = transform.position;

        //// （ポイント）マイナスをかけることで逆方向に移動する。
        //transform.Translate(transform.right * Time.deltaTime * 3 * direction);

        ////ここを端に行った往復するようにする
        //if (pos.x > -2)
        //{
        //    direction = -1;
        //}
        //if (pos.x < -11)
        //{
        //    direction = 1;
        //}

        float distance = Dis();

        // オブジェクトを変数 target の座標方向に向かせる
        transform.LookAt(targetPos);

        //吹き飛ばされた後は少しの時間動かない
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("BrowedAway"))
        {
            //デバッグ
            //Debug.Log(distance);

            // オブジェクトとターゲットオブジェクトの距離判定
            // 変数 distance（ターゲットオブジェクトとオブジェクトの距離）が変数 moveDistance の値より小さければ
            // さらに変数 distance が変数 stopDistance の値よりも大きい場合

            if (distance < moveDistance && distance > stopDistance)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
                    anim.Play("Base Layer.None");
                transform.position = transform.position + new Vector3(0.0f, 0.0f, 0.0f) * Time.deltaTime;
            }
            else
            {
                if (backEnemy)
                {
                    // 変数 moveSpeed を乗算した速度でオブジェクトを後ろ方向に移動する
                    transform.position = transform.position + (-transform.forward) * moveSpeed * Time.deltaTime;
                }
                else
                {
                    // 変数 moveSpeed を乗算した速度でオブジェクトを前方向に移動する
                    transform.position = transform.position + transform.forward * moveSpeed * Time.deltaTime;
                }
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("None"))
                    anim.Play("Base Layer.Walk");
            }
        }
    }

    void FixedUpdate()
    {
        float distance = Dis();

        // 吹き飛ばされた後は少しの時間動かない
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("BrowedAway"))
        {
            if (distance < moveDistance && distance > stopDistance)
            {
                ray = new Ray(transform.position, transform.forward);

                //当たったらプレイヤーに向かって球を打つ
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.collider.tag == "Player")
                    {
                        if (!flag)
                        {
                            ballObj = Instantiate(shotBall, transform.position, Quaternion.identity);
                            Rigidbody bRb = ballObj.GetComponent<Rigidbody>();
                            bRb.AddForce(transform.forward * speed);
                            FlagChange();
                            Destroy(ballObj, shotBallCoolTime);
                        }

                        if (!ballObj)
                        {
                            FlagChange();
                        }
                    }
                }
            }

            //デバッグ
            //Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
        }
    }

    //ヒットさせたら3秒止まる
    //void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        playerJumpSimple.enemyHit = true;
    //    }
    //}

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.position = transform.position + new Vector3(0.0f, 0.0f, 0.0f) * Time.deltaTime;
        }
    }

    float Dis()
    {
        // 変数 targetPos を作成してターゲットオブジェクトの座標を格納
        targetPos = target.position;
        targetPos.y = transform.position.y;

        // 変数 distance を作成してオブジェクトの位置とターゲットオブジェクトの距離を格納
        return Vector3.Distance(transform.position, target.position);
    }

    public void FlagChange()
    {
        if (!flag)
        {
            flag = true;
        }
        else
        {
            flag = false;
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