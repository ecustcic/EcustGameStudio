using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Changespeed : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = -5f;
    public float speed_change = 0f;
    public float speed2 = -6f;
    public float change_time = 5f;
    // Use this for initialization

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        change_time -= Time.deltaTime;
        if (change_time > 0)
        {
            transform.Translate(0f, (speed - change_time) * Time.deltaTime, 0f);
        }
        else if(change_time <= 0)
            transform.Translate(0f, (change_time) * Time.deltaTime, 0f);
    }
}
