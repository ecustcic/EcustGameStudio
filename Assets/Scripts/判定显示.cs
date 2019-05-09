using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 判定显示 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            transform.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
