using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour
{
    //PauseScriptソースコードパクりました(大陸さんが組んでくれました＋追加)
    bool pause;
    int selectedNumber;

    public GameObject gameoverScreen;
    public GameObject[] buttons = new GameObject[2];

    bool oldDownButton;
    bool downButton;

    bool oldUpButton;
    bool upButton;


    bool oldRightButton;
    bool rightButton;

    bool oldLeftButton;
    bool leftButton;

    bool oldBButton;
    bool bButton;

    // Start is called before the first frame update
    void Start()
    {
        pause = false;
        gameoverScreen.SetActive(true);
        pause = true;
        selectedNumber = 0;
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
        }

        downButton = false;
        upButton = false;
        oldDownButton = false;
        oldUpButton = false;

        rightButton = false;
        leftButton = false;
        oldRightButton = false;
        oldLeftButton = false;

        bButton = false;
        oldBButton = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("+Vertical") > 0)
        {
            upButton = true;
        }
        else if (Input.GetAxis("+Vertical") < 0)
        {
            downButton = true;
        }
        else if (Input.GetAxis("+Vertical") == 0)
        {
            downButton = false;
            upButton = false;
        }

        if (Input.GetAxis("+Horizontal") > 0)
        {
            rightButton = true;
        }
        else if (Input.GetAxis("+Horizontal") < 0)
        {
            leftButton = true;
        }
        else if (Input.GetAxis("+Horizontal") == 0)
        {
            rightButton = false;
            leftButton = false;
        }

        if (Input.GetButton("Abutton"))
        {
            bButton = true;
        }
        else
        {
            bButton = false;
        }

        if (pause)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow) || (upButton && !oldUpButton) || (rightButton && !oldRightButton))
            {
                buttons[selectedNumber].SetActive(false);
                if (selectedNumber == 0)
                {
                    selectedNumber = buttons.Length - 1;
                }
                else
                {
                    selectedNumber--;
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow) || (downButton && !oldDownButton) || (leftButton && !oldLeftButton))
            {
                buttons[selectedNumber].SetActive(false);
                if (selectedNumber == buttons.Length - 1)
                {
                    selectedNumber = 0;
                }
                else
                {
                    selectedNumber++;
                }
            }

            buttons[selectedNumber].SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space) || (bButton && !oldBButton))
            {
                switch (selectedNumber)
                {
                    case 0:
                        //前のシーンに移動
                        string ReturnSceneName = SceneNameKeep.getSceneName();
                        SceneManager.LoadScene(ReturnSceneName);
                        End();
                        break;
                    case 1:
                        SceneManager.LoadScene("Title");
                        End();
                        break;
                    default:
                        break;
                }
            }
        }
        oldBButton = bButton;
        oldDownButton = downButton;
        oldUpButton = upButton;

        oldRightButton = rightButton;
        oldLeftButton = leftButton;

        //デバッグ
        Debug.Log(Input.GetAxis("+Horizontal"));
        Debug.Log(Input.GetAxis("+Vertical"));
        Debug.Log("Lbutton = " + leftButton);
        Debug.Log("Rbutton = " + rightButton);
        Debug.Log("button = " + upButton);
        Debug.Log("button = " + downButton);
        Debug.Log("Abutton = " + bButton);
    }

    void End()
    {
        gameoverScreen.SetActive(false);
        pause = false;
    }
}

