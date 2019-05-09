using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatingbullet : MonoBehaviour
{
    public float firstspeed = -2f;
    public float minusspeed = 0f;
    float elapsedTime = 0f;
    public GameObject bullet;
    private float count;
    // Start is called before the first frame update
    void Start()
    {
        count = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        minusspeed += Time.deltaTime * 1.5f;
        count += Time.deltaTime * 180;
        transform.position += new Vector3(0f, (firstspeed+minusspeed) * Time.deltaTime, 0f);
        elapsedTime += Time.deltaTime;
        transform.Rotate(0f, 0f, 60f);
        if (elapsedTime > .1f)
        {
            Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(0f, 0f, count));
            Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(0f, 0f, count + 180f));
            elapsedTime = 0;
        }
    }
}
