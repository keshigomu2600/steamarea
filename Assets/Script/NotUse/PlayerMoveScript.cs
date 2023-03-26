using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    public float speed = 5f;
    public Transform groundPos;

    public LayerMask ground;

    Rigidbody rg;

    bool grounded = false;

    // Start is called before the first frame update
    void Start()
    {
        rg = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float verticalLeft = Input.GetAxis("L_Vertical");
        float horizontalLeft = Input.GetAxis("L_Horizontal");
        if (grounded)
        {
            if (Mathf.Abs(new Vector2(verticalLeft, horizontalLeft).magnitude) > 0.5f)
            {
                Vector3 playerForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
                Vector3 moveForward = playerForward * -verticalLeft + Camera.main.transform.right * horizontalLeft;

                rg.velocity = moveForward * speed;
                transform.rotation = Quaternion.LookRotation(moveForward);
            }
            else
            {
               rg.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            }
        }
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(groundPos.position, Vector3.down, 0.1f, ground))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }
}
