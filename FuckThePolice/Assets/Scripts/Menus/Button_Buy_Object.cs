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
    int cost_desktop;
    int cost_car;
    int cost_cell;
    int cost_interrogation_room;

    void Start()
    {
        Game_Manager = GameObject.Find("Game_Manager").GetComponent<Game_Manager>();
        cost_desktop = 400;
        cost_car = 600;
        cost_cell = 700;
        cost_interrogation_room = 1000;
    }

    private void OnMouseUpAsButton()
    {
        canvas = GameObject.Find("Game_Manager").GetComponent<Canvas>();
        
        if ((canvas.GetPoints() >= cost_desktop) && (object_buy.name == "Desktop_3" || object_buy.name == "Desktop_4" || object_buy.name == "Desktop_5"))
        {
            canvas.SetPoints(-cost_desktop);
            BuyObject();
            Game_Manager.AddPolice();
        }
        else if ((canvas.GetPoints() >= cost_cell) && (object_buy.name == "Cell_2"))
        {
            canvas.SetPoints(-cost_cell);
            BuyObject();
            Game_Manager.free_cells[1] = true;
        }
        else if ((canvas.GetPoints() >= cost_cell) && (object_buy.name == "Cell_3"))
        {
            canvas.SetPoints(-cost_cell);
            BuyObject();
            Game_Manager.free_cells[2] = true;
        }
        else if ((canvas.GetPoints() >= cost_cell) && (object_buy.name == "Cell_4"))
        {
            canvas.SetPoints(-cost_cell);
            BuyObject();
            Game_Manager.free_cells[3] = true;
        }
        else if ((canvas.GetPoints() >= cost_car) && (object_buy.name == "Police_Car_2" || object_buy.name == "Police_Car_3" || object_buy.name == "Police_Car_4"))
        {
            canvas.SetPoints(-cost_car);
            BuyObject();
        }
        else if ((canvas.GetPoints() >= cost_interrogation_room) && (object_buy.name == "Interrogatory Room_2"))
        {
            canvas.SetPoints(-cost_interrogation_room);
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
