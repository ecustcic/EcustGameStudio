using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class gamecontrol : MonoBehaviour//这是控制飞机本体的脚本
{
    public float uptime = 1f;//刚出现的时候会自动向上飞行一小段距离
    public int bome = 3;
    AudioSource audioSource;
    public controlscript control;//连接总控制脚本
    public float speed = 10f;
    public sheild sheild;//护盾，子弹，激光等等
    public GameObject bullet;
    public GameObject lazer;
    public GameObject lazer2;
    public GameObject lazer3;
    public GameObject smallbullet;
    public GameObject power_blue;
    public GameObject Bomb;
    private float bombcd = 1f;
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

        if (bombcd >= 0)//炸弹的使用有1秒的间隔
            bombcd -= Time.deltaTime;
        uptime -= Time.deltaTime;
        if(uptime>0)//开场自动向上飞行
        {
            transform.Translate(0f, -2f * Time.deltaTime, 0f);
        }
        else { uptime = -1f; }
        elapsedTime += Time.deltaTime;//注释内容为旧的移动写法，会有惯性，所以取消了
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
        if (Input.GetButton("Jump"))//攻击指令，空格攻击，按下shift的行动方式会切换到powerkind2模式
            //根据power等级发射不同的攻击，子弹仅仅是直线飞行脚本，敌人根据碰到的子弹tag而受伤，敌人死亡判定在敌人的脚本上
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
                    Instantiate(smallbullet, new Vector3(transform.position.x - .3f, transform.position.y + .2f, -2f), Quaternion.Euler(0f, 0f, 10f));
                    Instantiate(smallbullet, new Vector3(transform.position.x + .3f, transform.position.y + .2f, -2f), Quaternion.Euler(0f, 0f, -10f));
                    elapsedTime = 0f;
                }
                if (power >= 4 && power_kind == 1)
                {
                    Instantiate(bullet, new Vector3(transform.position.x, transform.position.y + 1.2f, -2f), Quaternion.identity);
                    Instantiate(smallbullet, new Vector3(transform.position.x - .3f, transform.position.y + .2f, -2f), Quaternion.Euler(0f, 0f, 10f));
                    Instantiate(smallbullet, new Vector3(transform.position.x + .3f, transform.position.y + .2f, -2f), Quaternion.Euler(0f, 0f, -10f));
                    Instantiate(smallbullet, new Vector3(transform.position.x - .5f, transform.position.y, -2f), Quaternion.Euler(0f, 0f, 25f));
                    Instantiate(smallbullet, new Vector3(transform.position.x + .5f, transform.position.y, -2f), Quaternion.Euler(0f, 0f, -25f));
                    elapsedTime = 0f;
                }
                if (power == 1 && power_kind == 2)
                {
                    Instantiate(lazer, new Vector3(transform.position.x, transform.position.y + .2f, -2f), Quaternion.identity);
                    elapsedTime = .18f;
                }
                if (power == 2 && power_kind == 2)
                {
                    Instantiate(lazer2, new Vector3(transform.position.x, transform.position.y + .2f, -2f), Quaternion.identity);
                    elapsedTime = .185f;
                }
                if (power == 3 && power_kind == 2)
                {
                    Instantiate(lazer2, new Vector3(transform.position.x, transform.position.y + .2f, -2f), Quaternion.identity);
                    elapsedTime = .19f;
                }
                if (power >= 4 && power_kind == 2)
                {
                    Instantiate(lazer3, new Vector3(transform.position.x, transform.position.y + .2f, -2f), Quaternion.identity);
                    elapsedTime = .185f;
                }
            }
        }
        if (Input.GetButtonUp("Jump"))//抬起空格会停止攻击音效
        {
            audioSource.loop = false;
        }
        if (control.healthpoint <= 0)//加入总控制判定没血了，那么死
        {
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {//这是现在的移动脚本，通过单位向量和速度来移动，没有惯性，但是用小键盘移动可能会有问题
        float moveHorizontal,moveVertical;
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && transform.position.x >= -8.5)
            moveHorizontal = -1;
        else
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && transform.position.x <= 8.5)
            moveHorizontal = 1;
        else moveHorizontal = 0;
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && uptime < 0 && transform.position.y <= 7)
            moveVertical = 1;
        else
        if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && uptime < 0 && transform.position.y >= -6)
            moveVertical = -1;
        else moveVertical = 0;
        Vector3 direction = new Vector3(moveHorizontal, moveVertical, 0);
        if (Input.GetKey(KeyCode.LeftShift))
        { transform.position += direction.normalized * 0.5f * speed * Time.deltaTime; power_kind = 2; }
        else
        { transform.position += direction.normalized * speed * Time.deltaTime; power_kind = 1; }
        if (Input.GetKeyDown(KeyCode.B) && bome > 0 && bombcd <= 0)//允许放B的条件
        {
            bome -= 1;
            control.Explode_sound();
            Instantiate(Bomb, new Vector3(transform.position.x, transform.position.y, -2f), Quaternion.identity);
            bombcd = 1f;
        }
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
            /*Vector3 pos = transform.position;这一段代码会出现问题，去掉了，
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
        else if (other.gameObject.tag == "power_blue")//这个是吃到加能量的道具，增加威力，如果威力已经满了，那么加一滴血，可以突破5
        {
            Destroy(other.gameObject);
            if (power < 4)
            {
                power += 1;
            }
            else
            {
                control.IncreaseHP();
                //power_kind = 1;
            }
        }
        
    }
   
}
