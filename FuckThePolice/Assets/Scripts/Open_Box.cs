using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open_Box : MonoBehaviour
{
    GameObject menu;
    bool active;
    void Start()
    {
        menu = GameObject.Find("Box_Car_Menu");
        menu.SetActive(false);
        active = false;
    }

    private void OnMouseUpAsButton()
    {
        if(active)
        {
            menu.SetActive(false);
        }
        else
        {
            menu.SetActive(true);
            menu.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 4, this.gameObject.transform.position.z);
        }
    }
}
