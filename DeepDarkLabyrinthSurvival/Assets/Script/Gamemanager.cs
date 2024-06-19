using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int attcount = 0;
    private int hitcount = 0;
    public GameObject player;

    private void Awake()
    {
        Debug.LogWarning("GameManager Awake called");

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("�̱��� ���� ���� ��");
        }
        else if (instance != this)
        {
            Destroy(gameObject);
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

    public void ClearGame()
    {
        if(attcount == 10)
        {
            //Ŭ����� �̵�
            player.transform.Translate(new Vector3(-20, 0, -20));
        }
        else
        {
            attcount++;
            Debug.Log("attcount" + attcount);
        }
    }

    public void GameOver()
    {
        if(hitcount == 3)
        {
            //���ӿ����� �̵�
            player.transform.Translate(new Vector3(-10, 0, -10));
        }
        else
        {
            hitcount++;
            Debug.Log("hitcount: " + hitcount);
        }
    } 

}
