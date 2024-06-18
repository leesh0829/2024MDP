using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    private int Intermission = 5;
    public Text ClockText;
    float time;
    public GameObject Room;

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
            //player.transform.Translate(new Vector3(0, 1, 92));
            //Room.transform.position = new Vector3(0, 0, 4);
            Room.SetActive(false);
        }
    }

    /*private void InterMission()
    {
        time = Intermission;
        time -= Time.deltaTime;
        ClockText.text = time.ToString();
    }*/
}
