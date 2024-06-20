using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterManager : MonoBehaviour
{
    public int MonCnt = MazeSpawner.instance.MonstCNT;
    public Text MonText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int MonAtt = Gamemanager.instance.attcount;
        Debug.Log("MM에서 처치 수:" +  MonAtt);
        MonText.text = (MonCnt-MonAtt).ToString();
    }
}
