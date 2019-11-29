using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    Quaternion rotation;
    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);
        rotation = Quaternion.Euler(45, 90, 0);
    }

    private void OnMouseUpAsButton()
    {
        if (menu.activeSelf)
        {
            menu.SetActive(false);
        }
        else
        {
            if(menu.name == "Police_Menu" && this.GetComponent<Agent_Variables>().waiting)
                menu.SetActive(true);
            else if(menu.name == "Criminal_Menu" && this.GetComponent<Criminal_Variables>().waiting)
                menu.SetActive(true);
            menu.transform.rotation = rotation;
            menu.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 8, this.gameObject.transform.position.z);
        }
    }
}
