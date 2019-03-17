using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMcontrol : MonoBehaviour
{
    public float time1 = 55f;
    public float time2 = 2f;
    public AudioClip bgm;
    public AudioClip alarm;
    public AudioClip bossfight;
    public AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        bgm = gameObject.GetComponent<AudioClip>();
        alarm = gameObject.GetComponent<AudioClip>();
        bossfight = gameObject.GetComponent<AudioClip>();

        gameObject.GetComponent<AudioSource>().clip = bgm;
    }

    // Update is called once per frame
    private void Update()
    {
        time1 -= Time.deltaTime;
        if(time1<0)
        {
            time2 -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {       
        if (time1 >= 0)
        {
            gameObject.GetComponent<AudioSource>().Play();
        }
        else if(time2 >= 0)
        {
            gameObject.GetComponent<AudioSource>().clip = alarm;
            gameObject.GetComponent<AudioSource>().Play();
        }
        else
        {
            gameObject.GetComponent<AudioSource>().clip = bossfight;
            gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
