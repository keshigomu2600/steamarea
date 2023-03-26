using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    bool pause;
    int selectedNumber;

    public GameObject pauseScreen;
    public GameObject settingScreen;
    public GameObject[] buttons = new GameObject[4];

    public GameObject[] asettingButtons = new GameObject[2];

    public GameObject player;

    //BGM調整
    public Text textBGMNumber;
    public Text textBGMNumberSetting;
    public static float BGMsound = 100.0f;
    //SE調整
    public Text textSENumber;
    public Text textSENumberSetting;
    public static float SEsound = 100.0f;

    [SerializeField]
    private AudioMixer audioMixer;
    AudioSource audioSource;

    bool playerState;

    bool oldDownButton;
    bool downButton;

    bool oldUpButton;
    bool upButton;

    //左右ボタン（音量調整）
    bool oldLeftButton;
    bool leftButton;

    bool oldRightButton;
    bool rightButton;

    //カウントして一定以上になったら連打に切り替え
    int buttonCount = 0;


    bool oldBButton;
    bool bButton;

    bool oldYButton;
    bool yButton;

    bool settingFlag = false;

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

        leftButton = false;
        rightButton = false;
        oldLeftButton = false;
        oldRightButton = false;

        textSENumber.text = "" + SEsound;
        textSENumberSetting.text = "" + SEsound;

        textBGMNumber.text = "" + BGMsound;
        textBGMNumberSetting.text = "" + BGMsound;

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    //大陸さんが組みました(～346)
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
            leftButton = false;
            rightButton = false;
        }

        if (leftButton || rightButton || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (buttonCount < 50)
            {
                buttonCount++;
            }
        }
        else
        {
            buttonCount = 0;
        }

        //GetButtonDownがなぜか聞かないので
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
                playerState = player.GetComponent<PlayerControllerFreeCameraVer>().canMove;
                player.SendMessage("CanMove", false);
                Time.timeScale = 0;
                pauseScreen.SetActive(true);
                pause = true;
                selectedNumber = 0;
                for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].SetActive(false);
                }
                for (int i = 0; i < asettingButtons.Length; i++)
                {
                    asettingButtons[i].SetActive(false);
                }
            }
        }

        if (!settingFlag)
        {
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
                            settingFlag = SettingFlagChange(settingFlag);
                            selectedNumber = 0;
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
        }
        else
        {
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
                    for (int i = 0; i < asettingButtons.Length; i++)
                    {
                        asettingButtons[i].SetActive(false);
                    }
                }
            }

            if (pause)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow) || (upButton && !oldUpButton))
                {
                    asettingButtons[selectedNumber].SetActive(false);
                    if (selectedNumber == 0)
                    {
                        selectedNumber = asettingButtons.Length - 1;
                    }
                    else
                    {
                        selectedNumber--;
                    }
                }

                if (Input.GetKeyDown(KeyCode.DownArrow) || (downButton && !oldDownButton))
                {
                    asettingButtons[selectedNumber].SetActive(false);
                    if (selectedNumber == asettingButtons.Length - 1)
                    {
                        selectedNumber = 0;
                    }
                    else
                    {
                        selectedNumber++;
                    }
                }

                asettingButtons[selectedNumber].SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space) || (bButton && !oldBButton))
                {
                    switch (selectedNumber)
                    {
                        case 0:
                            settingFlag = SettingFlagChange(settingFlag);
                            settingScreen.SetActive(false);
                            selectedNumber = 1;
                            break;
                        default:
                            break;
                    }
                }
                switch (selectedNumber)
                {
                    case 1:
                        SettingMainBGM();
                        break;
                    case 2:
                        SettingMainSE();
                        break;
                    default:
                        break;
                }
            }
        }

        oldDownButton = downButton;
        oldUpButton = upButton;

        //最初はカウントしないが長押ししたらスムーズに音量切り替えをする
        if (buttonCount < 50)
        {
            oldLeftButton = leftButton;
            oldRightButton = rightButton;
        }
        else
        {
            oldLeftButton = false;
            oldRightButton = false;
        }

        oldYButton = yButton;
        oldBButton = bButton;

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
        Time.timeScale = 1;
        player.SendMessage("CanMove", playerState);
        pauseScreen.SetActive(false);
        pause = false;
    }
    //ここまで大陸さん

    //ここから寺林
    bool SettingFlagChange(bool flag)
    {
        if (flag)
        {
            flag = false;
        }
        else
        {
            flag = true;
        }

        return flag;
    }

    void SettingMainBGM()
    {

        if (buttonCount == 50)
        {
            if (Input.GetKey(KeyCode.RightArrow) || (rightButton && !oldRightButton))
            {
                if (BGMsound < 100)
                {
                    BGMsound++;
                }
                else
                {
                    BGMsound = 100;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || (rightButton && !oldRightButton))
        {
            if (BGMsound < 100)
            {
                BGMsound++;
            }
            else
            {
                BGMsound = 100;
            }
        }

        if (buttonCount == 50)
        {
            if (Input.GetKey(KeyCode.LeftArrow) || (leftButton && !oldLeftButton))
            {
                if (BGMsound > 0)
                {
                    BGMsound--;
                }
                else
                {
                    BGMsound = 0;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || (leftButton && !oldLeftButton))
        {
            if (BGMsound > 0)
            {
                BGMsound--;
            }
            else
            {
                BGMsound = 0;
            }
        }

        audioMixer.SetFloat("BGMVol", BGMsound - 99.0f);

        textBGMNumber.text = "" + BGMsound;
        textBGMNumberSetting.text = "" + BGMsound;
    }

    void SettingMainSE()
    {

        if (buttonCount == 50)
        {
            if (Input.GetKey(KeyCode.RightArrow) || (rightButton && !oldRightButton))
            {
                if (SEsound < 100)
                {
                    SEsound++;
                }
                else
                {
                    SEsound = 100;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || (rightButton && !oldRightButton))
        {
            if (SEsound < 100)
            {
                SEsound++;
            }
            else
            {
                SEsound = 100;
            }
            GetComponent<AudioSource>().Play();
        }

        if (buttonCount == 50)
        {
            if (Input.GetKey(KeyCode.LeftArrow) || (leftButton && !oldLeftButton))
            {
                if (SEsound > 0)
                {
                    SEsound--;
                }
                else
                {
                    SEsound = 0;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || (leftButton && !oldLeftButton))
        {
            if (SEsound > 0)
            {
                SEsound--;
            }
            else
            {
                SEsound = 0;
            }
            GetComponent<AudioSource>().Play();
        }
        audioMixer.SetFloat("SEVol", SEsound - 99.0f);

        textSENumber.text = "" + SEsound;
        textSENumberSetting.text = "" + SEsound;
    }
}

