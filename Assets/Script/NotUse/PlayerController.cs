using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum Direction
    {
        None,
        Up,
        Right,
        Down,
        Left,
    }

    //�v���C���[���O�i���鑬�x 
    public float speed = 5f;

    //�v���C���[�̃W�����v����p���[
    float jumpPower = 0f;
    public float deltaJumpPower = 1f;
    public float jumpPowerMax = 32f;

    //�ړ����]�̍ۂ̃t���[�����[�g���J�E���g����
    int movingFlameCount;

    //�n�ʂɂ��邩�ǂ���(���X�|�[���n�_�������Ȃ��悤��public�ɂ��܂����BC#�Ȃ�����Ƃ������@���邩������Ȃ�)
    public bool grounded = false;

    //�W�����v�̃p���[�����߂Ă��邩�ǂ���
    bool isChargeJumpPower = false;
    //���������ɋl�܂�Ȃ��悤�Ɉړ�������ׂ̃t���O
    bool isSpeed = false;

    public GameObject particle;

    public Transform groundPos;

    public LayerMask ground;

    //�W�����v�p���[�i�[�p
    Rigidbody rg;

    //�E�X�e�B�b�N�̌���
    Direction rightStickDirection;
    Direction rightStickDirectionOld;

    Direction rightStickRotateDirection = Direction.None;

    // Start is called before the first frame update
    void Start()
    {
        //�t���[�����[�g��60�ɌŒ肷��
        Application.targetFrameRate = 60;
        //rigidbody�i�[
        rg = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //���X�e�B�b�N�̓��͒l
        float verticalLeft = Input.GetAxis("L_Vertical");
        float horizontalLeft = Input.GetAxis("L_Horizontal");

        //�E�X�e�B�b�N�̓��͒l
        float verticalRight = Input.GetAxis("R_Vertical");
        float horizontalRight = Input.GetAxis("R_Horizontal");

        //�����̓��͂����ꂽ���n�ʂɂ���Ƃ��̂�(grounded�̈Ӗ��������Ȃ��Ă��̂�if���ŕ����Ă݂܂���)
        if (grounded)
        {
            if (horizontalLeft != 0 || verticalLeft != 0)
            {
                //�J�����̕�������XZ���ʂɊւ���P�ʃx�N�g���𓾂�
                Vector3 playerForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
                //�J�����̌����Ɠ��͒l����ړ������𓾂�
                Vector3 moveForward = playerForward * -verticalLeft + Camera.main.transform.right * horizontalLeft;

                //���͂�����Ă���30�t���[�������Ă���Ƃ�
                if (movingFlameCount > 30)
                {
                    //�O�ɐi��
                    transform.Translate(speed * Vector3.forward * Time.deltaTime);
                }
                else
                {
                    //�J�����̕����x�N�g���Ɠ��͒l����̈ړ������x�N�g���̊p�x��2�{�����l�����߂�
                    float angle = 2 * Vector3.Angle(playerForward, moveForward);

                    //�������̓��͂��������ꍇ�p�x�𕉂ɂ���
                    if (horizontalLeft < 0)
                    {
                        angle = -angle;
                    }

                    //�����]��������
                    transform.Rotate(0f, angle * Time.deltaTime, 0f);
                }

                movingFlameCount++;
            }
            else
            {
                movingFlameCount = 0;
            }

            //�E�X�e�B�b�N�ŉ������̓��͂��������ꍇ�t���O�𗧂Ă�
            if (verticalRight == 1)
            {
                isChargeJumpPower = true;
            }

            //�������̓��͌�A�E�X�e�B�b�N�̓��͕�����4�����ɐU�蕪����
            if (isChargeJumpPower)
            {
                if (verticalRight > 0 && horizontalRight == 0)
                {
                    rightStickDirection = Direction.Down;
                }
                else if (verticalRight == 0 && horizontalRight < 0)
                {
                    rightStickDirection = Direction.Left;
                }
                else if (verticalRight < 0 && horizontalRight == 0)
                {
                    rightStickDirection = Direction.Up;
                }
                else if (verticalRight == 0 && horizontalRight > 0)
                {
                    rightStickDirection = Direction.Right;
                }
                else if (verticalRight == 0 && horizontalRight == 0)
                {
                    //�X�e�B�b�N�������ꂽ������֘A������������
                    rightStickDirection = Direction.None;
                    rightStickRotateDirection = Direction.None;

                    //�E�X�e�B�b�N�������ꂽ��t���O������
                    isChargeJumpPower = false;
                }
            }
        }
    }

    [System.Obsolete]
    private void FixedUpdate()
    {
        if (isChargeJumpPower)
        {
            //�W�����v�p���[���ő�l�ȉ��̎��̂݃p���[�����߂�
            if (jumpPower < jumpPowerMax)
            {
                //��]���������܂��ĂȂ��ꍇ
                if (rightStickRotateDirection == Direction.None)
                {
                    //��]�������擾����
                    if (rightStickDirection == Direction.Right)
                    {
                        rightStickRotateDirection = Direction.Left;
                    }
                    else if (rightStickDirection == Direction.Left)
                    {
                        rightStickRotateDirection = Direction.Right;
                    }
                }
                //�E��]�̎�
                else if (rightStickRotateDirection == Direction.Right)
                {
                    //��]�̌�������v���Ă����ꍇ�p���[�����߂�
                    if ((int)rightStickDirection == (int)rightStickDirectionOld % 4 + 1)
                    {
                        jumpPower++;
                    }
                }
                //����]�̎�
                else
                {
                    //��]�̌�������v���Ă����ꍇ�p���[�����߂�
                    if ((int)rightStickDirection % 4 == (int)rightStickDirectionOld - 1)
                    {
                        jumpPower++;
                    }
                }
            }

            //1�t���[���O�̌����̏���ۑ�����
            rightStickDirectionOld = rightStickDirection;
            //particle.GetComponent<ParticleSystem>().startLifetime = jumpPower * 0.5f;
            particle.GetComponent<ParticleSystem>().startSize = jumpPower * 0.5f;
        }
        else
        {
            //�W�����v����p���[��^����(rg�Ɋi�[���܂���)
            rg.AddForce(transform.TransformDirection(new Vector3(0, 2, 1)) * jumpPower * deltaJumpPower, ForceMode.Impulse);
            jumpPower = 0f;
            //particle.GetComponent<ParticleSystem>().startLifetime = 0f;
            particle.GetComponent<ParticleSystem>().startSize = 0f;
        }

        if (Physics.Raycast(groundPos.position, Vector3.down, 0.1f, ground))
        {
            grounded = true;
            isSpeed = false;
        }
        else
        {
            grounded = false;
            if (!isSpeed)
            {
                rg.AddForce(transform.TransformDirection(new Vector3(0, 0, 2.0f)), ForceMode.Impulse);
                isSpeed = true;
            }
        }
    }
}