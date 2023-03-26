using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//弾に当たった時のクールタイム等の処理は寺林(53～56、246～258行目)

public class PlayerControllerFreeCameraVer : MonoBehaviour
{
    enum Direction
    {
        None,
        Up,
        Right,
        Down,
        Left,
    }

    [Header("ポーズ画面表示の時は音を鳴らさない")]
    public GameObject pouseObject;

    public AudioSource walksound;

    public GameObject animObject;
    Animator walkAnim;
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

    public GameObject particle;

    public Transform groundPos;

    public LayerMask ground;

    //�W�����v�̃p���[�����߂Ă��邩�ǂ���
    bool isChargeJumpPower = false;
    //���������ɋl�܂�Ȃ��悤�Ɉړ�������ׂ̃t���O
    bool isSpeed = false;
    //操作可能かどうか
    bool OperationPossible = true;

    public bool canMove = false;

    //クールタイム
    float coolTime = 0;
    [Header("敵の球に当たったときのクールタイム")]
    public float coolTimePoint = 3.0f;

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
        walkAnim = animObject.GetComponent<Animator>();
        walkAnim.speed = 0f;
        walksound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //���X�e�B�b�N�̓��͒l
        float verticalLeft = Input.GetAxis("L_Vertical");
        float horizontalLeft = Input.GetAxis("L_Horizontal");

        //�E�X�e�B�b�N�̓��͒l
        //float verticalRight = Input.GetAxis("R_Vertical");
        //float horizontalRight = Input.GetAxis("R_Horizontal");

        //�����̓��͂����ꂽ���n�ʂɂ���Ƃ��̂�(grounded�̈Ӗ��������Ȃ��Ă��̂�if���ŕ����Ă݂܂���)
        if (grounded && OperationPossible && canMove)
        {
            //if (horizontalLeft < -0.5f || horizontalLeft > 0.5f || verticalLeft > 0.5f || verticalLeft < -0.5f)
            if (Mathf.Abs(new Vector2(verticalLeft, horizontalLeft).magnitude) > 0.5f) 
            {

                //�J�����̕�������XZ���ʂɊւ���P�ʃx�N�g���𓾂�
                Vector3 playerForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
                //�J�����̌����Ɠ��͒l����ړ������𓾂�
                Vector3 moveForward = playerForward * -verticalLeft + Camera.main.transform.right * horizontalLeft;

                //�ړ�
                rg.velocity = moveForward * speed;
                //�i�s����������
                transform.rotation = Quaternion.LookRotation(moveForward);

                walkAnim.speed = 1f;

                //歩くSE
                if (!walksound.isPlaying && !pouseObject.activeSelf)
                {
                    walksound.Play();
                }
            }
            else
            {
                rg.velocity = new Vector3(0.0f,0.0f,0.0f);
                walkAnim.speed = 0f;
                walksound.Stop();
            }
            //↓使わなくなった
            ////ジャンプ処理
            //if (verticalRight > 0.8f)
            //{
            //    isChargeJumpPower = true;
            //}

            //Debug.Log(rightStickDirection);
            //if (isChargeJumpPower)
            //{
            //    if (verticalRight > 0 && horizontalRight < 0.6)
            //    {
            //        rightStickDirection = Direction.Down;
            //    }
            //    else if (verticalRight < 0.6 && horizontalRight < 0)
            //    {
            //        rightStickDirection = Direction.Left;
            //    }
            //    else if (verticalRight < 0 && horizontalRight < 0.6)
            //    {
            //        rightStickDirection = Direction.Up;
            //    }
            //    else if (verticalRight < 0.6 && horizontalRight > 0)
            //    {
            //        rightStickDirection = Direction.Right;
            //    }
            //    else if (verticalRight < 0.6 && horizontalRight < 0.6)
            //    {
            //        //�X�e�B�b�N�������ꂽ������֘A������������
            //        rightStickDirection = Direction.None;
            //        rightStickRotateDirection = Direction.None;

            //        //�E�X�e�B�b�N�������ꂽ��t���O������
            //        isChargeJumpPower = false;
            //    }
            //}
        }

        //弾に当たったときのクールタイム
        if(!OperationPossible)
        {
            coolTime += Time.deltaTime;
            rg.constraints = RigidbodyConstraints.FreezeAll;
            walkAnim.speed = 0f;
            if (coolTime > coolTimePoint)
            {
                //FreezeRotationZ,FreezeRotationXをオンにする
                rg.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
                OperationPossible = true;
                coolTime = 0.0f;
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
            //particle.GetComponent<ParticleSystem>().startSize = 0f;
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

    private void OnTriggerEnter(Collider other)
    {
        //何処にこれ入れようか迷ったのでここに置いときました
        if (other.tag == "shotBall" && OperationPossible)
        {
            //var rb = GetComponent<Rigidbody>();

            //Vector3 vector = gameObject.transform.position - other.transform.position;
            //rb.AddForce(vector.normalized * 6, ForceMode.Impulse);

            OperationPossible = false;
        }
    }

    void CanMove(bool CanMove)
    {
        canMove = CanMove;
    }
}