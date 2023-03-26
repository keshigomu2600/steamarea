using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandSteam : MonoBehaviour
{
    [Header("スチーム（場所）")]
    public GameObject[] steamObject;

    [Header("何秒で切り替わる？")]
    public float countTime = 3.0f;

    float count = 0.0f;
    int appearObj;

    // Start is called before the first frame update
    void Start()
    {
        SteamObjectSet();
    }

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;

        if(count >= countTime)
        {
            SteamObjectSet();

            count = 0.0f;
        }
    }

    void SteamObjectSet()
    {
        for (int i = 0; i < steamObject.Length; i++)
        {
            steamObject[i].SetActive(false);
        }

        bool steamFlag = false;
        for (int i = 0; i < steamObject.Length; i++)
        {
            appearObj = Random.Range(0, 2);
            if (appearObj != 0)
            {
                steamObject[i].SetActive(true);
                steamFlag = true;
            }
        }
        if (!steamFlag)
        {
            appearObj = Random.Range(0, steamObject.Length);
            steamObject[appearObj].SetActive(true);
        }
    }
}
