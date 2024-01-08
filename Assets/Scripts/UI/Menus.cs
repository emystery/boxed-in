using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit(); 
    }
}
//use trees/graph to make difficulty selection - figure this out
//use linkedlist to make a user system - speedruns and times list
//use hashtables for pressure plates