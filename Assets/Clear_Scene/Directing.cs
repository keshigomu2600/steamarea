using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Directing : MonoBehaviour
{
    [Header("�����ɓǂݍ��݂����V�[�����������Ă�������")]
    public string sceneName;

    [Header("�^�C���L�[�p�[")]
    public TimeKeeper time;

    [Header("�Ö�")]
    public Image blackOut;
    [Header("�N���A�C���[�W")]
    public Image clearImage;
    [Header("�N���A�e�L�X�g")]
    public Image clearText;
    [Header("�{�^��")]
    public GameObject button;

    AudioSource finalmusic;

    // Start is called before the first frame update
    void Start()
    {
        clearText.SetOpacity(0.0f);
        finalmusic=GetComponent<AudioSource>();
        Time.timeScale = 1;
    }

    void Update()
    {
        if (button.activeSelf)
        {
            if (Input.GetKey("b") || Input.GetButton("Bbutton"))
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //�Ö��𓧖��ɂ���
        if(time.timeKeeper <= 3.0f)
        {
            blackOut.SetOpacity(3.0f - time.timeKeeper);
            if(!finalmusic.isPlaying)
            {
                finalmusic.Play();
            }
        }

        //�摜�𓮂���
        if(time.timeKeeper >= 4.0f && time.timeKeeper <= 9.0f)
        {
            clearImage.transform.position -= new Vector3(0.0f, 6.7f, 0.0f);
        }

        //�N���A�e�L�X�g���o��
        if (time.timeKeeper >= 10.0f)
        {
            clearText.SetOpacity(time.timeKeeper - 9.0f);
        }

        if (time.timeKeeper >= 12.0f)
        {
            button.SetActive(true);
        }
    }
}

public static class ImageExt
{
    /// <summary>
    /// Image�̕s�����x��ݒ肷��
    /// </summary>
    /// <param name="image">�ݒ�Ώۂ�Image�R���|�[�l���g(this)</param>
    /// <param name="alpha">�s�����x�B0=���� 1=�s����</param>
    public static void SetOpacity(this Image image, float alpha)
    {
        var c = image.color;
        image.color = new Color(c.r, c.g, c.b, alpha);
    }
}
