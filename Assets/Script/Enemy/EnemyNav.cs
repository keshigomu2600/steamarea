using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//アニメーション系は大陸さんが組みました
public class EnemyNav : MonoBehaviour
{
    public Transform[] points;
    //気絶時間
    public const float navCount = 5.0f;
    //範囲外に出てしまったときの対処
    public float outAreaOverPoint = -11.0f;

    [Header("outAreaの後に敵を消すか")]
    public bool appearEnemy = true;

    private int destPoint = 0;
    private NavMeshAgent agent;
    private Rigidbody rb;
    private float navCountSeconds = 0.0f;

    //真ん中に落ちたか判定するフラグ
    bool dropMid = false;

    //外側に落とした数を数えるスクリプトをアタッチしたオブジェクト
    public GameObject enemyDropOutside;
    
    //内側に落とした数を数えるスクリプトをアタッチしたオブジェクト
    public GameObject enemyDropInside;

    //落とした敵の数を数えるスクリプトをアタッチしたオブジェクト
    public GameObject allEnemuDropCondition;

    public GameObject EnemyBoxIn;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        agent.autoBraking = false;

        AgentStop();
    }

    void GotoNextPoint()
    {
        if (points.Length == 0)
            return;

        agent.destination = points[destPoint].position;

        destPoint = (destPoint + 1) % points.Length;
    }

    void Update()
    {
        if(agent.enabled)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();
        }

        //はじかれた後時間経過で元に戻す
        if(!agent.enabled)
        {
            navCountSeconds += Time.deltaTime;

            if(navCountSeconds >= navCount)
            {
                agent.enabled = true;
                rb.isKinematic = true;
                navCountSeconds = 0.0f;
                GotoNextPoint();
            }
        }
    }

    void FixedUpdate()
    {
        //範囲外に出てしまった場合の処理
        if (transform.position.y < outAreaOverPoint)
        {
            this.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //蒸気に当たったときのみナビゲーションoff
        if (other.tag == "Player")
        {
            AgentStop();
        }

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

    //敵をナビゲーションさせない
    void AgentStop()
    {
        agent.enabled = false;
        rb.isKinematic = false;
        transform.position = transform.position + new Vector3(0.0f, 0.0f, 0.0f) * Time.deltaTime;
    }
}
