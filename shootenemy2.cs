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
    public float cooldown = 3f;
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
        if (elapsedTime > cooldown)
        {
            Instantiate(enemybullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            Instantiate(enemybullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(0f, 0f, -30f));
            Instantiate(enemybullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(0f, 0f, 30f));
            elapsedTime = 0;
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
