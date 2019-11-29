using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Buy_Object : MonoBehaviour
{
    public GameObject menu;
    public GameObject object_buy;
    public GameObject police;
    public GameObject box;
    Canvas canvas;

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
        police.SetActive(true);
        box.SetActive(false);
    }
}
