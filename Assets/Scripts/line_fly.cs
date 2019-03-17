using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class line_fly : MonoBehaviour
{
    public float speed = -3f;
    // Use this for initialization

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0f, (speed) * Time.deltaTime, 0f);
    }
}
