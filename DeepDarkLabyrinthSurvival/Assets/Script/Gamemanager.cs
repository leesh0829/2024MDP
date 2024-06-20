using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;
    private int attcount = 0;
    private int hitcount = 0;
    public GameObject player;

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

    void Update()
    {
        if (TimeManager.instance.gamestart == true)
        {
            if (TimeLimitManager.instance.time <= 0)
            {
                Debug.Log("Ÿ�� ����");
                GameOver();
            }
        }
    }

    public void ClearGame()
    {
        if(attcount == 10)
        {
            //Ŭ����� �̵�]
            TimeManager.instance.gamestart = false;
            TimeManager.instance.TimerOn = false;
            SceneManager.LoadScene("GameClear");
        }
        else
        {
            attcount++;
            Debug.Log("attcount" + attcount);
        }
    }

    public void GameOver()
    {
        if(hitcount == 1)
        {
            //���ӿ����� �̵�
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
