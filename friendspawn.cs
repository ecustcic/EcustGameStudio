using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class friendspawn : MonoBehaviour
{
    public float time = 10f;
    public float spawndecrement = .2f;

    public GameObject power_blue;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            time = 30f;
            Vector3 pos = transform.position;
            Instantiate(power_blue, new Vector3(pos.x + Random.Range(-5, 5), 7.5f, -2), Quaternion.identity);
        }
    }
}