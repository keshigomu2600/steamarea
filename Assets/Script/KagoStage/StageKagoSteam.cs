using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageKagoSteam : MonoBehaviour
{
    [Header("ìGÇîÚÇŒÇ∑yç¿ïW")]
    public float Y = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Rigidbody enemyRigi = other.gameObject.GetComponent<Rigidbody>();

            enemyRigi.AddForce(transform.forward, ForceMode.Impulse);
            enemyRigi.AddForce(0.0f, Y, 0.0f, ForceMode.Impulse);
        }
    }
}
