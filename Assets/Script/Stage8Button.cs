using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage8Button : MonoBehaviour
{
    bool switchOn = false;
    public GameObject floor;
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(switchOn)
        {
            if(gameObject.transform.position.y > 40.1f)
                gameObject.transform.Translate(Vector3.down * 0.1f);

            if(floor.transform.position.y > 30)
                floor.transform.Translate(Vector3.down * 0.02f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            switchOn = true;
            enemy.SetActive(true);
        }
    }

}

