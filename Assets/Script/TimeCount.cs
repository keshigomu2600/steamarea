using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCount : MonoBehaviour
{
    //���Ԑ���
    public float MaxCount = 60.0f;
    public Text time;
    [SerializeField]
    public bool activeFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        time.text = " ";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(activeFlag)
        {
            MaxCount -= Time.deltaTime;
            if (MaxCount <= 0.0f)
            {
                time.text = " ";
                MaxCount = 0.0f;
                activeFlag = false;
                //���Ԑ؂�ɂȂ�����RoundCount�Ŏ��̃J�E���g�Ɉڂ�
            }
            else
            {
                time.text = "" + ((int)MaxCount + 1);
            }
        }
    }
}
