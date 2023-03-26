using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundSpawnScript : MonoBehaviour
{
    public GameObject RoundSpawnPoint;
    public TimeCount timeCount;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = RoundSpawnPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        gameObject.transform.position = RoundSpawnPoint.transform.position;
        gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "OutArea")
        {
            timeCount.MaxCount = 0f;
        }
    }
}
