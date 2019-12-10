using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Buy_Object : MonoBehaviour
{
    Game_Manager Game_Manager;
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
        
        if ((canvas.GetPoints() >= 500) && (object_buy.name == "Desktop_3" || object_buy.name == "Desktop_4" || object_buy.name == "Desktop_5"))
        {
            canvas.SetPoints(-500);
            BuyObject();
            Game_Manager.AddPolice();
        }
        else if ((canvas.GetPoints() >= 1000) && (object_buy.name == "Cell_2"))
        {
            canvas.SetPoints(-1000);
            BuyObject();
            Game_Manager.free_cells[1] = true;
        }
        else if ((canvas.GetPoints() >= 1000) && (object_buy.name == "Cell_3"))
        {
            canvas.SetPoints(-1000);
            BuyObject();
            Game_Manager.free_cells[2] = true;
        }
        else if ((canvas.GetPoints() >= 1000) && (object_buy.name == "Cell_4"))
        {
            canvas.SetPoints(-1000);
            BuyObject();
            Game_Manager.free_cells[3] = true;
        }
        else if ((canvas.GetPoints() >= 800) && (object_buy.name == "Police_Car_2" || object_buy.name == "Police_Car_3" || object_buy.name == "Police_Car_4"))
        {
            canvas.SetPoints(-800);
            BuyObject();
        }
        else if ((canvas.GetPoints() >= 1500) && (object_buy.name == "Interrogatory Room_2"))
        {
            canvas.SetPoints(-1500);
            BuyObject();
        }
    }

    void BuyObject()
    {
        object_buy.SetActive(true);
        box.SetActive(false);
        menu.SetActive(false);
    }
}
