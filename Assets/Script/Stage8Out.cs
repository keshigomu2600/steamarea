using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage8Out : MonoBehaviour
{
    public GameObject respawnPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.transform.position = respawnPos.transform.position;
            //‚©‚©‚Á‚Ä‚¢‚é—Í‚ð0‚É‚·‚é
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}


