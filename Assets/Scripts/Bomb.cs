using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float time = 2f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0f)
        {
            Destroy(this.gameObject);
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
        else if(other.gameObject.tag == "enemy")
        { Destroy(other.gameObject); }
    }


    void Settime(float x)
    {
        time = x;
    }
}
