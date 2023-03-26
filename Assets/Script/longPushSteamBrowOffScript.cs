using UnityEngine;
using UnityEngine.UI;

public class longPushSteamBrowOffScript : MonoBehaviour
{
    public AudioSource steamsound;


    public ParticleSystem steamParticle;
    public GameObject screw;
    ParticleSystem.Particle particle = new ParticleSystem.Particle();
    float power = 0f;
    float size;

    public Image steamMeter;

    const float steamAreaMin = 2f;
    const float steamAreaMid = 4f;
    const float steamAreaMax = 7f;

    public float defaultSize;

    Vector3 scale;
    bool isCharging = false;

    int emitValue = 0;

    // Start is called before the first frame update
    void Start()
    {
        scale = new Vector3(defaultSize, 0.1f, defaultSize);
        steamMeter.GetComponent<Image>().fillAmount = 0f;

        steamsound = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {


    }

    private void FixedUpdate()
    {
        if (Input.GetButton("Bbutton"))
        {
            power++;
            if (power <= 60)
                screw.transform.Rotate(Vector3.right * -400 * Time.deltaTime);
            //print(power);
            isCharging = false;

        }
        else
        {
            isCharging = true;
        }

        if (isCharging)
        {
            //print("hit");
            if (power == 0)
            {

            }
            else if (power <= 30)
            {
                particle.startLifetime = 0.1f;

                steamParticle.startLifetime = 0.1f;
                emitValue = 50;
                size = steamAreaMin;


                steamsound.pitch = 1;
                steamsound.volume = 0.1f;
                steamsound.Play();




            }
            else if (power <= 60)
            {
                steamParticle.startLifetime = 0.2f;
                emitValue = 100;
                size = steamAreaMid;

                steamsound.pitch = 0.7f;
                steamsound.volume = 0.2f;
                steamsound.Play();


            }
            else
            {
                steamParticle.startLifetime = 0.4f;
                emitValue = 2000;
                size = steamAreaMax;


                steamsound.pitch = 0.5f;
                steamsound.volume = 0.3f;
                steamsound.Play();


            }




        }
        if (scale.x >= size + defaultSize)
        {
            isCharging = false;
            size = 0f;
            scale = new Vector3(defaultSize, 0.1f, defaultSize);
            gameObject.transform.localScale = scale;
        }
        else
        {
            var ep = new ParticleSystem.EmitParams();
            steamParticle.Emit(ep, emitValue);

            scale += new Vector3(0.5f, 0, 0.5f);
            gameObject.transform.localScale = scale;
            power = 0f;
        }

        steamMeter.GetComponent<Image>().fillAmount = power / 61f;
    }

    private void OnTriggerEnter(Collider other)
    {
        //êÅÇ´îÚÇŒÇµ
        if (other.tag == "Enemy")
        {

            var rb = other.GetComponent<Rigidbody>();

            Vector3 vector = other.transform.position - gameObject.transform.position;
            Vector3 steamPower = vector.normalized * 10;

            switch (size)
            {
                case steamAreaMin:
                    steamPower *= 1f;
                    break;
                case steamAreaMid:
                    steamPower *= 1.6f;
                    break;
                case steamAreaMax:
                    steamPower = steamPower * 2 + Vector3.up * 5;
                    break;
            }
            rb.AddForce(steamPower, ForceMode.Impulse);

            other.SendMessage("PlayBrowedAwayAnimation");
        }
    }
}
