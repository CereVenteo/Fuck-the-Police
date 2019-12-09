using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Buy_Object : MonoBehaviour
{
    public Game_Manager Game_Manager;
    public GameObject menu;
    public GameObject object_buy;
    public GameObject box;
    Canvas canvas;
    void Start()
    {
        Game_Manager = GameObject.Find("Game_Manager").GetComponent<Game_Manager>();
    }
    private void OnMouseUpAsButton()
    {
        canvas = GameObject.Find("Game_Manager").GetComponent<Canvas>();
        if(canvas.GetStars() > 0)
        {
            BuyObject();
            menu.SetActive(false);
        }
    }

    void BuyObject()
    {
        canvas.SetStars(canvas.GetStars() - 1);
        object_buy.SetActive(true);
        if(object_buy.name == "Desktop_3" || object_buy.name == "Desktop_4" || object_buy.name == "Desktop_5")
        {
            Game_Manager.AddPolice();
        }
        if(object_buy.name == "Cell_2")
        {
            Game_Manager.free_cells[1] = true;
        }
        else if(object_buy.name == "Cell_3")
        {
            Game_Manager.free_cells[2] = true;
        }
        else if (object_buy.name == "Cell_4")
        {
            Game_Manager.free_cells[3] = true;
        }
        box.SetActive(false);
    }
}
