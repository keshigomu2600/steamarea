using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRand : MonoBehaviour
{
    public const float navCount = 5.0f;
    //�ړ��͈�
    public float xMax, xMin, zMax, zMin;
    //�͈͊O�ɏo�Ă��܂����Ƃ��̑Ώ�
    public float outAreaOverPoint = -11.0f;

    [Header("outArea�̌�ɓG��������")]
    public bool appearEnemy = true;

    const int valueNum = 5;

    Vector3[] points = new Vector3[valueNum];

    private float vecX;
    private float vecZ;
    private float navCountSeconds = 0.0f;

    private int destPoint = 0;
    private NavMeshAgent agent;
    private Rigidbody rb;

    //�^�񒆂ɗ����������肷��t���O
    bool dropMid = false;

    //�O���ɗ��Ƃ������𐔂���X�N���v�g���A�^�b�`�����I�u�W�F�N�g
    public GameObject enemyDropOutside;

    //�����ɗ��Ƃ������𐔂���X�N���v�g���A�^�b�`�����I�u�W�F�N�g
    public GameObject enemyDropInside;

    //���Ƃ����G�̐��𐔂���X�N���v�g���A�^�b�`�����I�u�W�F�N�g
    public GameObject allEnemuDropCondition;

    //box�̃X�N���v�g
    public GameObject EnemyBoxIn;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        agent.autoBraking = false;

        //�ړ�����ʒu�������_���ɑI��
        for (int i = 0; i < points.Length; i++) 
        {
            vecX = Random.Range(xMin, xMax);
            vecZ = Random.Range(zMin, zMax);
            points[i] = new Vector3(vecX, transform.position.y, vecZ);

            //Debug.Log(points[i]);
        }

        AgentStop();
    }


    void Update()
    {
        if (agent.enabled)
        {
            if (!agent.pathPending && agent.remainingDistance < 5.0f)
                GotoNextPoint();
        }


        //�͂����ꂽ�㎞�Ԍo�߂Ō��ɖ߂�
        if (!agent.enabled)
        {
            navCountSeconds += Time.deltaTime;

            if (navCountSeconds >= navCount)
            {
                agent.enabled = true;
                rb.isKinematic = true;
                navCountSeconds = 0.0f;
                GotoNextPoint();
            }
        }

        //Debug.Log(transform.position);
    }

    void FixedUpdate()
    {
        //�͈͊O�ɏo�Ă��܂����ꍇ�̏���
        if (transform.position.y < outAreaOverPoint)
        {
            this.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //���C�ɓ��������Ƃ��̂݃i�r�Q�[�V����off
        if (other.tag == "Player")
        {
            AgentStop();
        }

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

    void GotoNextPoint()
    {
        if (points.Length == 0)
            return;

        agent.destination = points[destPoint];

        destPoint = (destPoint + 1) % points.Length;
    }

    //�G���i�r�Q�[�V���������Ȃ�
    void AgentStop()
    {
        agent.enabled = false;
        rb.isKinematic = false;
        transform.position = transform.position + new Vector3(0.0f, 0.0f, 0.0f) * Time.deltaTime;
    }
}
