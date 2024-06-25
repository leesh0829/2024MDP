using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLimitManager : MonoBehaviour
{
    public Text[] ClockText;
    public float time;

    public static TimeLimitManager instance;

    private void Awake()
    {
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
        
    }

    // Update is called once per frame
    void Update()
    {
        if(TimeManager.instance.TimerOn)
        {
            time -= Time.deltaTime;
            ClockText[0].text = ((int)time / 60).ToString();
            ClockText[1].text = ((int)time % 60).ToString();
        }
    }
}
