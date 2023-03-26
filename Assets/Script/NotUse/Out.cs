using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Out : MonoBehaviour
{
    //地面に落ちたときの処理です。カメラワークはまだ出来ていないのですが
    //プレイヤーが地面についているときだけリスポーン地点を動かして→落ちたら落ちる前(z軸-10.0f)ポイントを記憶、
    //落ちた判定を行ったらそこの地点に座標移動してます。挙動としてはどんどん後ろに下がっていく感じ
    public GameObject target;
    public GameObject point;

    //ウェイト時間(これ消してTime.deltatime使う方がいいかも)
    public int flameC;

    //演出用
    bool waitMode = false;
    public bool outArea = false;
    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(outArea)
        {
            //演出
            count++;
            if(count > flameC)
            {
                //演出が終わったらフラグを立てる
                waitMode = true;
                count = 0;
            }
        }

        if (waitMode)
        {
            //リスポーンポイントに移動
            target.transform.position = point.transform.position;
            outArea = false;
            waitMode = false;
        }
    }

    //落ちる処理
    private void OnTriggerEnter(Collider collider)
    {
        //プラットフォームと衝突判定がある場合
        if (collider.gameObject.tag == "Player")
        {
            outArea = true;
        }
    }
}
