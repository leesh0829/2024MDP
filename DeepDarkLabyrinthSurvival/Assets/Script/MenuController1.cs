using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController1 : MonoBehaviour
{
    public void StartBtn()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
