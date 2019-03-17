using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootenemy : MonoBehaviour
{
    public float downtime = 7f;
    public float endtime = .1f;
    float speed = -3f;
    public float HP = 5;
    public controlscript control;
    public GameObject enemybullet;
    public float cooldown = .5f;
    float elapsedTime = 0f;
    float rotate_time = 30f;
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
        elapsedTime += Time.deltaTime;
        if (downtime > 0)
        {
            transform.Translate(0f, speed * Time.deltaTime, 0f);
            downtime -= endtime;
        }
        if (downtime <= 0)
        {
            if(rotate_time > 0)
            {
                transform.Rotate(0f, 0f, 5f);
                rotate_time -= endtime;
                if (elapsedTime > cooldown)
                {
                    Instantiate(enemybullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(0f,0f, rotate_time * 15));
                    elapsedTime = 0;
                }
            }
            else
            {
                transform.Translate(0f, -speed * Time.deltaTime, 0f);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {
            HP -= 1;
            if (HP <= 0)
            {
                Destroy(this.gameObject);
                control.Explode_sound();
                control.Addscore(2);
            }
        }
        if (other.gameObject.tag == "smallbullet")
        {
            HP -= 0.5f;
            if (HP <= 0)
            {
                Destroy(this.gameObject);
                control.Explode_sound();
                control.Addscore(2);
            }
        }
    }
}
