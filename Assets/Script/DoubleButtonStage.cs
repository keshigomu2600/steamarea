using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleButtonStage : MonoBehaviour
{
    bool switchOn = false;
    bool wallDown = false;
    public GameObject wall;
    public Material pushedColor;
    Material normalColor;

    public DoubleButtonStage pair;
    float time = 0f;

    Vector3 buttonPos;
    float objectDownPos;
    // Start is called before the first frame update
    void Start()
    {
        buttonPos = gameObject.transform.position;
        objectDownPos = wall.transform.position.y - 30f;
        normalColor = gameObject.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (switchOn)
        {
            gameObject.GetComponent<Renderer>().material = pushedColor;
            if (pair == null)
                wallDown = true;

            if (time <= 5)
            {
                time += Time.deltaTime;
                if (time < 0.08f)
                    gameObject.transform.Translate(Vector3.back * 0.1f);
            }
            else
            {
                gameObject.GetComponent<Renderer>().material = normalColor;
                time = 0;
                switchOn = false;
                if (!wallDown)
                    gameObject.transform.position = buttonPos;
            }

            if (pair != null)
            {
                if (pair.switchOn)
                {
                    wallDown = true;
                }
            }
        }

        if (wallDown)
        {
            if (wall.transform.position.y > objectDownPos)
                wall.transform.Translate(Vector3.down * 0.02f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            switchOn = true;
        }
    }
}
