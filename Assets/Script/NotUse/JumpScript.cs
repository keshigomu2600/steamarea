using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour
{
    //EnemyFlag�p
    public PlayerJumpSimple playerJumpSimple;

    float x = 0;
    float y = 0;
    float z = 0;

    public GameObject target;
    Vector3 targetPos;

    float position = 0;

    float rightVertical;
    float rightHorizontal;

    bool jumpFlagleft = false;
    bool jumpFlagRight = false;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = target.transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if(!playerJumpSimple.enemyHit)

        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                jumpFlagleft = true;
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                jumpFlagRight = true;
            }

        }
    }
    void FixedUpdate()
    {
        if (jumpFlagleft)
        {
            if (x <= targetPos.x - 0.1f + position)
            {
                x += targetPos.x * Time.deltaTime * 0.5f;
                z += targetPos.z * Time.deltaTime * 0.5f;
                y = -Mathf.Pow(x - targetPos.x / 2 - position, 2) + Mathf.Pow(targetPos.x / 2,2);

                gameObject.transform.position = new Vector3(x, y, z);
            }
            else
            {
                position += targetPos.x;
                jumpFlagleft = false;
            }
        }

        if (jumpFlagRight)
        {
            if (x <= targetPos.x - 0.1f + position)
            {
                x += targetPos.x * Time.deltaTime * 0.5f;
                z -= targetPos.z * Time.deltaTime * 0.5f;
                y = -Mathf.Pow(x - targetPos.x/2 - position, 2) + Mathf.Pow(targetPos.x / 2, 2);

                gameObject.transform.position = new Vector3(x, y, z);
            }
            else
            {
                position += 10;
                jumpFlagRight = false;
            }
        }
    }
}