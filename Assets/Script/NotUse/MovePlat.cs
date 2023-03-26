using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlat : MonoBehaviour
{
    //動くプラットフォームの処理です。座標で往復させたかったんですが出来ませんでした。要検討。

    //正の数右　負の数左
    public float direction = 1;
    public int countTime = 0;

    float count;

    GameObject player;

    void Update()
    {
        //往復処理（時間で往復）
        count += Time.deltaTime;
    }

    void FixedUpdate()
    {
        Vector3 vector = direction * Vector3.right * Time.deltaTime;
        transform.Translate(vector);

        if (count >= countTime)
        {
            count = 0;
            direction *= -1;
        }

        //プレイヤーを動かす

        if (player)
        {
            player.transform.Translate(vector, Space.World);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = null;
        }
    }
}
