using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    GameObject instructions;
    bool starting;
    // Start is called before the first frame update
    void Start()
    {
        instructions = GameObject.Find("Instructions");
        starting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(starting)
        {
            InstructionsOn();
            starting = false;
        }
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("start_menu");
    }

    public void InstructionsOn()
    {
        Time.timeScale = 0;
        instructions.SetActive(true);
    }

    public void InstructionsOff()
    {
        Time.timeScale = 1;
        instructions.SetActive(false);
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
