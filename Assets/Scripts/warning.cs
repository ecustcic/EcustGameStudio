using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warning : MonoBehaviour
{
    public float time2 = 55f;
    public AudioSource warn;
    private int isplayed = 0;
    // Start is called before the first frame update

    void Start()
    {
        warn = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        time2 -= Time.deltaTime;
        if (time2 <= -4f && isplayed == 0)
        {           
            warn.Play();
            isplayed = 1;
        }

    }
}
