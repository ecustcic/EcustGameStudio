using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zijijv : MonoBehaviour
{
    private Transform target;
    public float moveSpeed = 3f;
    private Vector3 direction;
    // Start is called before the first frame update

    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("player");
        target = go.transform;
        direction = (target.position - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawLine(target.transform.position, this.transform.position, Color.yellow);

        //lock at target Player       
        //Move towards target

        transform.position += direction * moveSpeed * Time.deltaTime;
    }
}
