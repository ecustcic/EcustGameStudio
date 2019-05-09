using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour {
    public controlscript control;
    // Use this for initialization
    void Start () {
        
}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "bomb")
            Destroy(other.gameObject);
    }
}
