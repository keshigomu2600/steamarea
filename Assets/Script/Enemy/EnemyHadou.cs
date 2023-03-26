using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHadou : MonoBehaviour
{
    //�^�[�Q�b�g�I�u�W�F�N�g�� Transform�R���|�[�l���g���i�[����ϐ�
    public Transform target;
    // �I�u�W�F�N�g�̈ړ����x���i�[����ϐ�
    public float moveSpeed;
    // �I�u�W�F�N�g����~����^�[�Q�b�g�I�u�W�F�N�g�Ƃ̋������i�[����ϐ�
    public float stopDistance;
    // �I�u�W�F�N�g���^�[�Q�b�g����ړ����J�n���鋗�����i�[����ϐ�
    public float moveDistance;
    //���ɉ����邩�O�֐i�ނ�
    public bool backEnemy = false;

    //public GameObject target;
    public GameObject shotBall;
    public float speed = 1.0f;

    [Header("�U���̃X�p��")]
    public float shotBallCoolTime = 5f;
    [Header("outArea�̌�ɓG��������")]
    public bool appearEnemy = true;

    Rigidbody rb;
    Vector3 targetPos;

    bool flag = false;
    GameObject ballObj;
    Ray ray;
    RaycastHit hit;

    //�^�񒆂ɗ����������肷��t���O
    bool dropMid = false;

    //�O���ɗ��Ƃ������𐔂���X�N���v�g���A�^�b�`�����I�u�W�F�N�g
    public GameObject enemyDropOutside;

    //�O���ɗ��Ƃ������𐔂���X�N���v�g���A�^�b�`�����I�u�W�F�N�g
    public GameObject enemyDropInside;

    //���Ƃ����G�̐��𐔂���X�N���v�g���A�^�b�`�����I�u�W�F�N�g
    public GameObject allEnemuDropCondition;

    public GameObject EnemyBoxIn;

    public Animator anim;
    float browedAwayPlayTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //������΂���Ă�A�j���[�V�������Đ�����鎞�Ԃ����߂�
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("BrowedAway"))
            browedAwayPlayTime += Time.deltaTime;
        else
            browedAwayPlayTime = 0;

        if (browedAwayPlayTime > 3f)
            anim.Play("Base Layer.None");
        //pos = transform.position;

        //// �i�|�C���g�j�}�C�i�X�������邱�Ƃŋt�����Ɉړ�����B
        //transform.Translate(transform.right * Time.deltaTime * 3 * direction);

        ////������[�ɍs������������悤�ɂ���
        //if (pos.x > -2)
        //{
        //    direction = -1;
        //}
        //if (pos.x < -11)
        //{
        //    direction = 1;
        //}

        float distance = Dis();

        // �I�u�W�F�N�g��ϐ� target �̍��W�����Ɍ�������
        transform.LookAt(targetPos);

        //������΂��ꂽ��͏����̎��ԓ����Ȃ�
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("BrowedAway"))
        {
            //�f�o�b�O
            //Debug.Log(distance);

            // �I�u�W�F�N�g�ƃ^�[�Q�b�g�I�u�W�F�N�g�̋�������
            // �ϐ� distance�i�^�[�Q�b�g�I�u�W�F�N�g�ƃI�u�W�F�N�g�̋����j���ϐ� moveDistance �̒l��菬�������
            // ����ɕϐ� distance ���ϐ� stopDistance �̒l�����傫���ꍇ

            if (distance < moveDistance && distance > stopDistance)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
                    anim.Play("Base Layer.None");
                transform.position = transform.position + new Vector3(0.0f, 0.0f, 0.0f) * Time.deltaTime;
            }
            else
            {
                if (backEnemy)
                {
                    // �ϐ� moveSpeed ����Z�������x�ŃI�u�W�F�N�g���������Ɉړ�����
                    transform.position = transform.position + (-transform.forward) * moveSpeed * Time.deltaTime;
                }
                else
                {
                    // �ϐ� moveSpeed ����Z�������x�ŃI�u�W�F�N�g��O�����Ɉړ�����
                    transform.position = transform.position + transform.forward * moveSpeed * Time.deltaTime;
                }
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("None"))
                    anim.Play("Base Layer.Walk");
            }
        }
    }

    void FixedUpdate()
    {
        float distance = Dis();

        // ������΂��ꂽ��͏����̎��ԓ����Ȃ�
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("BrowedAway"))
        {
            if (distance < moveDistance && distance > stopDistance)
            {
                ray = new Ray(transform.position, transform.forward);

                //����������v���C���[�Ɍ������ċ���ł�
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.collider.tag == "Player")
                    {
                        if (!flag)
                        {
                            ballObj = Instantiate(shotBall, transform.position, Quaternion.identity);
                            Rigidbody bRb = ballObj.GetComponent<Rigidbody>();
                            bRb.AddForce(transform.forward * speed);
                            FlagChange();
                            Destroy(ballObj, shotBallCoolTime);
                        }

                        if (!ballObj)
                        {
                            FlagChange();
                        }
                    }
                }
            }

            //�f�o�b�O
            //Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
        }
    }

    //�q�b�g��������3�b�~�܂�
    //void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        playerJumpSimple.enemyHit = true;
    //    }
    //}

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.position = transform.position + new Vector3(0.0f, 0.0f, 0.0f) * Time.deltaTime;
        }
    }

    float Dis()
    {
        // �ϐ� targetPos ���쐬���ă^�[�Q�b�g�I�u�W�F�N�g�̍��W���i�[
        targetPos = target.position;
        targetPos.y = transform.position.y;

        // �ϐ� distance ���쐬���ăI�u�W�F�N�g�̈ʒu�ƃ^�[�Q�b�g�I�u�W�F�N�g�̋������i�[
        return Vector3.Distance(transform.position, target.position);
    }

    public void FlagChange()
    {
        if (!flag)
        {
            flag = true;
        }
        else
        {
            flag = false;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        //outArea�ɓ����������
        if (other.tag == "outArea")
        {
            //�G��S�����Ƃ����E���h�̗������G���J�E���g���邽�߂̑N�x���b�Z�[�W��ǉ����܂���
            allEnemuDropCondition.SendMessage("DropEnemyCount", SendMessageOptions.DontRequireReceiver);
            EnemyBoxIn.SendMessage("DropEnemyCount", SendMessageOptions.DontRequireReceiver);

            if (!dropMid)
            {
                enemyDropOutside.SendMessage("DropEnemyCount", SendMessageOptions.DontRequireReceiver);
                enemyDropInside.SendMessage("RoundFailed", SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                enemyDropOutside.SendMessage("RoundFailed", SendMessageOptions.DontRequireReceiver);
                enemyDropInside.SendMessage("DropEnemyCount", SendMessageOptions.DontRequireReceiver);
            }

            if (appearEnemy)
            {
                this.gameObject.SetActive(false);
            }
        }

        //�����ɗ��������ǂ������m�F���邽�߂̔����ǉ����܂���
        if (other.tag == "BlueZone")
        {
            dropMid = true;
        }
    }
    void PlayBrowedAwayAnimation()
    {
        anim.Play("Base Layer.BrowedAway");
        browedAwayPlayTime = 0f;
    }
}