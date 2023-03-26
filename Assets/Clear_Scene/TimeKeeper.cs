using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeKeeper : MonoBehaviour
{
    [Header("ŽžŠÔ•`ŽÊ")]
    public float timeKeeper = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        timeKeeper = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeKeeper += Time.deltaTime;
    }
}
