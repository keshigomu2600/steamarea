using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffManeger : MonoBehaviour
{
    //ONOFF�ؑւ���I�u�W�F�N�g
    public GameObject taget;
    public TimeCount timeCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeCount.activeFlag)
        {
            taget.SetActive(true);
        }

        if (!timeCount.activeFlag)
        {
            taget.SetActive(false);
        }
    }
}
