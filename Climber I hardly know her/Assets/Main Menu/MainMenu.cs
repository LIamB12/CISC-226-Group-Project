using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{


    public void LoadScene(String level)
    {
        SceneManager.LoadScene(level);
    }
    

}
