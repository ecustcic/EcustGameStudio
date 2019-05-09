﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMcontrol : MonoBehaviour
{
    public float time1 = 55f;
    public AudioSource bgm;
    // Start is called before the first frame update

    void Start()
    {
        bgm = gameObject.GetComponent<AudioSource>();
        bgm.Play();
    }

    // Update is called once per frame
    private void Update()
    {
        if (time1 >= 0)
        { time1 -= Time.deltaTime; }
        else
        {
            bgm.Stop();
        }
    }
}
