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
            Debug.Log("싱글톤 패턴 생성 됨");
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            Debug.Log("실패--------------------");
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
            //클리어방 이동
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
            //게임오버방 이동
            player.transform.Translate(new Vector3(-10, 0, -10));
        }
        else
        {
            hitcount++;
            Debug.Log("hitcount: " + hitcount);
        }
    } 

}
