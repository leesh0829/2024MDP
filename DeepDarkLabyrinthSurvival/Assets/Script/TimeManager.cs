using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    private int Intermission = 20;
    public Text ClockText;
    float time;
    public GameObject Room;

    public static TimeManager instance;

    public bool TimerOn = false;
    public bool gamestart = false;
    public bool chksrt;

    private void Awake()
    {
        chksrt = true;
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            //Destroy(gameObject);
            Debug.Log("½ÇÆÐ--------------------");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //InterMission();
        time = Intermission;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        ClockText.text = ((int)time%60).ToString();

        if(((int)time % 60) == 0)
        {
            GameStart();
            //player.transform.Translate(new Vector3(0, 1, 92));
            //Room.transform.position = new Vector3(0, 0, 4);
        }
    }

    private void GameStart()
    {
        if(chksrt)
        {
            Room.SetActive(false);
            TimeLimitManager.instance.time = 210;
            TimerOn = true;
            gamestart = true;
            chksrt = false;
        }
    }

    /*private void InterMission()
    {
        time = Intermission;
        time -= Time.deltaTime;
        ClockText.text = time.ToString();
    }*/
}
