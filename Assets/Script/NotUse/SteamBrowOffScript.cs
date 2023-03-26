using UnityEngine;

public class SteamBrowOffScript : MonoBehaviour
{
    enum Direction
    {
        None,
        Up,
        Right,
        Down,
        Left,
    }

    public ParticleSystem steamParticle;

    float rightVertical;
    float rightHorizontal;

    Direction stickDirection = Direction.None;
    Direction stickDirectionOld;
    Direction stickRotateDirection = Direction.None;

    public float steamPower = 20f;
    public float defaultSize = 1f;
    public float maxScale = 7f;
    float size = 0f;

    Vector3 scale;

    bool sizeUpFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        size = defaultSize;
        scale = new Vector3(defaultSize, 0.1f, defaultSize);

        steamParticle = steamParticle.GetComponent<ParticleSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        rightHorizontal = Input.GetAxis("R_Horizontal");//ç∂âE
        rightVertical = Input.GetAxis("R_Vertical");//è„â∫

        stickDirectionOld = stickDirection;

        if (rightHorizontal <= 0.3f && rightVertical <= 0.3f && rightHorizontal >= -0.3f && rightVertical >= -0.3f)
        {
            stickDirection = Direction.None;
        }
        else if (rightHorizontal == 1 && rightVertical <= 0.3f && rightVertical >= -0.3f)
        {
            stickDirection = Direction.Right;
        }
        else if (rightHorizontal <= 0.3f && rightHorizontal >= -0.3f && rightVertical == 1)
        {
            stickDirection = Direction.Down;
            if (stickDirectionOld == Direction.None)
            {
                sizeUpFlag = true;
            }
        }
        else if (rightHorizontal == -1 && rightVertical <= 0.3f && rightVertical >= -0.3f)
        {
            stickDirection = Direction.Left;
        }
        else if (rightHorizontal <= 0.3f && rightHorizontal >= -0.3f && rightVertical == -1)
        {
            stickDirection = Direction.Up;
        }


        if (sizeUpFlag)
        {
            scale = gameObject.transform.localScale;

            //âÒì]ï˚å¸Ç™åàÇ‹Ç¡ÇƒÇ»Ç¢èÍçá
            if (stickRotateDirection == Direction.None)
            {
                //âÒì]ï˚å¸ÇéÊìæÇ∑ÇÈ
                if (stickDirection == Direction.Right)
                {
                    stickRotateDirection = Direction.Left;
                }
                else if (stickDirection == Direction.Left)
                {
                    stickRotateDirection = Direction.Right;
                }
            }
            //âEâÒì]ÇÃéû
            else if (stickRotateDirection == Direction.Right)
            {
                //âÒì]ÇÃå¸Ç´Ç™àÍívÇµÇƒÇ¢ÇΩèÍçáÉpÉèÅ[ÇÇΩÇﬂÇÈ
                if ((int)stickDirection == (int)stickDirectionOld % 4 + 1)
                {
                    if (size <= maxScale - defaultSize)
                    {
                        size += 0.5f;
                        print("up1");
                    }
                }
            }
            //ç∂âÒì]ÇÃéû
            else
            {
                //âÒì]ÇÃå¸Ç´Ç™àÍívÇµÇƒÇ¢ÇΩèÍçáÉpÉèÅ[ÇÇΩÇﬂÇÈ
                if ((int)stickDirection % 4 == (int)stickDirectionOld - 1)
                {
                    if (size <= maxScale - defaultSize)
                    {
                        size += 0.5f;
                        print("up1");
                    }
                }
            }

            if (stickDirection == Direction.None)
            {
                while (true)
                {
                    if (scale.x >= size + defaultSize)
                    {
                        sizeUpFlag = false;
                        size = 0f;

                        var ep = new ParticleSystem.EmitParams();
                        steamParticle.Emit(ep, 1000);

                        print("hit");
                        break;
                    }
                    
                    scale += new Vector3(0.125f, 0, 0.125f) * 0.125f * 0.125f;
                    gameObject.transform.localScale = scale;
                }
            }
        }
        else
        {
            if(scale.x > defaultSize)
            scale -= new Vector3(0.125f, 0, 0.125f) * 0.5f;
            gameObject.transform.localScale = scale;
        }

        //print(size);
    }

    private void FixedUpdate()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        //êÅÇ´îÚÇŒÇµ
        if (other.tag == "Enemy")
        {
            var rb = other.GetComponent<Rigidbody>();

            Vector3 vector = other.transform.position - gameObject.transform.position;
            rb.AddForce(vector.normalized * steamPower,ForceMode.Impulse);

            //print("hit");
        }
    }
}
