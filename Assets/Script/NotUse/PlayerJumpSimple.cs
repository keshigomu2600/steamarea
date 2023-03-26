//�g��Ȃ��Ǝv���܂�
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpSimple : MonoBehaviour
{
    enum Direction
    {
        None,
        Up,
        Right,
        Down,
        Left,
    }

    Direction rightStickRotateDirection = Direction.None;
    Direction rightStickDirection;
    Direction rightStickDirectionOld;

    //up,right,down,left��1�������Ă����A4�ɂȂ�����1��]����ɂ���
    [HideInInspector]
    public int directionFlag = 0;
    [HideInInspector]
    public bool enemyHit = false;

    float stickValue = 0.5f;
    //�R���t�B�O���������Ƃ��������肳�������̂ŉ����܂�������Ă����t���O�����
    bool configFlag = false;

    // Update is called once per frame
    void Update()
    {
        float verticalRight = Input.GetAxis("R_Vertical");
        float horizontalRight = Input.GetAxis("R_Horizontal");

        //�f�o�b�O
        //Debug.Log(rightStickDirection);
        //Debug.Log(directionFlag);

        if (enemyHit)
        {
            if (horizontalRight < stickValue && horizontalRight > -stickValue)
            {
                if (verticalRight > stickValue)
                {
                    rightStickDirection = Direction.Up;
                    if (!configFlag)
                    {
                        directionFlag++;
                    }
                    configFlag = true;

                }
                else if (verticalRight < -stickValue)
                {
                    rightStickDirection = Direction.Down;
                    if (!configFlag)
                    {
                        directionFlag++;
                    }
                    configFlag = true;
                }
                else
                {
                    rightStickDirection = Direction.None;
                    rightStickRotateDirection = Direction.None;
                    configFlag = false;
                }
            }
            else if (verticalRight < stickValue && verticalRight > -stickValue)
            {
                if (horizontalRight < -stickValue)
                {
                    rightStickDirection = Direction.Left;
                    if (!configFlag)
                    {
                        directionFlag++;
                    }
                    configFlag = true;
                }
                else if (horizontalRight > stickValue)
                {
                    rightStickDirection = Direction.Right;
                    if (!configFlag)
                    {
                        directionFlag++;
                    }
                    configFlag = true;
                }
                else
                {
                    rightStickDirection = Direction.None;
                    rightStickRotateDirection = Direction.None;
                    configFlag = false;
                }
            }
            else
            {
                rightStickDirection = Direction.None;
                rightStickRotateDirection = Direction.None;
                configFlag = false;
            }

            //4�z������G�𕥂�
            if (directionFlag > 4)
            {
                directionFlag = 0;
            }
        }
    }
}
