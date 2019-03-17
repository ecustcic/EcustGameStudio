using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
    float speed = 30f;
    //public star star;
    public controlscript control;
    // Use this for initialization
    void Start () {
        control = GameObject.Find("Gamecontrol").GetComponent<controlscript>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0f, speed * Time.deltaTime, 0f);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            Destroy(this.gameObject);
        }             
    }
}

