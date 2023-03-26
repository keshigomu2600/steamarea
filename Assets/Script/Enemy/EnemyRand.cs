using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRand : MonoBehaviour
{
    public const float navCount = 5.0f;
    //移動範囲
    public float xMax, xMin, zMax, zMin;
    //範囲外に出てしまったときの対処
    public float outAreaOverPoint = -11.0f;

    [Header("outAreaの後に敵を消すか")]
    public bool appearEnemy = true;

    const int valueNum = 5;

    Vector3[] points = new Vector3[valueNum];

    private float vecX;
    private float vecZ;
    private float navCountSeconds = 0.0f;

    private int destPoint = 0;
    private NavMeshAgent agent;
    private Rigidbody rb;

    //真ん中に落ちたか判定するフラグ
    bool dropMid = false;

    //外側に落とした数を数えるスクリプトをアタッチしたオブジェクト
    public GameObject enemyDropOutside;

    //内側に落とした数を数えるスクリプトをアタッチしたオブジェクト
    public GameObject enemyDropInside;

    //落とした敵の数を数えるスクリプトをアタッチしたオブジェクト
    public GameObject allEnemuDropCondition;

    //boxのスクリプト
    public GameObject EnemyBoxIn;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        agent.autoBraking = false;

        //移動する位置をランダムに選択
        for (int i = 0; i < points.Length; i++) 
        {
            vecX = Random.Range(xMin, xMax);
            vecZ = Random.Range(zMin, zMax);
            points[i] = new Vector3(vecX, transform.position.y, vecZ);

            //Debug.Log(points[i]);
        }

        AgentStop();
    }


    void Update()
    {
        if (agent.enabled)
        {
            if (!agent.pathPending && agent.remainingDistance < 5.0f)
                GotoNextPoint();
        }


        //はじかれた後時間経過で元に戻す
        if (!agent.enabled)
        {
            navCountSeconds += Time.deltaTime;

            if (navCountSeconds >= navCount)
            {
                agent.enabled = true;
                rb.isKinematic = true;
                navCountSeconds = 0.0f;
                GotoNextPoint();
            }
        }

        //Debug.Log(transform.position);
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

    void GotoNextPoint()
    {
        if (points.Length == 0)
            return;

        agent.destination = points[destPoint];

        destPoint = (destPoint + 1) % points.Length;
    }

    //敵をナビゲーションさせない
    void AgentStop()
    {
        agent.enabled = false;
        rb.isKinematic = false;
        transform.position = transform.position + new Vector3(0.0f, 0.0f, 0.0f) * Time.deltaTime;
    }
}
