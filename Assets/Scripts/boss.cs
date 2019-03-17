using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour
{
    public float downtime = 20f;
    private float turn_time = 3f;
    public float endtime = .1f;
    float speed = -2f;
    public float HP = 1000;
    public controlscript control;
    public GameObject bullet1;
    public GameObject bullet2;
    public GameObject jv;
    public GameObject minion1;
    public GameObject minion2;
    public GameObject warning;
    private int wave = 1;
    private float wave_time = 0;
    public float cooldown = .5f;
    float elapsedTime = 0f;
    float rotate_time = 0f;
    // Start is called before the first frame update

    private void Awake()
    {
        control = GameObject.Find("Gamecontrol").GetComponent<controlscript>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (downtime > 0)
        {
            transform.Translate(0f, speed * Time.deltaTime, 0f);
            downtime -= endtime;
        }
        else
        {
            elapsedTime += Time.deltaTime;
            switch (wave)
            {
                case 1:

                    if (elapsedTime >= 1f && wave_time == 0)
                    {
                        shoot_one(transform.position.x + 2.5f);
                        wave_time = 1;
                    }
                    else if (elapsedTime >= 2f && wave_time == 1)
                    {
                        shoot_one(transform.position.x - 2.5f);
                        wave_time = 2;
                    }
                    else if (elapsedTime >= 3f && wave_time == 2)
                    { shoot_one(transform.position.x + 2.5f); wave_time = 3; }
                    else if (elapsedTime >= 4f && wave_time == 3)
                    { shoot_one(transform.position.x - 2.5f); wave_time = 4; }
                    else if (elapsedTime >= 5f && wave_time == 4)
                    { shoot_one(transform.position.x + 2.5f); wave_time = 5; }
                    else if (elapsedTime >= 5.5f && wave_time == 5)
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
                case 2:
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
                case 3:
                    if (wave_time == 0f && elapsedTime >= 2f)
                    {
                        Warn(wave_time);
                        wave_time = 1f;
                    }
                    else if(wave_time == 1f && elapsedTime >= 4f)
                    {
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
                        Summon(wave_time);
                        wave_time = 6;
                        wave = 4;
                        elapsedTime = 0;
                        wave_time = 0f;
                    }
                    break;
                case 4:
                    Vector3 pos = transform.position;
                    if (elapsedTime >= 1f)
                    {
                        Instantiate(minion2, new Vector3(pos.x + Random.Range(-5, 5), 8f, -2), Quaternion.identity);
                        Instantiate(minion2, new Vector3(pos.x + Random.Range(-5, 5), 8f, -2), Quaternion.identity);
                        elapsedTime = 0;
                    }
                    wave_time += Time.deltaTime;
                    rotate_time += 400 * Time.deltaTime;
                    if (wave_time >= 0)
                    {
                        turn_time -= Time.deltaTime;
                        if(turn_time <=0)
                        {
                            Instantiate(bullet1, new Vector3(transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.Euler(0f, 0f, rotate_time));
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
                        else { wave = 1; wave_time = 0; elapsedTime = 0; turn_time = 3f; }
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
        Instantiate(bullet1, new Vector3(transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.Euler(0f, 0f, Random.Range(-45f, 45f)));
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
                Destroy(this.gameObject);
                control.PlayerWin();
                control.Explode_sound();
            }
        }
        if (other.gameObject.tag == "smallbullet")
        {
            HP -= 0.5f;
            Destroy(other.gameObject);
            if (HP <= 0)
            {
                Destroy(this.gameObject);
                control.PlayerWin();
                control.Explode_sound();
            }
        }
    }
}
