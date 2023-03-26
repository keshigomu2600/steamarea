using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//シーン名を保存するスクリプトです。シーンごとに入れてください（ランダムシーンにはいれなくてよい）
public class SceneNameKeep : MonoBehaviour
{
    public static string NowSceneName;

    void Start()
    {
        NowSceneName = SceneManager.GetActiveScene().name;
    }
        
    public static string getSceneName()
    {
        return NowSceneName;
    }
}
