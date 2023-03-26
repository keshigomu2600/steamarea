using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sec8 : MonoBehaviour
{
    public GameObject[] gameobject = new GameObject[6];
    float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time > 8)
            for(int i = 0;i<gameobject.Length;i++)
            {
                gameobject[i].SetActive(true);
            }   
    }
}
