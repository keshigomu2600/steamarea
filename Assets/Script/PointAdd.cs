using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointAdd : MonoBehaviour
{
    //���_
    public int point = 0;
    public Text score;

    //���݂̓��_
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
