using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointAdd : MonoBehaviour
{
    //得点
    public int point = 0;
    public Text score;

    //現在の得点
    int scorePoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            scorePoint += point;
            score.text = "SCORE:" + scorePoint;
        }
    }
}
