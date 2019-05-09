using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour//这是boss脚本，看上去很长，其实是因为7个攻击模式凑的长度
{
    public float downtime = 20f;//boss在生成之后会自动下降一段时间
    private float turn_time = 3f;//左右横移的时间
    public float endtime = .1f;
    float speed = -2f;
    public float HP = 1000;//血量
    public controlscript control;//读取的控制，子弹，召唤小怪，提示等等
    public GameObject bullet1;
    public GameObject bullet2;
    public GameObject wave_5_bullet;
    public GameObject wave_6_bullet;
    public GameObject jv;
    public GameObject minion1;
    public GameObject minion2;
    public GameObject warning;
    public bossmusic bossmusic;
    public GameObject bossexplode;
    private int wave = 1;//boss的攻击模组，从1到6循环再回到1如此往复，但是正常状态不会超过2次循环
    private float wave_time = 0;//这些是内部使用的子弹cd，特别波束等等，写得乱…………但是能用
    public float cooldown = .5f;
    float elapsedTime = .1f;
    float rotate_time = -.4f;
    // Start is called before the first frame update

    private void Awake()
    {//这是用来播放bgm的，但是其实并没有用这个吧………………是在另一个地方放的
        bossmusic = GameObject.Find("bossmusic").GetComponent<bossmusic>();
        control = GameObject.Find("Gamecontrol").GetComponent<controlscript>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (downtime > 0)//出生向下移动
        {
            transform.Translate(0f, speed * Time.deltaTime, 0f);
            downtime -= endtime;
        }
        else
        {//子弹的cd每时每刻都在计算
            elapsedTime += Time.deltaTime;
            switch (wave)
            {
                case 1:
                    //第一波，仅仅是几波简单的散射攻击而已
                    if (elapsedTime >= .8f && wave_time == 0)
                    {
                        shoot_one(transform.position.x + 2.5f);
                        wave_time = 1;
                    }
                    else if (elapsedTime >= 1.7f && wave_time == 1)
                    {
                        shoot_one(transform.position.x - 2.5f);
                        wave_time = 2;
                    }
                    else if (elapsedTime >= 2.6f && wave_time == 2)
                    { shoot_one(transform.position.x + 2.5f); wave_time = 3; }
                    else if (elapsedTime >= 3.5f && wave_time == 3)
                    { shoot_one(transform.position.x - 2.5f); wave_time = 4; }
                    else if (elapsedTime >= 4.4f && wave_time == 4)
                    { shoot_one(transform.position.x + 2.5f); wave_time = 5; }
                    else if (elapsedTime >= 4.7f && wave_time == 5)
                    {
                        Instantiate(bullet1, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z - .1f), Quaternion.Euler(0f, 0f, 40f));
                        Instantiate(bullet1, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z - .1f), Quaternion.Euler(0f, 0f, 20f));
                        Instantiate(bullet1, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z - .1f), Quaternion.Euler(0f, 0f, 0f));
                        Instantiate(bullet1, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z - .1f), Quaternion.Euler(0f, 0f, -20f));
                        Instantiate(bullet1, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z - .1f), Quaternion.Euler(0f, 0f, -40f));
                        wave = 2;
                        wave_time = 0f;
                        elapsedTime = 0;
                    }
                    break;
                case 2://第二波，3个方向的持续直线压制，配合3个方向的随机弹幕扫射
                    cooldown -= Time.deltaTime;
                    if (cooldown <= 0)
                    { Longshoot(); cooldown = .1f; }
                    wave_time += Time.deltaTime;
                    if (wave_time >= 0)
                    {
                        //turn_time -= Time.deltaTime;
                        if (wave_time <= 3)
                        {
                            transform.Translate(1f * Time.deltaTime, 0f, 0f);
                        }
                        else if (wave_time <= 9)
                        {
                            transform.Translate(-1f * Time.deltaTime, 0f, 0f);
                        }
                        else if (wave_time <= 15)
                        {
                            transform.Translate(1f * Time.deltaTime, 0f, 0f);
                        }
                        else if (wave_time <= 18)
                        {
                            transform.Translate(-1f * Time.deltaTime, 0f, 0f);
                        }
                        else { wave = 3; wave_time = 0; elapsedTime = 0; }
                    }
                    break;
                case 3://第三波，先召唤提示，再在提示位置召唤小怪，同时放出类似第一波的散弹，小怪会释放瞄准玩家初始位置的子弹
                    if (wave_time == 0f && elapsedTime >= 2f)
                    {
                        Warn(wave_time);
                        wave_time = 1f;
                    }
                    else if(wave_time == 1f && elapsedTime >= 4f)
                    {
                        shoot_one(transform.position.x);
                        Summon(wave_time);
                        wave_time = 2f;
                    }
                    else if (wave_time == 2f && elapsedTime >= 6f)
                    {
                        Warn(wave_time);
                        wave_time = 3f;
                    }
                    else if (wave_time == 3f && elapsedTime >= 8f)
                    {
                        shoot_one(transform.position.x);
                        Summon(wave_time);
                        wave_time = 4f;
                    }
                    else if (wave_time == 4f && elapsedTime >= 10f)
                    {
                        Warn(wave_time);
                        wave_time = 5f;
                    }
                    else if (wave_time == 5f && elapsedTime >= 12f)
                    {
                        shoot_one(transform.position.x);
                        Summon(wave_time);
                        wave_time = 6;
                        wave = 4;
                        elapsedTime = 0;
                        wave_time = 0f;
                    }
                    break;
                case 4://第四波，随机召唤撞你的小怪的同时，绕圈地弹幕攻击
                    Vector3 pos = transform.position;
                    if (elapsedTime >= 1f)
                    {
                        Instantiate(minion2, new Vector3(pos.x + Random.Range(-5, 5), 8f, -2), Quaternion.identity);
                        Instantiate(minion2, new Vector3(pos.x + Random.Range(-5, 5), 8f, -2), Quaternion.identity);
                        elapsedTime = 0;
                    }
                    wave_time += Time.deltaTime;
                    rotate_time += 420 * Time.deltaTime;
                    if (wave_time >= 0)
                    {
                        turn_time -= Time.deltaTime;
                        if(turn_time <=0)
                        {
                            Instantiate(bullet1, new Vector3(transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.Euler(0f, 0f, rotate_time));
                            Instantiate(bullet1, new Vector3(transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.Euler(0f, 0f, -rotate_time));
                            turn_time = .03f;
                        }
                        if (wave_time <= 3)
                        {
                            transform.Translate(1f * Time.deltaTime, 0f, 0f);
                        }
                        else if (wave_time <= 9)
                        {
                            transform.Translate(-1f * Time.deltaTime, 0f, 0f);
                        }
                        else if (wave_time <= 15)
                        {
                            transform.Translate(1f * Time.deltaTime, 0f, 0f);
                        }
                        else if (wave_time <= 18)
                        {
                            transform.Translate(-1f * Time.deltaTime, 0f, 0f);
                        }
                        else { wave = 5; wave_time = 0; elapsedTime = 0; turn_time = 3f; }
                    }
                    break;
                case 5://第五波，放出跟着bgm节奏走的爆裂自担，可以去看这个子弹单独的脚本，这里仅仅是召唤而已
                    Vector3 pos2 = transform.position;
                    if (elapsedTime >= .25f && wave_time == 0)
                    {
                        Instantiate(wave_5_bullet, new Vector3(pos2.x -5, pos2.y, -2.1f), Quaternion.identity);
                        wave_time = 1;
                    }
                    else if (elapsedTime >= .5f && wave_time == 1)
                    {
                        Instantiate(wave_5_bullet, new Vector3(pos2.x - 3, pos2.y, -2.1f), Quaternion.identity);
                        wave_time = 2;
                    }
                    else if (elapsedTime >= .75f && wave_time == 2)
                    {
                        Instantiate(wave_5_bullet, new Vector3(pos2.x, pos2.y, -2.1f), Quaternion.identity);
                        wave_time = 3;
                    }
                    else if (elapsedTime >= 1f && wave_time == 3)
                    {
                        Instantiate(wave_5_bullet, new Vector3(pos2.x + 3, pos2.y, -2.1f), Quaternion.identity);
                        wave_time = 4;
                    }
                    else if (elapsedTime >= 1.25f && wave_time == 4)
                    {
                        Instantiate(wave_5_bullet, new Vector3(pos2.x + 5, pos2.y, -2.1f), Quaternion.identity);
                        wave_time = 5;
                    }
                    else if (elapsedTime >= 2.25f && wave_time == 5)
                    {
                        Instantiate(wave_5_bullet, new Vector3(pos2.x + 5, pos2.y, -2.1f), Quaternion.identity);
                        wave_time = 6;
                    }
                    else if (elapsedTime >= 2.5f && wave_time == 6)
                    {
                        Instantiate(wave_5_bullet, new Vector3(pos2.x + 3, pos2.y, -2.1f), Quaternion.identity);
                        wave_time = 7;
                    }
                    else if (elapsedTime >= 2.75f && wave_time == 7)
                    {
                        Instantiate(wave_5_bullet, new Vector3(pos2.x, pos2.y, -2.1f), Quaternion.identity);
                        wave_time = 8;
                    }
                    else if (elapsedTime >= 3f && wave_time == 8)
                    {
                        Instantiate(wave_5_bullet, new Vector3(pos2.x - 3, pos2.y, -2.1f), Quaternion.identity);
                        wave_time = 9;
                    }
                    else if (elapsedTime >= 3.25f && wave_time == 9)
                    {
                        Instantiate(wave_5_bullet, new Vector3(pos2.x - 5, pos2.y, -2.1f), Quaternion.identity);
                        wave = 6;
                        wave_time = 0f;
                        elapsedTime = 0;
                    }
                    break;
                case 6://第六波是召唤回旋镖子弹，这个子弹自身也会绕圈释放变速回旋子弹，可以参考单独的脚本
                    Vector3 pos3 = transform.position;
                    if (elapsedTime >= 1f && wave_time == 0)
                    {
                        Instantiate(wave_6_bullet, new Vector3(pos3.x - 3, pos3.y, -2.1f), Quaternion.identity);
                        Instantiate(wave_6_bullet, new Vector3(pos3.x, pos3.y + 3, -2.1f), Quaternion.identity);
                        Instantiate(wave_6_bullet, new Vector3(pos3.x + 3, pos3.y, -2.1f), Quaternion.identity);
                        wave_time = 1;
                    }
                    else if (elapsedTime >= 7f && wave_time == 1)
                    {
                        Instantiate(wave_6_bullet, new Vector3(pos3.x - 1.5f, pos3.y - 3f, -2.1f), Quaternion.identity);
                        Instantiate(wave_6_bullet, new Vector3(pos3.x + 1.5f, pos3.y - 3f, -2.1f), Quaternion.identity);
                        wave_time = 2;
                    }
                    else if(elapsedTime >= 13f && wave_time == 2)
                    {
                        wave_time = 0;
                        elapsedTime = 0;
                        wave = 1;
                    }
                    break;
            }
        }
    }

    void shoot_one(float x)
    {
        Instantiate(bullet1, new Vector3(x, transform.position.y - 1, transform.position.z - .1f), Quaternion.Euler(0f, 0f, 35f));
        Instantiate(bullet1, new Vector3(x, transform.position.y - 1, transform.position.z - .1f), Quaternion.Euler(0f, 0f, 25f));
        Instantiate(bullet1, new Vector3(x, transform.position.y - 1, transform.position.z - .1f), Quaternion.Euler(0f, 0f, 15f));
        Instantiate(bullet1, new Vector3(x, transform.position.y - 1, transform.position.z - .1f), Quaternion.Euler(0f, 0f, 5f));
        Instantiate(bullet1, new Vector3(x, transform.position.y - 1, transform.position.z - .1f), Quaternion.Euler(0f, 0f, -5f));
        Instantiate(bullet1, new Vector3(x, transform.position.y - 1, transform.position.z - .1f), Quaternion.Euler(0f, 0f, -15f));
        Instantiate(bullet1, new Vector3(x, transform.position.y - 1, transform.position.z - .1f), Quaternion.Euler(0f, 0f, -25f));
        Instantiate(bullet1, new Vector3(x, transform.position.y - 1, transform.position.z - .1f), Quaternion.Euler(0f, 0f, -35f));
    }

    void Longshoot()
    {
        Instantiate(bullet2, new Vector3(transform.position.x - 3f, transform.position.y - 1, transform.position.z - .1f), Quaternion.identity);
        Instantiate(bullet2, new Vector3(transform.position.x + 3f, transform.position.y - 1, transform.position.z - .1f), Quaternion.identity);
        Instantiate(bullet2, new Vector3(transform.position.x - 3f, transform.position.y - 1, transform.position.z - .1f), Quaternion.Euler(0f, 0f, -45f));
        Instantiate(bullet2, new Vector3(transform.position.x + 3f, transform.position.y - 1, transform.position.z - .1f), Quaternion.Euler(0f, 0f, 45f));
        Instantiate(bullet1, new Vector3(transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.Euler(0f, 0f, Random.Range(-30f, 30f)));
        Instantiate(bullet1, new Vector3(transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.Euler(0f, 0f, Random.Range(-90f, -30f)));
        Instantiate(bullet1, new Vector3(transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.Euler(0f, 0f, Random.Range(30f, 90f)));
    }

    void Warn(float wavex)
    {
        if (wavex == 0f)
        {
            //Instantiate(warning, new Vector3(8f, 6f, -2f), Quaternion.Euler(0f, 0f, 180f));
            Instantiate(warning, new Vector3(-8f, 2f, -2f), Quaternion.Euler(0f, 0f, 0f));
            Instantiate(warning, new Vector3(8f, -2f, -2f), Quaternion.Euler(0f, 0f, 180f));
            Instantiate(warning, new Vector3(-8f, -6f, -2f), Quaternion.Euler(0f, 0f, 0f));
        }
        else if(wavex == 2f)
        {
            Instantiate(warning, new Vector3(8f, 0f, -2f), Quaternion.Euler(0f, 0f, 180f));
            Instantiate(warning, new Vector3(-8f, -2f, -2f), Quaternion.Euler(0f, 0f, 0f));
            Instantiate(warning, new Vector3(8f, -4f, -2f), Quaternion.Euler(0f, 0f, 180f));
            //Instantiate(warning, new Vector3(-8f, 2f, -2f), Quaternion.Euler(0f, 0f, 0f));
        }
        else if (wavex == 4f)
        {
            Instantiate(warning, new Vector3(1f, 13f, -2f), Quaternion.Euler(0f, 0f, -90f));
            Instantiate(warning, new Vector3(0f, 11f, -2f), Quaternion.Euler(0f, 0f, -90f));
            Instantiate(warning, new Vector3(-1f, 13f, -2f), Quaternion.Euler(0f, 0f, -90f));
            //Instantiate(warning, new Vector3(-5f, 13f, -2f), Quaternion.Euler(0f, 0f, -90f));
            //Instantiate(warning, new Vector3(5f, 13f, -2f), Quaternion.Euler(0f, 0f, -90f));

        }
    }

    void Summon(float wavex)
    {
        if (wavex == 1f)
        {
            Instantiate(minion1, new Vector3(-8f, 2f, -2f), Quaternion.Euler(0f, 0f, 90f));
            Instantiate(minion1, new Vector3(8f, -2f, -2f), Quaternion.Euler(0f, 0f, -90f));
            Instantiate(minion1, new Vector3(-8f, -6f, -2f), Quaternion.Euler(0f, 0f, 90f));
            //Instantiate(minion1, new Vector3(8f, 6f, -2f), Quaternion.Euler(0f, 0f, -90f));
        }
        else if(wavex == 3f)
        {
            Instantiate(minion1, new Vector3(8f, 0f, -2f), Quaternion.Euler(0f, 0f, -90f));
            Instantiate(minion1, new Vector3(-8f, -2f, -2f), Quaternion.Euler(0f, 0f, 90f));
            Instantiate(minion1, new Vector3(8f, -4f, -2f), Quaternion.Euler(0f, 0f, -90f));
            //Instantiate(minion1, new Vector3(-8f, 2f, -2f), Quaternion.Euler(0f, 0f, 90f));
        }
        else if (wavex == 5f)
        {
            Instantiate(minion1, new Vector3(1f, 13f, -2f), Quaternion.Euler(0f, 0f, 0f));
            Instantiate(minion1, new Vector3(0f, 11f, -2f), Quaternion.Euler(0f, 0f, 0f));
            Instantiate(minion1, new Vector3(-1f, 13f, -2f), Quaternion.Euler(0f, 0f, 0f));
            //Instantiate(minion1, new Vector3(-5f, 13f, -2f), Quaternion.Euler(0f, 0f, 0f));
            //Instantiate(minion1, new Vector3(5f, 13f, -2f), Quaternion.Euler(0f, 0f, 0f));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {
            HP -= 1;
            Destroy(other.gameObject);
            if (HP <= 0)
            {
                Instantiate(bossexplode, new Vector3(transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.identity);
                Destroy(this.gameObject);
                control.PlayerWin();
                control.Explode_sound();
                //bossmusic.Time_stop();
            }
        }
        if (other.gameObject.tag == "smallbullet")
        {
            HP -= 0.5f;
            Destroy(other.gameObject);
            if (HP <= 0)
            {
                Instantiate(bossexplode, new Vector3(transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.identity);
                Destroy(this.gameObject);
                control.PlayerWin();
                control.Explode_sound();
                //bossmusic.Time_stop();
            }
        }
        if (other.gameObject.tag == "lazer")
        {
            HP -= 0.1f;
            Destroy(other.gameObject);
            if (HP <= 0)
            {
                Instantiate(bossexplode, new Vector3(transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.identity);
                Destroy(this.gameObject);
                control.PlayerWin();
                control.Explode_sound();
            }
        }
        if (other.gameObject.tag == "lazer2")
        {
            HP -= 0.2f;
            Destroy(other.gameObject);
            if (HP <= 0)
            {
                Instantiate(bossexplode, new Vector3(transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.identity);
                Destroy(this.gameObject);
                control.PlayerWin();
                control.Explode_sound();
            }
        }
        if (other.gameObject.tag == "lazer3")
        {
            HP -= 0.3f;
            Destroy(other.gameObject);
            if (HP <= 0)
            {
                Instantiate(bossexplode, new Vector3(transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.identity);
                Destroy(this.gameObject);
                control.PlayerWin();
                control.Explode_sound();
            }
        }
    }
}
