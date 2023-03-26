using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOut : MonoBehaviour
{
    [HideInInspector]
    public bool outFlag = false;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            outFlag = true;
        }
    }
}
