using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Out : MonoBehaviour
{
    //�n�ʂɗ������Ƃ��̏����ł��B�J�������[�N�͂܂��o���Ă��Ȃ��̂ł���
    //�v���C���[���n�ʂɂ��Ă���Ƃ��������X�|�[���n�_�𓮂����ā��������痎����O(z��-10.0f)�|�C���g���L���A
    //������������s�����炻���̒n�_�ɍ��W�ړ����Ă܂��B�����Ƃ��Ă͂ǂ�ǂ���ɉ������Ă�������
    public GameObject target;
    public GameObject point;

    //�E�F�C�g����(���������Time.deltatime�g��������������)
    public int flameC;

    //���o�p
    bool waitMode = false;
    public bool outArea = false;
    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(outArea)
        {
            //���o
            count++;
            if(count > flameC)
            {
                //���o���I�������t���O�𗧂Ă�
                waitMode = true;
                count = 0;
            }
        }

        if (waitMode)
        {
            //���X�|�[���|�C���g�Ɉړ�
            target.transform.position = point.transform.position;
            outArea = false;
            waitMode = false;
        }
    }

    //�����鏈��
    private void OnTriggerEnter(Collider collider)
    {
        //�v���b�g�t�H�[���ƏՓ˔��肪����ꍇ
        if (collider.gameObject.tag == "Player")
        {
            outArea = true;
        }
    }
}
