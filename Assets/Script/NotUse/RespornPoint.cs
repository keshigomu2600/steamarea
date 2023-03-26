using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespornPoint : MonoBehaviour
{
    //���X�|�[���n�_���̂̏����ł��B����Ă���e��y���W�Œ�̂݁B
    //�ǋL�F������΂������̂ŕς��܂����B�v���C���[���n�ʂɂ��Ă��Ȃ����͑S�ʒ�~
    public GameObject target;
    public PlayerController playerController;

    //�ꎞ�ϐ�
    Vector3 dis;
    float y;
    //�������W
    Vector3 initialCoor;

    //�����Ńv���C���[�͗��������i����������W�Œ�j
    bool checkPoint = false;

    private void Start()
    {
        //�������W
        initialCoor = transform.localPosition;
        //y���W�Œ�
        y = target.transform.position.y;
    }
    // Update is called once per frame
    void FixedUpdate()
    {  
        if(playerController.grounded)
        {
            if(checkPoint)
            {
                //�����ʒu����
                transform.localPosition = initialCoor;
                checkPoint = false;
            }
            //�Œ�ʒu
            dis = initialCoor;
            //���X�|�[���n�_���̉��ɗ����Ȃ��悤��y���W�Œ�
            dis.y = y;
            transform.localPosition = dis;

            //�����͎g��Ȃ����Ȃ̂ŃR�����g�A�E�g���Ă����܂���
            //Debug.Log(initialCoor);
        }
        else
        {
            if(!checkPoint)
            {
                //�Œ�
                dis = transform.position;
                dis.y = y;
                checkPoint = true;
            }
            transform.position = dis;
        }
    }

}
