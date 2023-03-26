using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BrackOutGameOverPlus : MonoBehaviour
{
    public ResultNotRand resultNotRand;
    public RoundCountNotRand roundCount;
    public GameObject brackOut;

    [Header("�����ɊY������V�[�����������Ă�������")]
    public string sceneName = "ResultScene";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SceneChange()
    {
        //�������s������GAMEOVER�V�[���̕��Ɉړ����������Ǝv���܂�
        if (roundCount.count == roundCount.roundCount + 1)
        {
            brackOut.SetActive(true);
            if (resultNotRand.gameoverFlag)
            {
                SceneManager.LoadScene("Gameover");
            }
            else
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}
