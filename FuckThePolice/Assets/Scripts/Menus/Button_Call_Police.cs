using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Call_Police : MonoBehaviour
{
    public GameObject menu;

    private void OnMouseUpAsButton()
    {
        CallNextCitizen();
        menu.SetActive(false);
        
    }

    void CallNextCitizen()
    {

    }
}
