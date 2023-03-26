using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNotMove : MonoBehaviour
{
    //�G�Ƃ��ē����Œ���̏����������Ă܂���

    //�^�[�Q�b�g�I�u�W�F�N�g�� Transform�R���|�[�l���g���i�[����ϐ�
    public Transform target;
    //�^�񒆂ɗ����������肷��t���O
    bool dropMid = false;

    //�O���ɗ��Ƃ������𐔂���X�N���v�g���A�^�b�`�����I�u�W�F�N�g
    public GameObject enemyDropOutside;

    //����
    public GameObject enemyDropInside;

    //���Ƃ����G�̐��𐔂���X�N���v�g���A�^�b�`�����I�u�W�F�N�g
    public GameObject allEnemuDropCondition;

    public GameObject EnemyBoxIn;

    [Header("outArea�̌�ɓG��������")]
    public bool appearEnemy = true;

    public Animator anim;
    float browedAwayPlayTime;

    void Start()
    {

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

        // �ϐ� targetPos ���쐬���ă^�[�Q�b�g�I�u�W�F�N�g�̍��W���i�[
        Vector3 targetPos = target.position;
        targetPos.y = transform.position.y;

        // �ϐ� distance ���쐬���ăI�u�W�F�N�g�̈ʒu�ƃ^�[�Q�b�g�I�u�W�F�N�g�̋������i�[
        float distance = Vector3.Distance(transform.position, target.position);

        // �I�u�W�F�N�g��ϐ� target �̍��W�����Ɍ�������
        transform.LookAt(targetPos);
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.position = transform.position + new Vector3(0.0f, 0.0f, 0.0f) * Time.deltaTime;
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

