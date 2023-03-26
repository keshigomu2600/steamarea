using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kagoStageCameraScript : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject steam;
    public GameObject enemy;
    float time;

    bool isAtacked;

    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
        transform.rotation = mainCamera.transform.rotation;
        isAtacked = false;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time <= 4)
        {
            //mainCameraの位置などをコピー
            transform.position = mainCamera.transform.position;
        }
        else if (time > 4 && time < 5.5f)
        {
            transform.position += Vector3.forward * 13 * Time.deltaTime;
            if (transform.eulerAngles.x < 90f)
                transform.eulerAngles -= new Vector3(1.0f, 0.0f, 0.0f) * Time.deltaTime * 18;

            steam.SetActive(true);

            if (isAtacked == false)
            {
                enemy.GetComponent<Rigidbody>().AddForce(Vector3.forward * 15, ForceMode.Impulse);
                isAtacked = true;
            }

        }
        else if (time > 6.5f && time <= 8f)
        {
            transform.position -= Vector3.forward * 13 * Time.deltaTime;
            if (transform.eulerAngles.x < 90f)
                transform.eulerAngles += new Vector3(1.0f, 0.0f, 0.0f) * Time.deltaTime * 18;
        }
        else if (time > 8f)
        {
            mainCamera.SetActive(true);
            gameObject.SetActive(false);
            enemy.SetActive(false);
        }
    }
}
