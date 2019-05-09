using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class star : MonoBehaviour//这个是左右横移来尝试撞你的小怪，在最最早的版本是想打流星的，所以这个名字就叫star了，不要吐槽
{
    public float xtime = 5f;
    public float turn_time = .1f;
    float speed = -3f;
    public float HP = 5;
    public controlscript control;
    public GameObject explode;
    private bool isInvincible = false;
    public float timeSpentInvincible = 0f;
    // Use this for initialization
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
        /*if (isInvincible)
        {
            //2
            timeSpentInvincible += Time.deltaTime;

            //3
            if (timeSpentInvincible < .1f)
            {
                float remainder = timeSpentInvincible % 0.3f;
                //renderer.enabled = remainder > 0.15f;
                GetComponent<Renderer>().enabled = remainder > 0.05f;
            }
            //4
            else
            {
                GetComponent<Renderer>().enabled = true;
                isInvincible = false;
            }
        }以上是用于受到攻击后模型暂时消失的闪烁效果，但是受到激光攻击会持续消失，还需要调整*/ 

        if (xtime > 0)
        {
            transform.Translate(2f * Time.deltaTime, 0f, 0f);
            xtime -= turn_time;
        }
        else if (xtime < 0)
        {
            transform.Translate(-2f * Time.deltaTime, 0f, 0f);
            xtime -= turn_time;
        }
        if (xtime < -5)
        {
            xtime = 5f;
        }
        transform.Translate(0f, (speed) * Time.deltaTime, 0f);

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {
            timeSpentInvincible = 0f;
            isInvincible = true;
            HP -= 1;
            if (HP <= 0)
            {
                Instantiate(explode, new Vector3(transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.identity);
                Destroy(this.gameObject);
                control.Explode_sound();
                control.Addscore(1);
            }
        }
        if (other.gameObject.tag == "smallbullet")
        {
            timeSpentInvincible = 0f;
            isInvincible = true;
            HP -= 0.5f;
            if (HP <= 0)
            {
                Instantiate(explode, new Vector3(transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.identity);
                Destroy(this.gameObject);
                control.Explode_sound();
                control.Addscore(1);
            }
        }
        if (other.gameObject.tag == "lazer")
        {
            timeSpentInvincible = 0f;
            isInvincible = true;
            HP -= 0.1f;
            if (HP <= 0)
            {
                Instantiate(explode, new Vector3(transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.identity);
                Destroy(this.gameObject);
                control.Explode_sound();
                control.Addscore(1);
            }
        }
        if (other.gameObject.tag == "lazer2")
        {
            timeSpentInvincible = 0f;
            isInvincible = true;
            HP -= 0.2f;
            if (HP <= 0)
            {
                Instantiate(explode, new Vector3(transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.identity);
                Destroy(this.gameObject);
                control.Explode_sound();
                control.Addscore(1);
            }
        }
        if (other.gameObject.tag == "lazer3")
        {
            timeSpentInvincible = 0f;
            isInvincible = true;
            HP -= 0.3f;
            if (HP <= 0)
            {
                Instantiate(explode, new Vector3(transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.identity);
                Destroy(this.gameObject);
                control.Explode_sound();
                control.Addscore(1);
            }
        }
    }
}

