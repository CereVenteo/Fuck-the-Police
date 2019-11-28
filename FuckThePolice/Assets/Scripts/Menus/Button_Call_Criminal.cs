using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Call_Criminal : MonoBehaviour
{
    public GameObject menu;

    private void OnMouseUpAsButton()
    {
        CallCriminal();
        menu.SetActive(false);

    }

    void CallCriminal()
    {

    }
}
