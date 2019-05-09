using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteotspawn : MonoBehaviour
{
    public float spawntime_1 = 3f;
    public float spawndecrement = 0f;
    public float spawntime_2 = 10f;
    public float spawntime_3 = 20f;
    public float leveltime = 50f;
    private float waittime = 10f;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject boss;
    public int wave = 1;
    private int bossleft = 1;
    int wave_2 = 1;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {        
        if (leveltime > 0)
        {
            leveltime -= Time.deltaTime;
            spawntime_1 -= Time.deltaTime;
            spawntime_2 -= Time.deltaTime;
            spawntime_3 -= Time.deltaTime;
            if (spawntime_1 <= 0)
            {
                Vector3 pos = transform.position;
                if (spawndecrement <= 2f)
                {
                    spawndecrement += .2f;
                }
                Instantiate(enemy1, new Vector3(pos.x + Random.Range(-5, 5), 8f, -2), Quaternion.identity);
                spawntime_1 = 3f - spawndecrement;
            }
            if (spawntime_2 <= 0)
            {
                Vector3 pos = transform.position;
                if (wave == 1)
                {
                    Instantiate(enemy2, new Vector3(pos.x - 3, 8f, -2), Quaternion.identity);
                    Instantiate(enemy2, new Vector3(pos.x + 3, 8f, -2), Quaternion.identity);
                    wave = 2;
                    spawntime_2 = 5f;
                }
                else
                {
                    Instantiate(enemy2, new Vector3(pos.x, 8f, -2), Quaternion.identity);
                    spawntime_2 = 5f;
                    wave = 1;
                }
            }
            if (spawntime_3 <= 0)
            {
                Vector3 pos = transform.position;
                if (wave_2 == 1)
                {
                    Instantiate(enemy3, new Vector3(pos.x, 8f, -2), Quaternion.identity);
                    Instantiate(enemy3, new Vector3(pos.x + 4, 8f, -2), Quaternion.identity);
                    Instantiate(enemy3, new Vector3(pos.x - 4, 8f, -2), Quaternion.identity);
                    spawntime_3 = 20f;
                    wave_2 = 2;
                }
                else if (wave_2 == 2)
                {
                    Instantiate(enemy3, new Vector3(pos.x + 2, 8f, -2), Quaternion.identity);
                    Instantiate(enemy3, new Vector3(pos.x - 2, 8f, -2), Quaternion.identity);
                    spawntime_3 = 20f;
                    wave_2 = 3;
                }
                else if (wave_2 == 3)
                {
                    Instantiate(enemy3, new Vector3(pos.x + 5, 8f, -2), Quaternion.identity);
                    spawntime_3 = .5f;
                    wave_2 = 4;
                }
                else if (wave_2 == 4)
                {
                    Instantiate(enemy3, new Vector3(pos.x + 4, 8f, -2), Quaternion.identity);
                    spawntime_3 = .5f;
                    wave_2 = 5;
                }
                else if (wave_2 == 5)
                {
                    Instantiate(enemy3, new Vector3(pos.x + 3, 8f, -2), Quaternion.identity);
                    spawntime_3 = .5f;
                    wave_2 = 6;
                }
                else if (wave_2 == 6)
                {
                    Instantiate(enemy3, new Vector3(pos.x - 5, 8f, -2), Quaternion.identity);
                    spawntime_3 = .5f;
                    wave_2 = 7;
                }
                else if (wave_2 == 7)
                {
                    Instantiate(enemy3, new Vector3(pos.x - 4, 8f, -2), Quaternion.identity);
                    spawntime_3 = .5f;
                    wave_2 = 8;
                }
                else
                {
                    Instantiate(enemy3, new Vector3(pos.x - 3, 8f, -2), Quaternion.identity);
                    spawntime_3 = 20f;
                    wave_2 = 1;
                }
            }
        }
        else if(leveltime <= 0)
        {
            if (waittime > 0)
            {
                waittime -= Time.deltaTime;
            }
            else
            {
                if (bossleft == 1)
                {
                    Vector3 pos = transform.position;
                    Instantiate(boss, new Vector3(pos.x, 14f, -2), Quaternion.identity);
                    bossleft = 0;
                }
            }
        }
    }
}