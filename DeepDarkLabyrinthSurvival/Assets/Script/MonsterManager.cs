using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterManager : MonoBehaviour
{
    public int MonCnt = MazeSpawner.instance.goalcount;
    public Text MonText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MonText.text = MonCnt.ToString();
    }
}
