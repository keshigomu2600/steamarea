using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Directing : MonoBehaviour
{
    [Header("ここに読み込みたいシーン名を書いてください")]
    public string sceneName;

    [Header("タイムキーパー")]
    public TimeKeeper time;

    [Header("暗幕")]
    public Image blackOut;
    [Header("クリアイメージ")]
    public Image clearImage;
    [Header("クリアテキスト")]
    public Image clearText;
    [Header("ボタン")]
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
        //暗幕を透明にする
        if(time.timeKeeper <= 3.0f)
        {
            blackOut.SetOpacity(3.0f - time.timeKeeper);
            if(!finalmusic.isPlaying)
            {
                finalmusic.Play();
            }
        }

        //画像を動かす
        if(time.timeKeeper >= 4.0f && time.timeKeeper <= 9.0f)
        {
            clearImage.transform.position -= new Vector3(0.0f, 6.7f, 0.0f);
        }

        //クリアテキストを出す
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
    /// Imageの不透明度を設定する
    /// </summary>
    /// <param name="image">設定対象のImageコンポーネント(this)</param>
    /// <param name="alpha">不透明度。0=透明 1=不透明</param>
    public static void SetOpacity(this Image image, float alpha)
    {
        var c = image.color;
        image.color = new Color(c.r, c.g, c.b, alpha);
    }
}
