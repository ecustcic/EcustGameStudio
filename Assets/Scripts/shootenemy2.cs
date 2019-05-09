using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootenemy2 : MonoBehaviour
{
    public float speed = -.5f;
    // Use this for initialization
    public float HP = 7;
    public controlscript control;
    public GameObject enemybullet;
    public GameObject explode;
    public float cooldown = 3f;
    private int wave = 0;
    float elapsedTime = 0f;

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
        transform.Translate(0f, (speed) * Time.deltaTime, 0f);
        elapsedTime += Time.deltaTime;
        if (elapsedTime > cooldown && wave == 0)
        {
            wave = 1;
            Instantiate(enemybullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(0f, 0f, -30f));            
        }
        else if(elapsedTime >= cooldown * 2 && wave == 1)
        {
            wave = 2;
            Instantiate(enemybullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        }
        else if (elapsedTime >= cooldown * 3 && wave == 2)
        {
            Instantiate(enemybullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(0f, 0f, 30f));
            elapsedTime = 0;
            wave = 0;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {
            HP -= 1;
            if (HP <= 0)
            {
                Instantiate(explode, new Vector3(transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.identity);
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
                Instantiate(explode, new Vector3(transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.identity);
                Destroy(this.gameObject);
                control.Explode_sound();
                control.Addscore(2);
            }
        }
        if (other.gameObject.tag == "lazer")
        {
            HP -= 0.1f;
            if (HP <= 0)
            {
                Instantiate(explode, new Vector3(transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.identity);
                Destroy(this.gameObject);
                control.Explode_sound();
                control.Addscore(2);
            }
        }
        if (other.gameObject.tag == "lazer2")
        {
            HP -= 0.2f;
            if (HP <= 0)
            {
                Instantiate(explode, new Vector3(transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.identity);
                Destroy(this.gameObject);
                control.Explode_sound();
                control.Addscore(2);
            }
        }
        if (other.gameObject.tag == "lazer3")
        {
            HP -= 0.3f;
            if (HP <= 0)
            {
                Instantiate(explode, new Vector3(transform.position.x, transform.position.y, transform.position.z - .1f), Quaternion.identity);
                Destroy(this.gameObject);
                control.Explode_sound();
                control.Addscore(2);
            }
        }
    }
}
