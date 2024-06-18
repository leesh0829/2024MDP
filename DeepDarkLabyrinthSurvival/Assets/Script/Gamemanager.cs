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
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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
        }
    } 

}
