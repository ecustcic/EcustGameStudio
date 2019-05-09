using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explode : MonoBehaviour
{
    public float time = 0.4f;
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
}
