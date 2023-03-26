using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespornPoint : MonoBehaviour
{
    //リスポーン地点自体の処理です。やってる内容はy座標固定のみ。
    //追記：挙動やばかったので変えました。プレイヤーが地面についていない時は全面停止
    public GameObject target;
    public PlayerController playerController;

    //一時変数
    Vector3 dis;
    float y;
    //初期座標
    Vector3 initialCoor;

    //何処でプレイヤーは落ちたか（落ちたら座標固定）
    bool checkPoint = false;

    private void Start()
    {
        //初期座標
        initialCoor = transform.localPosition;
        //y座標固定
        y = target.transform.position.y;
    }
    // Update is called once per frame
    void FixedUpdate()
    {  
        if(playerController.grounded)
        {
            if(checkPoint)
            {
                //初期位置を代入
                transform.localPosition = initialCoor;
                checkPoint = false;
            }
            //固定位置
            dis = initialCoor;
            //リスポーン地点自体下に落ちないようにy座標固定
            dis.y = y;
            transform.localPosition = dis;

            //↓今は使わなそうなのでコメントアウトしておきました
            //Debug.Log(initialCoor);
        }
        else
        {
            if(!checkPoint)
            {
                //固定
                dis = transform.position;
                dis.y = y;
                checkPoint = true;
            }
            transform.position = dis;
        }
    }

}
