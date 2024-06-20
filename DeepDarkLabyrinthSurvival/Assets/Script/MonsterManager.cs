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
<<<<<<< HEAD
        int MonAtt = Gamemanager.instance.attcount;
        //Debug.Log("MM에서 처치 수:" +  MonAtt);
        MonText.text = (MonCnt-MonAtt).ToString();
=======
        MonText.text = MonCnt.ToString();
>>>>>>> parent of 67d3fbd8 (Merge pull request #47 from leesh0829/Adelie)
    }
}
