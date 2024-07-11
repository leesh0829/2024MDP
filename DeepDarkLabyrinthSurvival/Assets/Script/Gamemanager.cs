using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;
    public int attcount = 0;
    private int hitcount = 0;
    public GameObject player;
    public GameObject bgm;
    //public bool TimeOver = false;

    private void Awake()
    {
        Debug.LogWarning("GameManager Awake called");

        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
            Debug.Log("�̱��� ���� ���� ��");
        }
        else if (instance != this)
        {
            //Destroy(gameObject);
            Debug.Log("����--------------------");
        }
    }

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
    }

    public void Update()
    {
        if (TimeManager.instance.gamestart == true)
        {
            if (TimeLimitManager.instance.time <= 0)
            {
                //TimeOver = true;
                Monster_Aitest.instance.jumpScare();
                Debug.Log("Ÿ�� ����");
                GameOver();
            }
        }
    }

    public void ClearGame()
    {
        if (attcount < MazeSpawner.instance.MonstCNT)
        {
            attcount++;
            Debug.Log("attcount" + attcount);
        }
        if(attcount >= MazeSpawner.instance.MonstCNT)
        {
            //Ŭ����� �̵�
            TimeManager.instance.gamestart = false;
            TimeManager.instance.TimerOn = false;
            SceneManager.LoadScene("GameClear");
        }
    }

    public void GameOver()
    {
        if(hitcount == 1)
        {
            //���ӿ����� �̵�
            Destroy(bgm);
            TimeManager.instance.gamestart = false;
            TimeManager.instance.TimerOn = false;
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            hitcount++;
            Debug.Log("hitcount: " + hitcount);
        }
    } 

}
