using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelPosReset : MonoBehaviour
{
    Vector3 colPos;
    public GameObject col;
    // Start is called before the first frame update
    void Start()
    {
        colPos = gameObject.transform.position - col.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        gameObject.transform.position = col.transform.position + colPos;
    }
}
