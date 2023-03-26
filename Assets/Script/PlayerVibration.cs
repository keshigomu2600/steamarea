using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVibration : MonoBehaviour
{
    [Header("U“®‚·‚éXÀ•W")]
    public float vibrationX = 2.0f;
    [Header("U“®‚·‚éZÀ•W")]
    public float vibrationZ = 2.0f;
    [Header("U“®‘¬“x")]
    public float speed = 10.0f;

    [Header("U“®‚·‚éŠÔ(d’¼ŠÔ)")]
    public float vibrationTime = 3.0f;

    bool vibrationFlag = false;
    float coolTime = 0.0f;

    //Œü‚«
    // «‘Œ^‚Ì•Ï”
    Dictionary<string, bool> move = new Dictionary<string, bool>
    {
        {"right", false },
        {"left", false },
    };

    //Œ³‚ÌˆÊ’u
    Vector3 vec;

    // Start is called before the first frame update
    void Start()
    {
        vec = transform.localPosition;
        move["right"] = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(vibrationFlag)
        {
            coolTime += Time.deltaTime;

            if(coolTime > vibrationTime)
            {
                coolTime = 0.0f;
                transform.localPosition = vec;
                vibrationFlag = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (vibrationFlag)
        {
            if(move["right"])
            {
                transform.localPosition += new Vector3(speed, 0.0f, speed);
            }
            if(move["left"])
            {
                transform.localPosition -= new Vector3(speed, 0.0f, speed);
            }

            if(transform.localPosition.x >vec.x + vibrationX || transform.localPosition.z > vec.z + vibrationZ)
            {
                move["right"] = false;
                move["left"] = true;
            }
            if (transform.localPosition.x < vec.x - vibrationX || transform.localPosition.z < vec.z - vibrationZ)
            {
                move["right"] = true;
                move["left"] = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //“–‚½‚Á‚ÄvirationTime•bŠÔU“®‚µ‚Ü‚·B
        if (other.tag == "shotBall" && !vibrationFlag)
        {
            vibrationFlag = true;
        }
    }
}
