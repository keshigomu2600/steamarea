using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    [Header("開く床")]
    public GameObject collisionEnterArea;
    [Header("ボタンの押し込み")]
    public GameObject button;

    [Header("開く床の距離")]
    public float areaDistance;
    [Header("開く時のスピード")]
    public float speed = 0.1f;

    bool moveFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(moveFlag)
        {
            if(collisionEnterArea.transform.position.y <= areaDistance)
            {
                collisionEnterArea.transform.Translate(0.0f, speed, 0.0f);
                button.transform.Translate(0.0f, -0.02f, 0.0f);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            moveFlag = true;
        }
    }
}
