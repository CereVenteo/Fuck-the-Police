using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("start_menu");
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("Assignment 01");
    }
    public void CloseGame()
    {
        Application.Quit();
    }
}
