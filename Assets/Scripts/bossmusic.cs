using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossmusic : MonoBehaviour
{
    public float time1 = 58f;
    public AudioSource bgm;
    // Start is called before the first frame update

    void Start()
    {
        bgm = gameObject.GetComponent<AudioSource>();
        bgm.Stop();
    }

    // Update is called once per frame
    private void Update()
    {   
        time1 -= Time.deltaTime;
        if (time1 <= -7f && bgm.isPlaying == false)
        {
            
            bgm.loop = true;
            bgm.Play();
        }        
    }

    public void Time_stop()
    {
        bgm.Stop();
    }
}
