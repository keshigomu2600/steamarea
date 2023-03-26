using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BrackOut : MonoBehaviour
{
    public RoundCount roundCount;
    public GameObject brackOut;

    [Header("‚±‚±‚ÉŠY“–‚·‚éƒV[ƒ“–¼‚ğ‘‚¢‚Ä‚­‚¾‚³‚¢")]
    public string sceneName = "ResultScene";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (roundCount.count == roundCount.roundCount + 1)
        {
            brackOut.SetActive(true);
            SceneManager.LoadScene(sceneName);
        }
    }
}
