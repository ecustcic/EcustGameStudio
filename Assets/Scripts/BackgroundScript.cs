using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BackgroundScript : MonoBehaviour {
    public float speed = -2;
    public int jugde = -15;
    public float change_position = 30f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0f, speed * Time.deltaTime, 0f);
        if(transform.position.y <= jugde)
        {
            transform.Translate(0f, change_position, 0f);
        }
	}
}
