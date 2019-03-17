using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class controlscript : MonoBehaviour {
    public gamecontrol gamecontrol;
    public GameObject player;
    bool isRunning = true;
    bool win = false;
    bool dead = false;
    int playerScore = 0;
    public int healthpoint = 5;
    AudioSource audioSource;
    public Text score;
    public Text HP;
    public Text END;
    // Use this for initialization
    void Start () {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if(healthpoint <= 0)
        {
            PlayerDied();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Main");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit(); ;
        }
        if (isRunning == true)
        {
            score.text = "Your score: " + playerScore.ToString();
            HP.text = "Your HP: " + healthpoint;
        }
        else if(isRunning == false && dead == true)
        {
            END.text = "YOU DIED";
            score.text = "";
            HP.text = "";
        }
        else if(isRunning == false && win == true)
        {
            END.text = "YOU WIN\nyour score was:" + playerScore.ToString();
        }
    }

    public void Addscore(int number)
    {
        playerScore += number;
    }
    
    public void PlayerDied()
    {
        isRunning = false;
        dead = true;
    }

    public void PlayerWin()
    {
        isRunning = false;
        win = true;
    }

    public void Explode_sound()
    {
        audioSource.Play();
    }

    /*void OnGUI()
    {
        if(isRunning == true)
        {
            GUI.Label(new Rect(5, 5, 150, 50), "Player Score: " + playerScore);
            GUI.Label(new Rect(5, 20, 100, 30), "Player HP: " + gamecontrol.HP);
        }
        else if(dead == true)
        {
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "You Died, your score was:" + playerScore);
        }
        else if (win == true)
        {
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "YOU WIN,your score was:" + playerScore);
        }
    }
    */
    public void LowerHP()
    {
        healthpoint -= 1;
    }

    public void IncreaseHP()
    {
        healthpoint += 1;
    }

    public void Reborn()
    {
        Instantiate(player, new Vector3(0f, -7f, -2f), Quaternion.Euler(0f, 0f, 180f));
    }
}
