using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open_Box : MonoBehaviour
{
    public GameObject menu;
    Canvas canvas;
    void Start()
    {
        menu.SetActive(false);
        canvas = GameObject.Find("Game_Manager").GetComponent<Canvas>();
    }

    private void OnMouseUpAsButton()
    {
        if(menu.activeSelf)
        {
            menu.SetActive(false);
        }
        else
        {
            menu.SetActive(true);
            menu.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 4, this.gameObject.transform.position.z);
            
            if (canvas.GetStars() > 0)
            {
                //Poner boton oscuro
            }
        }
    }
}
