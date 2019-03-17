using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sheild : MonoBehaviour
{
    public float time = 20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if(time <= 0f)
        {
            transform.GetComponent<Collider>().enabled = false;
            transform.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            transform.GetComponent<Collider>().enabled = true;
            transform.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemybullet")
        { Destroy(other.gameObject); }
    }

    void Settime(float x)
    {
        time = x;
    }
}
