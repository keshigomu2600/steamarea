using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelect : MonoBehaviour
{
    //ほぼResult依存です

   

    [Header("ステージ(何入れてもいいです)")]
    public GameObject[] stage;
    public TimeCount timeCount;

    //ステージ選択用の要素数
    int value = 0;
    [HideInInspector]
    public int valueOld = 0;

    // Update is called once per frame
    void Update()
    {

    }

    public void RandStage()
    {
        //ランダムでステージ選択
        value = Random.Range(0, stage.Length);

        stage[value].SetActive(true);
        valueOld = value;

    }
}
