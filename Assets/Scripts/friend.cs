using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class friend : MonoBehaviour
{
    float speedx = -2f;
    float speedy = -2f;
    public float lifetime = 20f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        transform.Translate((speedx) * Time.deltaTime, (speedy) * Time.deltaTime, 0f);
        if (transform.position.x <= -6f)
        {
            speedx *= -1;
        }
        if (transform.position.x >= 6f)
        {
            speedx *= -1;
        }
        if (transform.position.y <= -6f)
        {
            speedy *= -1;
        }
        if (transform.position.y >= 7.5f)
        {
            speedy *= -1;
        }
        if(lifetime <= 0)
        {
            Destroy(this.gameObject);
        }
        
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
