using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScriptImageVer : MonoBehaviour
{
    bool pause;
    int selectedNumber;

    public GameObject pauseScreen;
    public GameObject settingScreen;
    public GameObject[] buttons = new GameObject[4];

    bool oldDownButton;
    bool downButton;

    bool oldUpButton;
    bool upButton;

    bool oldBButton;
    bool bButton;

    bool oldYButton;
    bool yButton;

    // Start is called before the first frame update
    void Start()
    {
        pause = false;
        pauseScreen.SetActive(false);
        settingScreen.SetActive(false);

        downButton = false;
        upButton = false;
        oldDownButton = false;
        oldUpButton = false;
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

        //GetButtonDown‚ª‚È‚º‚©•·‚©‚È‚¢‚Ì‚Å
        if (Input.GetButton("Ybutton"))
        {
            yButton = true;
        }
        else
        {
            yButton = false;
        }

        if (Input.GetButton("Abutton"))
        {
            bButton = true;
        }
        else
        {
            bButton = false;
        }

        if ((yButton && !oldYButton) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause)
            {
                End();
            }
            else
            {
                Time.timeScale = 0;
                pauseScreen.SetActive(true);
                pause = true;
                selectedNumber = 0;
                for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].SetActive(false);
                }
            }
        }

        if (pause)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || (upButton && !oldUpButton))
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

            if (Input.GetKeyDown(KeyCode.DownArrow) || (downButton && !oldDownButton))
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
                        Scene loadScene = SceneManager.GetActiveScene();
                        SceneManager.LoadScene(loadScene.name);
                        End();
                        break;
                    case 1:
                        settingScreen.SetActive(true);
                        break;
                    case 2:
                        SceneManager.LoadScene("Title");
                        End();
                        break;
                    default:
                        End();
                        break;
                }
            }
        }

        oldDownButton = downButton;
        oldUpButton = upButton;

        oldYButton = yButton;
        oldBButton = bButton;
    }

    void End()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        pause = false;
    }
}
