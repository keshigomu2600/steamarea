using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//�V�[������ۑ�����X�N���v�g�ł��B�V�[�����Ƃɓ���Ă��������i�����_���V�[���ɂ͂���Ȃ��Ă悢�j
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
