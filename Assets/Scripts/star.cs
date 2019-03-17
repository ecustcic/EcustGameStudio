using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class star : MonoBehaviour
{
    public float xtime = 5f;
    public float turn_time = .1f;
    float speed = -3f;
    public float HP = 5;
    public controlscript control;
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
            HP -= 1;
            if (HP <= 0)
            {
                
                Destroy(this.gameObject);
                control.Explode_sound();
                control.Addscore(1);
            }
        }
        if (other.gameObject.tag == "smallbullet")
        {
            HP -= 0.5f;
            if (HP <= 0)
            {
                Destroy(this.gameObject);
                control.Explode_sound();
                control.Addscore(1);
            }
        }
    }
}
