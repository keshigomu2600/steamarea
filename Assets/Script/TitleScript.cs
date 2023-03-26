using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    [Header("‚±‚±‚É“Ç‚Ýž‚Ý‚½‚¢ƒV[ƒ“–¼‚ð‘‚¢‚Ä‚­‚¾‚³‚¢")]
    public string sceneName;

    public GameObject[] button = new GameObject[3];
    public GameObject userGuide;

    bool isShowingUserGuide = false;

    int selectedNumber;

    bool rightButton;
    bool leftButton;
    bool downButton;
    bool upButton;

    bool oldRightButton;
    bool oldLeftButton;
    bool oldDownButton;
    bool oldUpButton;

    bool bButton;
    bool oldBButton;

    // Start is called before the first frame update
    void Start()
    {
        selectedNumber = 0;
        button[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Abutton"))
        {
            bButton = true;
        }
        else
        {
            bButton = false;
        }

        if (isShowingUserGuide)
        {
            if (bButton && !oldBButton)
            {
                userGuide.SetActive(false);
                isShowingUserGuide = false;
            }
        }
        else
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
                leftButton = false;
                rightButton = false;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) || (upButton && !oldUpButton) || (rightButton && !oldRightButton))
            {
                button[selectedNumber].SetActive(false);
                if (selectedNumber == 0)
                {
                    selectedNumber = button.Length - 1;
                }
                else
                {
                    selectedNumber--;
                }
                button[selectedNumber].SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) || (downButton && !oldDownButton) || (leftButton && !oldLeftButton))
            {
                button[selectedNumber].SetActive(false);
                if (selectedNumber == button.Length - 1)
                {
                    selectedNumber = 0;
                }
                else
                {
                    selectedNumber++;
                }
                button[selectedNumber].SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Space) || (bButton && !oldBButton))
            {
                switch (selectedNumber)
                {
                    case 0:
                        StartGame();
                        break;
                    case 1:
                        userGuide.SetActive(true);
                        isShowingUserGuide = true;
                        break;
                    case 2:
                        Application.Quit();
                        break;
                }
            }
        }
        oldRightButton = rightButton;
        oldLeftButton = leftButton;
        oldDownButton = downButton;
        oldUpButton = upButton;

        oldBButton = bButton;
    }

    public void StartGame()
    {  
        SceneManager.LoadScene(sceneName);
    }
}
