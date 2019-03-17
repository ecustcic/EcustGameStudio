using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class gamecontrol : MonoBehaviour
{
    public float uptime = 1f;
    AudioSource audioSource;
    public controlscript control;
    public float speed = 10f;
    public sheild sheild;
    public GameObject bullet;
    public GameObject smallbullet;
    public GameObject power_blue;
    public float bulletThreshold = .5f;
    float elapsedTime = 0;
    public int power = 1;
    public int power_kind = 1;
    // Use this for initialization
    void Start()
    {
        uptime = 1f;
        audioSource = gameObject.GetComponent<AudioSource>();
        sheild = GameObject.Find("sheild").GetComponent<sheild>();
        //从这里得到sheild，不然不能使用来自sheild的time来设置护盾时间
    }

    // Update is called once per frame
    void Update()
    {
        uptime -= Time.deltaTime;
        if(uptime>0)
        {
            transform.Translate(0f, -2f * Time.deltaTime, 0f);
        }
        else { uptime = -1f; }
        elapsedTime += Time.deltaTime;
        //transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime * -1, 0f, 0f);
        //if (Input.GetKey(KeyCode.A))
        //    rb.velocity = new Vector3(-speed, 0f, 0f);
        //if (Input.GetKey(KeyCode.D))
        //    rb.velocity = new Vector3(speed, 0f, 0f);
        /*if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            rb.velocity = new Vector3(0f, 0f, 0f);
        }*/
        //transform.Translate(0f, Input.GetAxis("Vertical") * speed * Time.deltaTime * -1, 0f);
        if (Input.GetButton("Jump"))
        {
            if (elapsedTime > bulletThreshold)
            {
                if (audioSource == null)
                {
                    audioSource = gameObject.AddComponent<AudioSource>();
                }
                audioSource.loop = true; //是否循环播放
                audioSource.Play();
                if (power == 1 && power_kind == 1)
                {
                    Instantiate(smallbullet, new Vector3(transform.position.x - .5f, transform.position.y + .2f, -2f), Quaternion.identity);
                    Instantiate(smallbullet, new Vector3(transform.position.x + .5f, transform.position.y + .2f, -2f), Quaternion.identity);
                    elapsedTime = 0f;
                }
                if (power == 2 && power_kind == 1)
                {
                    Instantiate(smallbullet, new Vector3(transform.position.x - .5f, transform.position.y - .3f, -2f), Quaternion.identity);
                    Instantiate(smallbullet, new Vector3(transform.position.x + .5f, transform.position.y - .3f, -2f), Quaternion.identity);
                    Instantiate(smallbullet, new Vector3(transform.position.x, transform.position.y + .2f, -2f), Quaternion.identity);
                    elapsedTime = 0f;
                }
                if (power == 3 && power_kind == 1)
                {
                    Instantiate(bullet, new Vector3(transform.position.x, transform.position.y + 1.2f, -2f), Quaternion.identity);
                    Instantiate(smallbullet, new Vector3(transform.position.x - .3f, transform.position.y + .2f, -2f), Quaternion.Euler(0f, 0f, 5f));
                    Instantiate(smallbullet, new Vector3(transform.position.x + .3f, transform.position.y + .2f, -2f), Quaternion.Euler(0f, 0f, -5f));
                    elapsedTime = 0f;
                }
                if (power >= 4 && power_kind == 1)
                {
                    Instantiate(bullet, new Vector3(transform.position.x, transform.position.y + 1.2f, -2f), Quaternion.identity);
                    Instantiate(smallbullet, new Vector3(transform.position.x - .3f, transform.position.y + .2f, -2f), Quaternion.Euler(0f, 0f, 5f));
                    Instantiate(smallbullet, new Vector3(transform.position.x + .3f, transform.position.y + .2f, -2f), Quaternion.Euler(0f, 0f, -5f));
                    Instantiate(smallbullet, new Vector3(transform.position.x - .5f, transform.position.y, -2f), Quaternion.Euler(0f, 0f, 20f));
                    Instantiate(smallbullet, new Vector3(transform.position.x + .5f, transform.position.y, -2f), Quaternion.Euler(0f, 0f, -20f));
                    elapsedTime = 0f;
                }
            }
        }
        if (Input.GetButtonUp("Jump"))
        {
            audioSource.loop = false;
        }
        if (control.healthpoint <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal,moveVertical;
        if (Input.GetKey(KeyCode.A))
            moveHorizontal = -1;
        else
        if (Input.GetKey(KeyCode.D))
            moveHorizontal = 1;
        else moveHorizontal = 0;
        if (Input.GetKey(KeyCode.W) && uptime < 0)
            moveVertical = 1;
        else
        if (Input.GetKey(KeyCode.S) && uptime < 0)
            moveVertical = -1;
        else moveVertical = 0;
        Vector3 direction = new Vector3(moveHorizontal, moveVertical, 0);
        transform.position += direction.normalized * speed * Time.deltaTime;

    }


    private void OnTriggerEnter(Collider other)//碰撞触发等等
    {
        if (other.gameObject.tag == "enemy")
        {
            if (power >= 2)
            { power -= 1; }
            Destroy(other.gameObject);
            control.LowerHP();
            sheild.time = 3f;
            /*Vector3 pos = transform.position;
            Instantiate(power_blue, new Vector3(pos.x, pos.z, -2), Quaternion.Euler(0f,0f,Random.Range(0,360))); if (power >= 3)
            {
                Instantiate(power_blue, new Vector3(pos.x, pos.z, -2), Quaternion.Euler(0f, 0f, 0f));
                Instantiate(power_blue, new Vector3(pos.x, pos.z, -2), Quaternion.Euler(0f, 0f, 0f));
            }//如果死亡时拥有3点以上的power，那么补偿2个武器等级，不然补偿1个
            else
            {
                Instantiate(power_blue, new Vector3(pos.x, pos.z, -2), Quaternion.Euler(0f, 0f, 0f));
            }*/
            control.Explode_sound();
            //Destroy(this.gameObject);
            //if (control.healthpoint > 0)
            //{ control.Reborn(); }                    
        }
        else if (other.gameObject.tag == "enemybullet")
        {
            if (power >= 2)
            { power -= 1; }
            Destroy(other.gameObject);
            control.LowerHP();
            sheild.time = 3f;
            /*Vector3 pos = transform.position;
            Instantiate(power_blue, new Vector3(pos.x, pos.y, -2f), Quaternion.identity);
            Destroy(this.gameObject);
            if (control.healthpoint > 0)
            { control.Reborn(); }*/
        }
        else if (other.gameObject.tag == "death")
        {
            control.LowerHP();
            /*Destroy(this.gameObject);
            if (control.healthpoint > 0)*/
            { control.PlayerDied(); }
        }
        else if (other.gameObject.tag == "power_blue")
        {
            Destroy(other.gameObject);
            if (power < 4 && power_kind == 1)
            {
                power += 1;
            }
            else
            {
                control.IncreaseHP();
                power_kind = 1;
            }
        }
        
    }
   
}
