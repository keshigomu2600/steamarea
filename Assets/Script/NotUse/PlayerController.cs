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

    //プレイヤーが前進する速度 
    public float speed = 5f;

    //プレイヤーのジャンプするパワー
    float jumpPower = 0f;
    public float deltaJumpPower = 1f;
    public float jumpPowerMax = 32f;

    //移動や回転の際のフレームレートをカウントする
    int movingFlameCount;

    //地面にいるかどうか(リスポーン地点が動かないようにpublicにしました。C#ならもっといい方法あるかもしれない)
    public bool grounded = false;

    //ジャンプのパワーをためているかどうか
    bool isChargeJumpPower = false;
    //落ちた時に詰まらないように移動させる為のフラグ
    bool isSpeed = false;

    public GameObject particle;

    public Transform groundPos;

    public LayerMask ground;

    //ジャンプパワー格納用
    Rigidbody rg;

    //右スティックの向き
    Direction rightStickDirection;
    Direction rightStickDirectionOld;

    Direction rightStickRotateDirection = Direction.None;

    // Start is called before the first frame update
    void Start()
    {
        //フレームレートを60に固定する
        Application.targetFrameRate = 60;
        //rigidbody格納
        rg = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //左スティックの入力値
        float verticalLeft = Input.GetAxis("L_Vertical");
        float horizontalLeft = Input.GetAxis("L_Horizontal");

        //右スティックの入力値
        float verticalRight = Input.GetAxis("R_Vertical");
        float horizontalRight = Input.GetAxis("R_Horizontal");

        //方向の入力がされたかつ地面にいるときのみ(groundedの意味が無くなってたのでif文で分けてみました)
        if (grounded)
        {
            if (horizontalLeft != 0 || verticalLeft != 0)
            {
                //カメラの方向からXZ平面に関する単位ベクトルを得る
                Vector3 playerForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
                //カメラの向きと入力値から移動方向を得る
                Vector3 moveForward = playerForward * -verticalLeft + Camera.main.transform.right * horizontalLeft;

                //入力がされてから30フレームたっているとき
                if (movingFlameCount > 30)
                {
                    //前に進む
                    transform.Translate(speed * Vector3.forward * Time.deltaTime);
                }
                else
                {
                    //カメラの方向ベクトルと入力値からの移動方向ベクトルの角度を2倍した値を求める
                    float angle = 2 * Vector3.Angle(playerForward, moveForward);

                    //左向きの入力が入った場合角度を負にする
                    if (horizontalLeft < 0)
                    {
                        angle = -angle;
                    }

                    //方向転換をする
                    transform.Rotate(0f, angle * Time.deltaTime, 0f);
                }

                movingFlameCount++;
            }
            else
            {
                movingFlameCount = 0;
            }

            //右スティックで下方向の入力があった場合フラグを立てる
            if (verticalRight == 1)
            {
                isChargeJumpPower = true;
            }

            //下方向の入力後、右スティックの入力方向を4方向に振り分ける
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
                    //スティックが離されたら向き関連を初期化する
                    rightStickDirection = Direction.None;
                    rightStickRotateDirection = Direction.None;

                    //右スティックが離されたらフラグを消す
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
            //ジャンプパワーが最大値以下の時のみパワーをためる
            if (jumpPower < jumpPowerMax)
            {
                //回転方向が決まってない場合
                if (rightStickRotateDirection == Direction.None)
                {
                    //回転方向を取得する
                    if (rightStickDirection == Direction.Right)
                    {
                        rightStickRotateDirection = Direction.Left;
                    }
                    else if (rightStickDirection == Direction.Left)
                    {
                        rightStickRotateDirection = Direction.Right;
                    }
                }
                //右回転の時
                else if (rightStickRotateDirection == Direction.Right)
                {
                    //回転の向きが一致していた場合パワーをためる
                    if ((int)rightStickDirection == (int)rightStickDirectionOld % 4 + 1)
                    {
                        jumpPower++;
                    }
                }
                //左回転の時
                else
                {
                    //回転の向きが一致していた場合パワーをためる
                    if ((int)rightStickDirection % 4 == (int)rightStickDirectionOld - 1)
                    {
                        jumpPower++;
                    }
                }
            }

            //1フレーム前の向きの情報を保存する
            rightStickDirectionOld = rightStickDirection;
            //particle.GetComponent<ParticleSystem>().startLifetime = jumpPower * 0.5f;
            particle.GetComponent<ParticleSystem>().startSize = jumpPower * 0.5f;
        }
        else
        {
            //ジャンプするパワーを与える(rgに格納しました)
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