using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlat : MonoBehaviour
{
    //�����v���b�g�t�H�[���̏����ł��B���W�ŉ�����������������ł����o���܂���ł����B�v�����B

    //���̐��E�@���̐���
    public float direction = 1;
    public int countTime = 0;

    float count;

    GameObject player;

    void Update()
    {
        //���������i���Ԃŉ����j
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

        //�v���C���[�𓮂���

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
