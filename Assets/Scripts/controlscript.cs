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
    AudioSource explode_sound;
    public Text score;
    public Text HP;
    public Text END;
    public Text Bomb;
    public int fakebomb = 3;
    private float bombcd = 1f;
    // Use this for initialization

    void Start () {
        explode_sound = gameObject.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (bombcd >= 0)
            bombcd -= Time.deltaTime;
        if(healthpoint <= 0)//调用玩家死亡的函数
        {
            PlayerDied();
        }
        if (Input.GetKeyDown(KeyCode.R))//重置游戏场景
        {
            SceneManager.LoadScene("Main");
        }
        if (Input.GetKeyDown(KeyCode.Escape))//切换到菜单场景
        {
            SceneManager.LoadScene("caidan");
        }
        if (isRunning == true)//基本的UI
        {
            score.text = "Your score: " + playerScore.ToString();
            HP.text = "Your HP: " + healthpoint;
            Bomb.text = "You have " + fakebomb + " BOMB left";
        }
        else if(isRunning == false && dead == true)
        {
            END.text = "菜";
            score.text = "";
            HP.text = "";
        }
        else if(isRunning == false && win == true)
        {
            END.text = "YOU WIN\nyour score was:" + playerScore.ToString();
        }
        if (Input.GetKeyDown(KeyCode.B) && fakebomb > 0 && bombcd <= 0)
        {
            fakebomb -= 1;
            bombcd = 1f;
        }
    }

    public void Addscore(int number)//让小怪死亡之前调用这个函数来加分，加分显示在UI上
    {
        playerScore += number;
    }
    
    public void PlayerDied()//你死了，死了死了死翘翘了
    {
        isRunning = false;
        dead = true;
    }

    public void PlayerWin()//吼吼你赢了
    {
        isRunning = false;
        win = true;
    }

    public void Explode_sound()//这是播放爆炸声的函数
    {
        explode_sound.Play();
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
